using UnityEngine;

// Добавление глобальных переменных счётчиков очков
public class add_score : MonoBehaviour
{
    // показатели для определение концовки игры ( по ходу игры могут уменьшать/прибавляться )
    // очки память - положительные
    public static int memory = 0;
    // очки хаоса - негативные
    public static int chaos = 0;
    // можно задать очки с самого начала (для теста)
    public int set_memory = 0;
    public int set_chaos = 0;

    void Start()
    {
        // задаёт начальные показатели в первой локации
        memory = set_memory;
        chaos = set_chaos;
    }
}
