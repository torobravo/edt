using AutoMapper;
using BookLibraryAPI.Controllers;
using BookLibraryAPI.Data;
using BookLibraryAPI.Dtos;
using BookLibraryAPI.Models;
using BookLibraryAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookLibraryAPI.Tests
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookRepo> _mockBookRepo;
        private readonly Mock<IPatronRepo> _mockPatronRepo;
        private readonly BooksController _booksController;

        public BooksControllerTests()
        {
            _mockBookRepo = new Mock<IBookRepo>();
            _mockPatronRepo = new Mock<IPatronRepo>();

            // auto mapper config
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BooksProfile());
                cfg.AddProfile(new PatronsProfile());
            });

            var mapper = mockMapper.CreateMapper();

            _booksController = new BooksController(_mockBookRepo.Object, _mockPatronRepo.Object, mapper);
        }

        [Fact]
        public void Test1_GetBooks()
        {
            _mockBookRepo.Setup(s => s.GetBooks());

            var books = _booksController.GetBooks();

            Assert.IsType<OkObjectResult>(books.Result);

        }

        [Fact]
        public void GetBook_ReturnsNotFound_WhenBookDoesNotExists()
        {
            _mockBookRepo.Setup(x => x.GetBookById(It.IsAny<int>())).Returns((Book)null);

            var result = _booksController.GetBookById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetBook_ReturnsBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test", Author = "Test", Isbn = "XYZ", Year = 1980 };
            _mockBookRepo.Setup(x => x.GetBookById(book.Id)).Returns(book);

            // Act
            var result = _booksController.GetBookById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BookReadDto>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<BookReadDto>(objectResult.Value);

            Assert.Equal("Test", returnValue.Title);
        }

        [Fact]
        public void CheckoutBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _mockBookRepo.Setup(x => x.GetBookById(It.IsAny<int>())).Returns((Book)null);

            // Act
            var result = _booksController.CheckoutBook(1, 1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CheckoutBook_ReturnsBadRequest_WhenBookIsAlreadyCheckedOut()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test", Author = "Test", Isbn = "XYZ", Year = 1980, PatronId = 1 };
            _mockBookRepo.Setup(x => x.GetBookById(book.Id)).Returns(book);

            var patron = new Patron { Id = 1, FirstName = "FName", LastName = "LName", Email = "test@gmail.com" };
            _mockPatronRepo.Setup(p => p.GetPatronById(patron.Id)).Returns(patron);

            // Act
            var result = _booksController.CheckoutBook(1, 1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CheckinBook_ReturnsBadRequest_WhenBookIsNotCheckedOut()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test", Author = "Test", Isbn = "XYZ", Year = 1980 };
            _mockBookRepo.Setup(x => x.GetBookById(book.Id)).Returns(book);

            // Act
            var result = _booksController.CheckinBook(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void CheckinBook_ReturnsNoContent_WhenBookIsCheckedOut()
        {
            // Arrange
            var patron = new Patron { Id = 1, FirstName = "FName", LastName = "LName", Email = "test@gmail.com" };

            var book = new Book { Id = 1, Title = "Test", Author = "Test", Isbn = "XYZ", Year = 1980, PatronId = 1, Patron = patron };
            _mockBookRepo.Setup(x => x.GetBookById(book.Id)).Returns(book);

            // Act
            var result = _booksController.CheckinBook(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}