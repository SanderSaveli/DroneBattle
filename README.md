# DroneBattle
Реализация тестового задания Савин А. А.

# Реализованный функционал: 
* Сбор ресурсов дронами
* Учет очков каждого игрока
* Миникарта
* Цветовое различие дронов
* Управления скоростью симуляции
* Отображение состояниядрона
* Динамическая настройка параметров симуляции через UI

# Описание архитектуры
## Дроны
AI дрона построен на [FSM](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/Tools/FSM/FSM.cs) с тремя состояниями: Поиск ресурса ([DroneSearchState](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/GameEntity/Drone/DroneSearchState.cs)), Сбор ресурса ([DroneCollectState](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/GameEntity/Drone/DroneCollectState.cs)) и транспортировка ресурса на фабрику ([DroneTransportState](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/GameEntity/Drone/DroneTransportState.cs)). Каждое состояние управляеит поведением дрона через интерфейс [IDroneController](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/GameEntity/Drone/IDroneController.cs), чтобы отделить общее поведение от конкретной реализации. Дрон использует [NavMeshPlus](https://github.com/h8man/NavMeshPlus) для поиска пути и избегания препятсвий. 

## Спавнеры
Спавнеры используют [абстрактные фабрики](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/Factories/AbstractFactory.cs) для создания тех или иных игровых объектов (Дронов и Ресурсов). Абстрактные фабрики в свою очередь используют [ObjectPool](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/Tools/ObjectPool/ObjectPool.cs) для улучшения оптимизации. Границы области, в которой создается предмет определяются с помощью Sprite Renderrer переданого в соответсвующие поля.

## Отображение очков
[База](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/GameEntity/Fabrik.cs) считает количество ресурсов которые в нее принесли. Она имеет публичное свойство Score и Action, вызываемый при изменении Score. View элемент отображения подписывается на это событие и актуализиурет значение текстового поля при срабатывании.

## Настройки
Каждая игровая сущьность получает в зависимость через Zenject класс со своими [настройками](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/Settings/AbstractSettings.cs). Она устанавливает значение полей в соответствии с полями переданного объекта и подписывается на событие изменения полей  для актуализации настроек. В [Ui](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/Managers/SettingsSetter.cs) достаточно просто менять переменные в файах настроек и нужные суьности будут сразу обновлять свои состояния. s

## Миникарта
[Миникарта](https://github.com/SanderSaveli/DroneBattle/blob/main/Assets/Source/Scripts/MinimapCamera.cs) представляет собой RawImage с текстурой, которую рендерит отельная камера, закрепленная сверху и захватывающая только один слой (В свою очедедь основная камера рендерит все, кроме этого слоя). Каждый объект который должен отобрааться на миникарте имееет SpriteRenderrer с нужным спрайтом, которому выставлен слой миникарты.

# Используемые техенологии:
* [NavMeshPlus](https://github.com/h8man/NavMeshPlus) - навигация
* DoTween - анимации
* Zenject - внедрения зависимостей
