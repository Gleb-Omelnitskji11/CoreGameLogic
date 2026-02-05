# Game Framework Core

Framework для швидкого створення hybrid-casual ігор з модульною архітектурою та розширюваними системами.

## Framework Architecture

### Як працює:

#### Event Bus
Framework використовує **Zenject SignalBus** для комунікації між системами. Це дозволяє слабко зв'язану архітектуру, де компоненти не знають один про одного безпосередньо. Використовую StaticContext, ProjectContext, SceneContext.

#### Economy
**CurrencyManager** зберігає усю валюту в грі. Підтримує:
- Зміну показника валюти через `AddCurrencies()`та `SpendCurrency()`
- Додавання унікальної валюти в залежності від гри, у прикладі enum `CurrencyType`
- Перевірка можливості витрати через `CanSpend()`
- Автоматичне збереження в `ICurrencyStorage` (у прикладі PlayerPrefs)

**Валюти зберігаються** через інтерфейс `ICurrencyStorage`, що дозволяє легко змінити спосіб збереження (PlayerPrefs, файли, сервер тощо).

#### Game State
**GameStateManager** керує життєвим циклом механіки. Кожна зміна стану генерує `GameStateChangedSignal` для інших систем.

#### Analytics
**IAnalyticsService** надає структуроване логування подій. У прикладі реалізовано `DebugAnalyticsService`, який виводить події в консоль Unity.

#### Monetization
**IRewardedAdsHolder** конфіг з списком нагород для кожного місця реклами. У прикладі реалізовано через `RewardAdConfig` ScriptableObject, що дозволяє налаштувати нагороди в редакторі Unity.

## Як додати нову гру:

### 1. Створити клас, що наслідується від BaseGameplayController

```csharp
public class MyGameplayController : BaseGameplayController, ITickable
{
    public MyGameplayController(
        SignalBus signalBus,
        GameStateManager gameStateManager,
        ICurrencyManager currencyManager)
        : base(signalBus, gameStateManager, currencyManager)
    {
    }

    protected override void OnStartGame()
    {
        // Ініціалізація гри
    }

    protected override void OnEndGame(bool isWin)
    {
        // Логіка завершення гри
    }
}
```

### 2. Зареєструвати в Installer

У `MainSceneInstaller` додайте binding:

```csharp
Container.Bind<BaseGameplayController>().To<MyGameplayController>().AsSingle().NonLazy();
```

### 3. Додати UI компоненти (опціонально)

Створіть UI компоненти, які підписуються на `GameStateChangedSignal` для відображення стану гри.

## Структура проєкту

```
Assets/Project/
├── Core/                   # Основні системи framework
│   ├── EventBus/           # Сигнали для комунікації
│   ├── Economy/            # Управління валютою
│   ├── GameState/          # Управління станами гри
│   ├── Analytics/          # Аналітика
│   ├── Monetization/       # Монетизація
│   └── Input/              # Абстракція input (touch/mouse)
├── Gameplay/
│   └── Base/               # BaseGameplayController
└── Example/                # Приклади використання
    ├── Scripts/
    │   ├── Economy/        # UI компоненти для економіки
    │   ├── Gameplay/       # Приклади геймплею (Clicker, Timer, Swipe)
    │   ├── UI/             # UI компоненти
    │   └── Installers/     # Zenject installers
    │   └── Helper/         # Utils and Constants
    │   └── Analytics/      # Analytics
    │   └── Monetization/   # Scripts for ads including adapter that help claiming rewards from project  
    └── GameResources/      # Ресурси (ScriptableObjects тощо)
```

## Mobile Considerations

### Object Pooling

### Mechanics
- Використовувати `ScriptableObject` для конфігурацій механіки

## Приклади геймплею

### Clicker
Тап по екрану = +1 монета. Win при досягненні 10 монет.

## Що б покращив з більшим часом:

1. **Unit тести** - додати тести для Economy та Event Bus систем
2. **Editor tools** - створити Editor tool для швидкого setup нової гри
3. **Zenject.IInitializable** - впровадив для рефакторінгу
4. **Currency Animation** - реалізував би анімацію монет
5. **Audio System** - додати core систему звуку
6. **Object Pooling** - додав helper для перевикористання об'єктів
7. **Save System** - впровадив generic рішення для підтримки валют у іграх
8. **Scene Management** - покращити управління сценами з transitions
9. **Error Handling** - додати централізовану систему обробки помилок
10. **Performance Profiling** - інтеграція з Unity Profiler для моніторингу продуктивності
