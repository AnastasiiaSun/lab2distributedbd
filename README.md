# Лабораторна робота: Створення сервісу з розподіленою базою даних (SQL та MongoDB)

## Опис
Дана лабораторна робота зосереджується на створенні сервісу з розподіленою базою даних, що використовує реляційну базу SQL та нереляційну MongoDB для зберігання та управління інформацією про студентів і курси. Студенти можуть бути пов’язані з курсами через таблиці, що описують відношення "багато до багатьох", де кожен студент може брати участь у кількох курсах, а кожен курс може мати кількох студентів. Також до кожного курсу прив'язаний викладач відношенням "один до багатьох": один викладач може викладати багато курсів, але курс викладає лише один викладач.

Проект реалізує два шари:
- **Web API** на основі ASP.NET Core для взаємодії з користувачем;
- **Бібліотека доступу до даних (DAL)** для обробки запитів до баз даних.

Web API дозволяє створювати, читати, оновлювати та видаляти інформацію про студентів, курси, а також синхронізує дані між SQL та MongoDB за допомогою транзакцій для забезпечення цілісності даних.

## Функціональні можливості
- **SQL база даних:** використовується для зберігання структурованих даних про студентів, курси та викладачів.
- **MongoDB база даних:** використовується для зберігання даних про студентів у NoSQL форматі.
- **Синхронізаційний сервіс:** забезпечує копіювання та актуалізацію даних між SQL та MongoDB.
  
