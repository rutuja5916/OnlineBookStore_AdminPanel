# Online Book Store – Admin Panel

This is an ASP.NET MVC Core web application developed as part of a developer assignment for creating an **Online Book Store Admin Panel**. It allows authenticated administrators to manage books, categories, and view order listings.

The project is built using **ASP.NET MVC Core 5+**, **Entity Framework (Code First)**, and **SQL Server**, with **Bootstrap 5** for basic UI styling.

---

## 🚀 Technologies Used

* ASP.NET Core MVC (.NET 8.0)
* C#
* Entity Framework Core (Code First)
* SQL Server
* Bootstrap 5
* Javascript
* Custom Image Upload Service

### 🔐 User Authentication

* Login functionality using **ASP.NET Identity** for secure user authentication and role management.

### 📚 Books Management

* List all books in both **Card View** and **List View** modes (toggle available).
* Add, edit, delete, and clone books.
* Book fields: Title, Author, ISBN, Price, Category, and Cover image.
* Image upload support with preview.

### 🗂️ Category Management

* List, add, edit, and delete categories.
* Categories that are associated with existing books cannot be deleted or edited to maintain data integrity.

### 📦 Orders (Read-Only)

* Dummy data displayed in a simple order listing view.

### 🔍 Optional Enhancements

* Book search and filter functionality.
* Client-side validation (using Bootstrap/jQuery).
* Enhanced UI/UX with preview of uploaded images.
* Fully responsive design – optimized for desktops, tablets, and mobile devices. 

---

## 🏗️ Project Structure

The solution is divided into the following projects for separation of concerns:

* **Sanmol_BookStore_Models**
  Contains the DbContext/model classes used across the application.

* **Sanmol_BookStore_Services**
  Contains business logic, service interfaces, and implementations including:
  - Generic Repository pattern for CRUD operations with Dependency Injection (DI).
  - `ImageUploadService` – a reusable service for uploading and managing images.

* **Sanmol_BookStore**
  The main ASP.NET Core MVC project that acts as the frontend. It contains all Controllers, Views, and manages interactions between the UI and backend services.

---

## ⚙️ Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022 or later
- SQL Server Management Studio (SSMS) 19 or later

---
🔑 Default Admin Login
Use the following credentials to log in to the Admin Panel after setup:

📧 Email: admin@bookstore.com  
🔒 Password: Admin@123

---
## 🛠️ Setup Instructions

1. Download the Project
Go to the GitHub repository link below and click "Code" > "Download ZIP" to get all files, including the database script and README:
🔗 https://github.com/rutuja5916/OnlineBookStore_AdminPanel.git
   ```
2. Extract the ZIP File
Unzip the downloaded file to your preferred location.
3. Open the Solution
Open the .sln file using Visual Studio 2022 or later.
4.Edit the Connection String
In appsettings.json, update the connection string to point to your local SQL Server instance.
Here's a sample:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=SanmolBookStore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
🔧 Replace YOUR_SERVER_NAME with your local SQL Server instance name.
If using SQL authentication, you can use this format:

"Server=YOUR_SERVER_NAME;Database=SanmolBookStore;User Id=sa;Password=your_password;Trusted_Connection=True;TrustServerCertificate=True"
6. Run the project (`F5` or `Ctrl+F5`).

---

## 🧪 Database Setup

- Create a new database named **SanmolBookStore** in SQL Server.
- Ensure SQL Server is running.
- Execute the provided SQL Script to set up the database schema and data.
---


## 🙋‍♂️ Contact

For any inquiries or issues, contact Rutuja Bodke at rutujabodke17@gmail.com 

