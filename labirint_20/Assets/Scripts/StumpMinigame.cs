using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Миниигра с пнём во второй локации
public class StumpMinigame : MonoBehaviour
{
    [System.Serializable] // Чтобы видеть в инспекторе
    // Информация о предметах в виде класса
    public class Item
    {
        public GameObject gameObject;           // Объект
        public string name;                     // Имя
        public SpriteRenderer spriteRenderer;   // Визуализация
        public bool isHeld = false;             // Взято ли в руку
        public Vector3 originalPosition;        // Где лежит в начале (есть лево, центр и право)
        // Возможные позиции - лево, центр, право
        public Vector3 leftPosition; // Лево
        public Vector3 centerPosition; // Центр
        public Vector3 rightPosition; // Право
        public int currentPosition; // Текущая позиция. 1 - лево, 2 - центр, 3 - право
    }

    [Header("Настройки предметов")]
    public Item[] items; // 0: Пень, 1: Бант, 2: Ножницы, 3: Книга
    public int currentSelectedIndex = 1; // Текущий выбранный предмет. Будет активен при начале миниигры
    
    [Header("Настройки позиций")]
    public Transform stumpTransform;            // Позиция пня
    //public Transform[] placementPositions;      // Позиции (0, 1, 2) для размещения предметов на пне
    
    [Header("Настройки UI")]
    public GameObject messagePanel;     // Панелька для сообщений
    // public Text messageText;            // Сам текст
    public GameObject hintPanel;        // Подсказка по управлению
    //public Text hintText;               // Текст для подсказки
    // public GameObject selectionFrame;   // Рамка выбранного предмета
    
    // [Header("Сообщения")]
    // public string startMessage = "Расставьте предметы на пне в правильном порядке!";
    // public string hintMessage = "A/D: Выбор предмета | Z: Взять/положить | Стрелки: Переместить | X: Выход";
    
    [Header("Правильный порядок")]
    public string[] correctOrder = { "Ножницы", "Бант", "Книга" }; // Нужный порядок
    
    private Camera mainCamera;                  // Для запоминания камеры
    private Vector3 cameraOriginalPosition;     // Позиция камеры до игры
    private Quaternion cameraOriginalRotation;  // Вращение камеры до игры
    private bool isMinigameActive = false;      // Текущее состояние игры
    private Item heldItem = null;               // Какой предмет сейчас в руке
    private bool alreadyCompleted = false;      // Для отрицания повторного прохождения
    
    private Vector3 frameOffset = new Vector3(0, 0.1f, 0); // Смещение рамки

    void Start() // При появлении в сцене
    {
        mainCamera = Camera.main;
        
        // Деактивация панелей с текстом до их появления
        messagePanel.SetActive(false);
        hintPanel.SetActive(false);
        //selectionFrame.SetActive(false); // Деактивация отдельно рамки вокруг предметов
    }
    
    // Начинаем игру
    public void StartMinigame()
    {
        // Не даём пройти игру многократно
        if (alreadyCompleted) return;

        isMinigameActive = true;
        
        // Фиксируем камеру
        cameraOriginalPosition = mainCamera.transform.position;
        cameraOriginalRotation = mainCamera.transform.rotation;
        MouseCamera.moving = false;
        

        // Позиционируем камеру над пнем
        Vector3 cameraPosition = stumpTransform.position + new Vector3(0, 5f, -5f);
        mainCamera.transform.position = cameraPosition;
        mainCamera.transform.LookAt(stumpTransform.position + new Vector3(0, 1.4f, 0));
        
        StartCoroutine(EndGameAfterDelay(15f)); // Прерываем игру через 15 секунд

        // Показываем сообщения
        ShowStartMessage();
        
        // Активируем подсказку
        hintPanel.SetActive(true);
        // hintText.text = hintMessage;
        
        // Выбираем первый предмет
        SelectItem(1);
    }
    
    void ShowStartMessage()
    {
        messagePanel.SetActive(true);
        
        // Скрываем сообщение через 8 секунд
        StartCoroutine(HideMessageAfterDelay(8f));
    }
    
    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePanel.SetActive(false);
    }
    
    IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ExitMinigame();
    }

    void SelectItem(int index)
    {
        if (heldItem != null && FindItemAtPosition(index) == heldItem) return;
        currentSelectedIndex = index;

        //if (selectionFrame != null) UpdateSelectionFrame();
    }

    // Каждый кадр
    void Update()
    {
        if (!isMinigameActive) return;
        
        HandleInput(); // Обрабатываем ввод
        //UpdateSelectionFrame(); // Меняем рамку, если надо
    }
    
// //////////////////////

    // Обработка нажатия кнопок
    void HandleInput()
    {
        // Выход из игры
        if (Input.GetKeyDown(KeyCode.X))
        {
            ExitMinigame();
            return;
        }
        
        // Взять/отпустить предмет
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (heldItem == null)
            {
                // Берем предмет в руку
                PickUpItem(FindItemAtPosition(currentSelectedIndex));
            }
            else
            {
                // Отпускаем предмет
                ReleaseItem();
            }
        }
        
        if (heldItem != null) // Если уже держим что-то
        {
            // Меняем местами с предметом слева на A
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwapWithNeighbor(-1); // -1 = влево
            }
            // Меняем местами с предметом справа на D
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwapWithNeighbor(1); // 1 = вправо
            }
        }
        else
        {
            // Если предмет не в руке - просто выбираем другой
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectPreviousItem();
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectNextItem();
            }
        }
    }
    
    // void UpdateSelectionFrame()
    // {
    //     if (!isMinigameActive || selectionFrame == null) return;
    
    //     // Если предмет в руке или игра не активна - скрываем рамку
    //     if (heldItem != null || !isMinigameActive)
    //     {
    //         selectionFrame.SetActive(false);
    //         return;
    //     }
        
    //     // Получаем текущий выбранный предмет
    //     Item selectedItem = FindItemAtPosition(currentSelectedIndex);
        
    //     if (selectedItem.gameObject == null) return;
        
    //     // Активируем и позиционируем рамку над предметом
    //     selectionFrame.SetActive(true);
        
    //     // Позиция рамки = позиция предмета + небольшое смещение вверх
    //     Vector3 itemPosition = selectedItem.gameObject.transform.position;
    //     selectionFrame.transform.position = itemPosition + frameOffset;
    // }

    void ExitMinigame()
    {
        if (!isMinigameActive) return;

        isMinigameActive = false;
        
        // Возвращаем камеру
        mainCamera.transform.position = cameraOriginalPosition;
        mainCamera.transform.rotation = cameraOriginalRotation;
        MouseCamera.moving = true;

        // Скрываем UI
        hintPanel.SetActive(false);
        //selectionFrame.SetActive(false);
        if (heldItem != null) ReleaseItem();

        alreadyCompleted = true; // Чтобы не запускать игру вновь
        
        Debug.Log("Мини-игра завершена");
    }

    // Смещаем выбор влево, по кругу
    void SelectPreviousItem()
    {
        int newIndex = currentSelectedIndex - 1;
        if (newIndex < 1) newIndex = items.Length - 1; // Пень с индексом 0 пропускается
        SelectItem(newIndex);
    }
    
    // Смещаем выбор вправо, по кругу
    void SelectNextItem()
    {
        int newIndex = currentSelectedIndex + 1;
        if (newIndex >= items.Length) newIndex = 1;
        SelectItem(newIndex);
    }
    
    // Подбор предмета
    void PickUpItem(Item item)
    {
        if (item == items[0] || heldItem != null) return;
        heldItem = item;
        item.isHeld = true;
        item.gameObject.transform.position += new Vector3(0, 0.5f, 0); // Приподнимем его
    }

    // Отпускание предмета
    void ReleaseItem()
    {
        if (heldItem == null) return;
        heldItem.isHeld = false;
        
        if (heldItem.currentPosition == 0) // Если предмет улетел
        {
            heldItem.gameObject.transform.position = heldItem.originalPosition;
        }
        else
        {
            PlaceItemOnPosition(heldItem, heldItem.currentPosition); // Автоматически опустится
        }
        heldItem = null;
        CheckSolution();
    }

    // Поместить предмет на заготовленную позицию
    void PlaceItemOnPosition(Item item, int position)
    {
        Vector3 targetPosition;
        switch (position)
        {
            case 1: targetPosition = item.leftPosition; break;
            case 2: targetPosition = item.centerPosition; break;
            case 3: targetPosition = item.rightPosition; break;
            default: return;
        }
        if (item.isHeld) targetPosition += new Vector3(0, 0.5f, 0);
        item.currentPosition = position;
        item.gameObject.transform.position = targetPosition;
    }

    // Обмен двух соседей местами
    void SwapWithNeighbor(int direction)
    {
        if (heldItem == null) return;
        
        int heldPositionIndex = heldItem.currentPosition;
        if (heldPositionIndex == 0) 
        {Debug.Log("Ошибка с положением предмета");
        return;} // Не на пне, технически не должно такого быть
        
        int neighborIndex = heldPositionIndex + direction;
        
        if (neighborIndex < 1) neighborIndex = items.Length - 1; // Идём в конце списка
        if (neighborIndex > 3) neighborIndex = 1; // Идём в начало списка
        
        Item neighborItem = FindItemAtPosition(neighborIndex);
        if (neighborItem == null) return;
        
        SwapItems(heldItem, neighborItem);
        currentSelectedIndex = neighborIndex;
        Debug.Log($"Поменяли местами: {heldItem.name} и {neighborItem.name}");
        //CheckSolution();
    }

    // Получает предмет по его позиции в числе
    Item FindItemAtPosition(int position)
    {
        foreach (Item item in items)
        {
            if (item.currentPosition == position) //&& item != heldItem)
                return item;
        }
        return null;
    }

    // Меняет двух соседей местами
    void SwapItems(Item item1, Item item2)
    {
        int tempPos = item1.currentPosition; // Временное хранилище позиции
        item1.currentPosition = item2.currentPosition;
        item2.currentPosition = tempPos;
        
        PlaceItemOnPosition(item1, item1.currentPosition);
        PlaceItemOnPosition(item2, item2.currentPosition);
    }
    
    void CheckSolution()
    {
        // Собираем предметы на пне в порядке позиций
        string[] currentOrder = new string[3];
        
        for (int i = 1; i <= 3; i++) // Позиции 1,2,3
        {
            Item item = FindItemAtPosition(i);
            currentOrder[i-1] = item.name;
        }
        
        // Проверяем совпадение
        bool isCorrect = true;
        for (int i = 0; i < 3; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                isCorrect = false;
                break;
            }
        }
        
        if (isCorrect)
        {
            add_score.memory += 3;
            Debug.Log("Поздравляем! Правильный порядок!");
            ExitMinigame();
        }
    }
    
    // Метод для активации мини-игры извне
    public void ActivateMinigame()
    {
        StartMinigame();
    }
}