# ExpenseTrackerAPI

A RESTful Expense Tracker API built with ASP.NET Core, Entity Framework Core, and Microsoft SQL Server.

This project provides APIs for managing users, expense categories, and personal expenses. It is designed as a practical backend project demonstrating clean API development, database integration, authentication, and common REST API practices.

## 🚀 Features

- User registration and login
- JWT-based authentication
- Create, read, update, and delete expenses
- Expense category management
- User-specific expense management
- Search and filter expenses
- Filter expenses by date range
- Monthly expense summary
- Category-wise expense summary
- Input validation
- Global exception handling
- Swagger / OpenAPI documentation
- Entity Framework Core migrations
- SQL Server database integration

## 🛠️ Technologies

- **C#**
- **.NET / ASP.NET Core Web API**
- **Entity Framework Core**
- **Microsoft SQL Server**
- **LINQ**
- **JWT Authentication**
- **Swagger / OpenAPI**
- **RESTful API**
- **Git / GitHub**

## 📁 Project Structure

```text
ExpenseTrackerAPI
│
├── Controllers
│   ├── AuthController.cs
│   ├── CategoriesController.cs
│   └── ExpensesController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Models
│   ├── User.cs
│   ├── Category.cs
│   └── Expense.cs
│
├── DTOs
│   ├── Auth
│   └── Expenses
│
├── Repositories
│   ├── IExpenseRepository.cs
│   └── ExpenseRepository.cs
│
├── Services
│   ├── IExpenseService.cs
│   └── ExpenseService.cs
│
├── Middleware
│   └── ExceptionMiddleware.cs
│
├── Migrations
│
├── Program.cs
└── appsettings.json
