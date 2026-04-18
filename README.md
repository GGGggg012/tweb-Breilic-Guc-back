# eAviaSales — ASP.NET Core 8 Web API

Курсовой проект по дисциплине "Технологии веб-разработки".
Система управления продажей авиабилетов.

## Архитектура

Проект построен по многоуровневой архитектуре:

- **eUseControl.Domain** — доменные сущности с бизнес-методами
- **eUseControl.Model** — модели запросов и ответов
- **eUseControl.Business** — бизнес-логика, маппинг
- **eUseControl.DataAccess** — работа с базой данных (EF Core + SQLite)
- **eUseControl.Api** — контроллеры, фильтры, middleware

## Стек технологий

- ASP.NET Core 8
- Entity Framework Core 8 + SQLite
- AutoMapper 13
- JWT Bearer Authentication
- Swagger / OpenAPI

## Запуск

```bash
docker run --rm -v "%cd%:/app" -w /app/eUseControl.Api mcr.microsoft.com/dotnet/sdk:8.0 dotnet run
```

Swagger UI доступен по адресу: `http://localhost:5000/swagger`

## API Endpoints

### Auth
- `POST /api/auth/register` — регистрация
- `POST /api/auth/login` — вход, возвращает JWT токен

### Users (требуется авторизация)
- `GET /api/users` — список пользователей
- `GET /api/users/{id}` — пользователь по ID
- `PUT /api/users/{id}` — обновление данных
- `POST /api/users/change-password` — смена пароля
- `DELETE /api/users/{id}` — деактивация (только Admin)

### Products
- `GET /api/products` — список товаров
- `GET /api/products/{id}` — товар по ID
- `POST /api/products` — создание (только Admin)
- `PUT /api/products/{id}` — обновление (только Admin)
- `DELETE /api/products/{id}` — удаление (только Admin)

### Orders (требуется авторизация)
- `GET /api/orders` — все заказы (только Admin)
- `GET /api/orders/my` — мои заказы
- `POST /api/orders` — создать заказ

## Авторизация

Авторизация на основе JWT. После входа добавить заголовок:
```
Authorization: Bearer <токен>
```
