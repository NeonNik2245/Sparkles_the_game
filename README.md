# Проект Искорка

Приключенческая квест игра от первого лица про человека, попавшего в мир снов и забывшего кто он есть.\
Задача, пробираясь по локациям мира, либо выбраться и вспомнить кто ты есть, собирая искры воспоминаний и решая различные загадки, либо навсегда остаться в междумирье.\
Игра будет разработана на игровом движке Unity с 3D графикой и с внедрением LLM-технологий.

### Установка проекта:

1. Установить версию `2022.3.62f3 LST` для проекта Unity
2. Скачать архив / клонировать проект отсюда в локальную папку

>**ВАЖНО**: НЕ импортировать проект `labirint_20` в Unity Hub и не запускать _(т.к. текстуры, материалы не будут отображаться в дальнейшем)_
3. Скачиваем все Ассеты из списка ссылок:
    1. #NVJOB Water Shaders V2 > [Water Shaders V2.x](https://assetstore.unity.com/packages/vfx/shaders/water-shaders-v2-x-149916)
    0. [8K Skybox Pack Free](https://assetstore.unity.com/packages/2d/textures-materials/sky/8k-skybox-pack-free-150926)
    0. AllSkyFree > [AllSky Free - 10 Sky / Skybox Set](https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014)
    0. Almgp_unity5_ice > [Autumn planet pack](https://assetstore.unity.com/packages/vfx/shaders/autumn-planet-pack-61186)
    0. ANGRY MESH > [FREE Snowman](https://assetstore.unity.com/packages/3d/props/free-snowman-105123)
    0. Dry_Trees > [Dry Trees](https://assetstore.unity.com/packages/3d/vegetation/trees/dry-trees-86967)
    0. Fantasy Forest Environment Free Sample > [Fantasy Forest Environment - Free Demo](https://assetstore.unity.com/packages/3d/environments/fantasy/fantasy-forest-environment-free-demo-35361)
    0. GrassFlowers > [Grass Flowers Pack Free](https://assetstore.unity.com/packages/2d/textures-materials/nature/grass-flowers-pack-free-138810)
    0. [Mini Nature Pack](https://assetstore.unity.com/packages/3d/vegetation/mini-nature-pack-54072)
    0. NatureManufacture Assets > [Winter Mountains and Stamps](https://assetstore.unity.com/packages/3d/environments/landscapes/winter-mountains-and-stamps-129245)
    0. NOT_Lonely > [HQ Big Rock](https://assetstore.unity.com/packages/3d/props/exterior/hq-big-rock-50759)
    0. Real Stars Skybox > [Real Stars Skybox Lite](https://assetstore.unity.com/packages/3d/environments/sci-fi/real-stars-skybox-lite-116333)
    0. Realistic Hail Set > [Hail Particles Pack](https://assetstore.unity.com/packages/vfx/particles/environment/hail-particles-pack-62038)
    0. SineVFX > [Translucent Crystals](https://assetstore.unity.com/packages/3d/environments/fantasy/translucent-crystals-106274)
    0. SkySeries Freebie > [Skybox Series Free](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)
    0. Trees_WorldSpace_FREE > [World Space Trees (FREE) - Shader](https://assetstore.unity.com/packages/vfx/shaders/world-space-trees-free-shader-117088)
    0. YughuesFreeRockMaterials > [Yughues Free Rock Materials](https://assetstore.unity.com/packages/2d/textures-materials/stone/yughues-free-rock-materials-12962)
    0. LLMUnity > [LLM for Unity](https://assetstore.unity.com/packages/tools/ai-ml-integration/llm-for-unity-273604)

4. Переносим папки скаченных ассетов в папку проекта `labirint_20/Assets`
5. Импортируем проект `labirint_20` в Unity Hub
6. Ждём загрузку библиотек и среда разработки настроена
7. В игровом объекте LLM проекта установить модель например: [Meta-Llama-3.1-8B-Instruct-Q4_K_M.gguf](https://huggingface.co/bartowski/Meta-Llama-3.1-8B-Instruct-GGUF?show_file_info=Meta-Llama-3.1-8B-Instruct-Q4_K_M.gguf)

