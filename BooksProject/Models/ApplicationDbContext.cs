using BooksProject.Map;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Editor> Editoras { get; set; }
        public DbSet<Author> Autores { get; set; }
        public DbSet<Gender> Generos { get; set; }
        public DbSet<Book> Livros { get; set; }
        public DbSet<Identifier> Identificadores { get; set; }
        public DbSet<AuthorBook> AutorLivro { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping
            new AuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<Author>());
            new EditorEntityTypeConfiguration().Configure(modelBuilder.Entity<Editor>());
            new GenderEntityTypeConfiguration().Configure(modelBuilder.Entity<Gender>());
            new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
            new IdentifierEntityTypeConfiguration().Configure(modelBuilder.Entity<Identifier>());
            new AuthorBookEntityTypeConfiguration().Configure(modelBuilder.Entity<AuthorBook>());
        }
    }
}