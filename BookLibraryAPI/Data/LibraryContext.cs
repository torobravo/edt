using BookLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Patron> Patrons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Title1", Author = "Author1", Year = 1980, Isbn = "12345", Price = 10, CreatedAt = DateTime.Now, ModifiedBy = "Henry J" },
                new Book { Id = 2, Title = "Title2", Author = "Author2", Year = 1981, Isbn = "54321", Price = 20, CreatedAt = DateTime.Now, ModifiedBy = "Henry J" }
            );

            modelBuilder.Entity<Patron>().HasData(
                new Patron { Id = 1, Name = "PatronName1", Email = "patron1@email.com" },
                new Patron { Id = 2, Name = "PatronName2", Email = "patron2@email.com" }
            );
        }
    }
}