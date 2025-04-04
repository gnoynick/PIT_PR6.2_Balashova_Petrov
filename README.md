# Практическая работа №6: Создание автоматизированных unit-тестов 2 часть
## Цель работы:

провести тестирование разработанных
программных модулей авторизации и регистрации пользователей с
использованием средств автоматизации Microsoft Visual Studio методом
"белого ящика".

## Скриншоты
На скриншоте 1 продемонстрировано содержимое таблицы User из СУБД Microsoft SQL Server.

![image](https://github.com/user-attachments/assets/8422096a-5ef6-4cce-816a-ae9bb05015ef)

Скриншот 1 – Таблица User

На скриншоте 2 продемонстрировано окно "Обозреватель тестов" с результатами выполнения всех тестов.

![image](https://github.com/user-attachments/assets/367e586e-52b1-4a89-8822-ea4af827b753)

Скриншот 2 – Окно "Обозреватель тестов"

На скриншоте 3 продемонстрирована таблица User после выполнения всех тестов.

![image](https://github.com/user-attachments/assets/70227e2d-26f5-41e0-ac0c-f62ec1d0bbcc)

Скриншот 3 – Таблица User после тестов

## Вывод о проведенном тестировании  
   В ходе тестирования были выполнены следующие проверки:
   - тестирование модуля авторизации;
   - тестирование работы капчи;
   - тестирование модуля регистрации.

   Все тесты прошли успешно. Причины успешного выполнения тестов:
   - корректная реализация методов авторизации и регистрации;
   - успешное взаимодействие с базой данных;
   - учет всех пограничных случаев (пустые поля, форматы данных);
   - полное покрытие сценариев (негативные/позитивные);
   - мзолированность тестов (уникальные данные в каждом);
   - правильная генерация и проверка капчи.

## Прикрепленные материалы
   - [**SQL-скрипт базы данных User**](https://github.com/gnoynick/PIT_PR6.2_Balashova_Petrov/blob/master/скрипт%20бд.sql)
   - [**Ссылка на программное решение в СКВ Git**](https://github.com/gnoynick/PIT_PR6.2_Balashova_Petrov/tree/master)
