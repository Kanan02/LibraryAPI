# Library API

This is Library api with 3 models (Author,Genre and Book) and 2 controllers (BooksController and AuthController).
This project can be extended (AuthorController and GenreController can be added) 

## Technologies

This project is built using **CODE FIRST** approach so it better to firstly remove Migrations folder
and rerun:
### `Add-Migration Initial`

then run the project.


Except default technologies such as **.NET core**, **EF core**, **MSSQL** other technologies were used:

- **Lazy Loading** 

- **AutoMapper** 

## Usage

When the project is running you can do a **POST** request for registration to Authorize and use BooksController.
### `/api/Auth/Register`

Then you can login using your credentials:
### `/api/Auth/Login`

When you will get token just put it in **Authorize** section of SwaggerUI like:

### `Bearer your_token`

Now you can call requests from Swagger.


Here some other projects for you to check:

[https://github.com/Kanan02/ToDoListWebApi](https://github.com/Kanan02/ToDoListWebApi)  

[https://github.com/Kanan02/RabbitMQUserWebapi](https://github.com/Kanan02/RabbitMQUserWebapi)  

[https://github.com/Kanan02/MyMonefyApp_WPF_MVVM](https://github.com/Kanan02/MyMonefyApp_WPF_MVVM)  

