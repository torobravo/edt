# BookLibraryAPI
Implementation of a web API in C# using .NET 8 to manage a small book library.  This system enable basic operations related to books in the system. An in-memory database is used for demonstration purposes, but it can be swapped for a real database without breaking the current implementation.

Software Developer Technical Assessment for Education Development Trust - October 2024

## Setup
Run the following command to clone the project into your local repository
```bash
    git clone https://github.com/torobravo/edt.git
```

Run the project with:
```bash
    dotnet run
```

To run the tests, use the following command:
```bash
    dotnet test
```


## Access the API
You can access the API via Swagger by navigating to http://localhost:5290/swagger/index.html in your browser or Postman to test the API.

#### Example Endpoints
#### Books
* GET /api/books - Retrieve all books.
* GET /api/books/{id} - Retrieve a book by its Id.
* POST /api/books - Add a new book.
* PUT /api/books/{id} - Update an existing book.
* PATCH /api/books/{id} - Partially update an existing book.
* DELETE /api/books/{id} - Delete a book.
* PUT /api/books/{patronId}/checkout/{bookId} - Checkout a book.
* PUT /api/books/checkin/{bookId} - Checkin a book.

#### Patrons
* GET /api/patrons - Retrieve all patrons with their checked-out books.
* GET /api/patrons/{id} - Retrieve a patron and their checked-out books.
* POST /api/patrons - Create a new patron.
