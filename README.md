## About the project

This API, developed using **.NET 8.0**, adopts the principles of **Domain-Driven Design (DDD)** to offer a structured and effective soluction for managing a barbarshop's billing. The main objective is to allow owners to record their billings, using information such as the barber's name, client's name, the service provided, payment type and the date the service was performed, with the data securely stored in **MySQL database**.

The **API's** architecture is based on **REST**, utilizing standard HTTp methods for efficient and simplified communication, Furthermore, it is complemented by **Swagger** documentation, which provides an interactive graphical interface for developers to easily explore and test the endpoints.

Among the NuGet packages used, **AutoMapper** is responsible for mapping between domain and request/response objects, reducing the need for repetitive and manual code. **FluentAssertion** is used in unit tests to make verifications more readable, helping to write clear and understandable tests. For validations, **FluentValidation** is used to implement validation rules simply and intuitively in request classes, keeping the code clean and easy to maintain. Finally, **EntityFramework** acts as an ORM (Object-Relational Mapper) that simplifies interactions with the database, alloowing the use of .NET objects to directly manipulate data without the need to deal with SQL queries.

![hero-image]

### Features

 - **Domain-Drive Design (DDD)**: Modeluar structure that facilitates understanding and maintenance of the application's domain.
 - **Unit Test**: Comprehensive tests with FluentAssertions to ensure functionality and quality.
 - **Report Generation**: Ability to export detailed reports to PDF and Excel, offering a visual and effective analysis of expenses.
 - **RESTful API with Swagger Documentation**: Documented interface that facilitates integration and testing by developers.


### Construido com

![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-swagger]


## Getting Started

To get a local copy up and running, follow these simple steps.

### Requirements

- Visual Studio version 2022+ or Visual Studio Code
- Windowns 10+ or Linux/macOs with [,NET SDK][dot-net-sdk] installed
- MySQL Server

### Instalation

1. Clone the repository:
    ```sh
    gitclone https://github.com/MateusFernanndo/BarberBoss.git
    ```

2. Fill in the information in the `appsettings.Development.json` file.

3. Run the Api and enjoy your test :)


<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- Image -->
[hero-image]: images/heroimage.png

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=flat-square
[badge-windows]: https://img.shields.io/badge/Windowns-blue?style=flat-square&logo=Windows
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-purple?style=flat-square
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=flat-square
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=flat-square
