# TelegramMathBot 


## Участиники
Гильмутдинов Даниил  
Кузьмин Роман  
Аткишкин Владислав  
Фрицлер Виктор  

## Проблема, которую решает проект: 
Студент хочет решить математическую задачу, но вычисления слишком затратные. При этом у него установлен Telegram. Для этого он заходит в переписку с нашим ботом, выбирает режим работы(вычисления примеров, решение интегралов, построение графиков и тд.) и вбивает свой пример.

## Описание основных компонент системы
- Telegram Бот  
Взаимодествие с пользователем:  
Принимает запросы клиента в виде сообщений и передает их на обработку глобальному диспетчеру, затем получает результат от него и отправляет его клиенту.
- Глобальный диспетчер  
Определяет какой функцией воспользовался клиент (решение задач/справка по боту/консультация) и отправляет соответствующему диспетчеру. Получает от каждого диспетчера результат и возвращает его.
- Конкретный диспетчер  
интерфейс IDispatcher<T> с методом GetCommand<T>  
Если было выбрано решение задач, то определяет какой тип задач был выбран и передает соответствующему парсеру.  
Возвращает результат в виде команды-запроса для Telegram - бота (например /sendPhoto..., /sendMessage)
- Парсер  
Получает данные в текстовом формате, парсит, передает в метод класса-решателя.
- Класс-решатель (получает данные и возвращает результат - класс с одним методом)  
С generic - параметром
Получает данные, решает задачу и возвращает результат.

## Точки расширения
1) Типы расширяемых задач
- Численное решение по формулам (размещения, факториал, сочетания и тд.)
- Отрисовка графика функции одной переменной
- Дифференцирование сложных функций
- Справка по каждому типу задач
- Можно добавить класс - решатель для каждой новой задачи
- Для каждой задачи необходим свой парсер, с расширением задач расширяется парсер
- Для каждой задачи добавляется новый интерфейс взаимодействия - сценарий пользователя (например, разные кнопки, последовательность требуемых действий от пользователя для каждого типа задач)

2) Многофункциональность бота
- Возможность добавить консультацию в чате (доп. функция)
- Решение задач математических
- Bug report

3) Генерация картинок в разном формате
- PNG
- JPG
- SVG
