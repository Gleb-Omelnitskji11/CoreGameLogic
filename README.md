# Framework Architecture

## Як працює:
- **Event Bus**: Framework використовує **EventBus** та Zenject для комунікації між системами. Це дозволяє слабко зв'язану архітектуру, де компоненти не знають один про одного безпосередньо. Використовую StaticContext, SceneContext.
- **Economy**: **CurrencyManager** зберігає усю валюту в грі. Підтримує зміну показника валюти через `AddCurrencies()` та `SpendCurrency()`, додавання унікальної валюти через enum `CurrencyType`, перевірку можливості витрати через `CanSpend()`, та автоматичне збереження в `ICurrencyStorage`.
- **Game State**: **GameStateManager** керує життєвим циклом механіки. Кожна зміна стану генерує `GameStateChangedSignal` для інших систем.

## Як додати нову гру:
1. Створи клас, що наслідується від BaseGameplayController.
2. Створи ScriptableObject для game options.
3. Реалізуй інсталлер по аналогії з Example.

## Mobile considerations:
1. Обрати 1 ось орієнтації.
2. Додати helper для перевикористання об'єктів
3. Ставити на паузу при втраті фокусу

## Що б покращив з більшим часом:
1. **Unit тести** - додати тести для Economy та Event Bus систем
2. **Editor tools** - створити Editor tool для швидкого setup нової гри
3. **Zenject.IInitializable** - впровадити для рефакторінгу
4. **Currency Animation** - реалізувати анімацію переміщення монет в каунтер
5. **Audio System** - додати core систему звуку
6. **Save System** - впровадив шифроване рішення для збережень
7. **Scene Management** - покращити управління сценами з transitions
8. **Error Handling** - додати централізовану систему обробки помилок
9. **Performance Profiling** - інтеграція з Unity Profiler для моніторингу продуктивності

### Cheat
У проекті реалізовани чити **RewardButton** **SpendButton** які дозволяють витрачити монети та брати нагороду за просмотр реклами, каутер монет змінюється з часом необхідної для анімації. 
