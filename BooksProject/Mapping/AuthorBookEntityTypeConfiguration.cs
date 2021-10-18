using BooksProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksProject.Mapping
{
    public class AuthorBookEntityTypeConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            // Entity Configuration
            builder.ToTable("tbl_autores_livros");
            builder.Ignore(ab => ab.Id);
            builder.HasKey(ab => new { ab.BookId, ab.AuthorId });

            // Property Configuration
            builder.Property(ab => ab.BookId).HasColumnName("book_id").IsRequired();
            builder.Property(ab => ab.AuthorId).HasColumnName("author_id").IsRequired();

            // Relationship
            builder.HasOne<Book>(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookId);

            builder.HasOne<Author>(ab => ab.Author)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId);
        }
    }
}