# CashMachine

### минимальные требования : 
.NET Core 2.1, SqlServer

### инструкция по установке
1. Распаковать архив (клонировать репозиторий с Github)
2. Изменить connectionstring в файле ApplicationContext.cs на connectionstring для сервера вашей БД
3. В консоли: ``` dotnet ef database update ``` эта команда применяет миграции
4. Затем: ``` dotnet run ``` эта команда запускает приложение

При создании базы данных создается счет, на котором 80000, и ячейки банкомата: 5000 купюр 25 шт, 1000 купюр 50 шт, 200 купюр 50 шт, 100 купюр 100 шт.

Логи сохраняются в папку c:\temp. Папку можно изменить в файле nlog.config

### функционал:
1. Чтобы узнать состояние счета, сделать запрос GET с URL /api/atm.
 Ответ будет в формате json (например {"accountId":1,"amount":9989100})
2. Чтобы снять деньги со счета, сделать запрос POST с URL /api/atm/getcash.
 В теле запроса json вида {"accountId":1,"amount":3500} где amount - снимаемая сумма.
 Ответ вида 
 {
    "money": [
        {
            "id": 0,
            "value": 5000,
            "quantity": 2
        },
        {
            "id": 0,
            "value": 1000,
            "quantity": 2
        }
    ]
}
Где Value - номинал купюры,  quantity - количество купюр данного номинала.

3. Для пополнения счета, сделать запрос POST с URL /api/atm/refillbalance.
В теле запроса json {"accountId":1,"amount":3500}, где amount - сумма для пополнения баланса.
Отввет вида {
    "accountId": 1,
    "amount": 9989200
}
С указанием баланса, после пополнения счета.
