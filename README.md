Prototype - Green Agri Hub 

Features
- User Authentication: Login feature with role-based access
  
Employee Capabilities
- Add new farmers to the application
- View products added by farmers
- Filter products based on the category and date

Farmer Capabilities
- Add products to the marketplace
- View the marketplace
- View personal product listings
  
Prerequisites to run the project
- Visual Studio 2022
- .NET 6.0 or later
- SQLite
- Git

Technolgy Used
- ASP.NET MVC (Visual Studio 2022)
- SQLite Database
- Entity Framework Core
- C#

Installation
1. Clone the repository from Github
   [https://github.com/ta-mac/GreenAgriHub](https://github.com/ta-mac/GreenAgriHub)
2. Open the solution file (.sln) in Visual Studio 2022
3. The SQLite database file is within the project folder so no setup is required.
4. Build the solution: Navigate to Build in the menu and select Build Solution
   Build > Build Solution
5. Run the application

NuGet Packages
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Tools

###Farmer Login
-FarmerName = "Thabo",
-FarmerSurname = "Nkosi",
-FarmerPhone = "0612345678",
-FarmerEmail = "tnkosi@gmail.com",
-FarmerPassword = "nkosi123"

-FarmerName = "Nathi",
-FarmerSurname = "Zondo",
-FarmerPhone = "0712345678",
-FarmerEmail = "nzondo@gmail.com",
-FarmerPassword = "zondo123"

###Employee Login
-EmployeeName = "Bob",
-EmployeePassword = "bob20",
-EmployeeEmail = "bob@gmail.com"

EmployeeName = "Jack",
EmployeePassword = "jack10",
EmployeeEmail = "jack@gmail.com"

## Using the Application as Employee
**1. Login**
   - After running the project navigate to the login
   - Enter the required Employee details given above
   - The system will redirect you to the Employee Dashboard
   
**2. Managing Farmers**
   - On the dashboard you can navigate to the page to add new farmers page
   - Enter the required fields with the farmers details and click add farmer
   - The farmers information will be saved to the database
   
**3. Viewing Products**
   - Using either the navigation bar at the to or going back to the dashboard you can go to the page to view added products
   - On the page you are able to see all products that have been added by farmers or filter the products
   
**4. Logout**
   - There is also a logout option on the right hand side of the navigation bar
     
## Using the Application as a Farmer
**1. Login**
   - After running the project navigate to the login
   - Enter the seeded farmer details given above or use the details you added as employee
   - You will then be redirected to the Farmer Dashboard
   
**2. Adding Products**
   - Navigate to the add products page
   - Enter all the required fields and click add product.
   - The product should then save to the database and display in the market place
   
**3. Viewing products**
   - From the dashboard click on My products
   - You will then see the products you have added
   
**4. Marketplace**
   - If you navigate to the marketplace you will be able to see all products from diffrent accounts.
   
**5. Logout**
   - There is also a logout option on the right hand side of the navigation bar

## Refrences
- Role Based Authorization: https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-9.0
                            https://www.c-sharpcorner.com/article/role-based-authentication-in-asp-net-mvc/
- Pro C# 10 with .NET6 Foundational Principles and Practices in Programming 11th Edition Andrew Troelsen and Phil Japikse
- SQLite DB: IIE Programming 3A Module Manual 2025 (First Edition: 2018) and Lecturer Resources
