using BookLibraryAPI.Data;
using BookLibraryAPI.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace BookLibraryAPI.Tests
{
    public class BooksControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>> 
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _applicationFactory;

        public BooksControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _applicationFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<LibraryContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });
                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                    dbContext.Database.EnsureCreated();
                    
                });
            });

            _httpClient = _applicationFactory.CreateClient();
        }

        [Fact]
        public async Task GetBooks_ReturnsListOfBooks()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/books");

            // Assert
            response.EnsureSuccessStatusCode();

            var books = await response.Content.ReadFromJsonAsync<List<BookReadDto>>();
            Assert.NotNull(books);
            Assert.Equal(2, books.Count);
        }

        [Fact]
        public async Task GetBook_ReturnsBook_WhenBookExists()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/books/1");

            // Assert
            response.EnsureSuccessStatusCode();

            var book = await response.Content.ReadFromJsonAsync<BookReadDto>();
            Assert.NotNull(book);
            Assert.Equal("Title1", book.Title);
        }

        [Fact]
        public async Task CheckoutBook_ReturnsNoContent_WhenBookIsCheckedOut()
        {
            // Act
            var response = await _httpClient.PutAsync("/api/books/1/checkout/1", null);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task CheckinBook_ReturnsNoContent_WhenBookIsCheckedIn()
        {
            // Checkout
            await _httpClient.PutAsync("/api/books/1/checkout/1", null);

            // Act
            var response = await _httpClient.PutAsync("/api/books/checkin/1", null);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
