using BooksProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksProject.Map
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Entity Configuration
            builder.ToTable("tbl_livros");
            builder.HasKey(b => b.Id);

            // Property Configuration
            builder.Property(b => b.Id).HasColumnName("id");

            builder.Property(b => b.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(b => b.Price).HasColumnName("price").HasPrecision(17, 2);
            builder.Property(b => b.PubDate).HasColumnName("pub_date").IsRequired();

            builder.Property(b => b.IdentifierId).HasColumnName("identifier_id").IsRequired();
            builder.Property(b => b.EditorId).HasColumnName("editor_id").IsRequired();
            builder.Property(b => b.GenderId).HasColumnName("gender_id").IsRequired();

            // Relationship
            builder.HasOne<Editor>(b => b.Editor)
                .WithMany(e => e.Books)
                .HasForeignKey(b => b.EditorId);

            builder.HasOne<Gender>(b => b.Gender)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenderId);
        }
    }
}