using BooksProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksProject.Mapping
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            // Entity Configuration
            builder.ToTable("tbl_autores");
            builder.HasKey(a => a.Id);
            
            // Property Configuration
            builder.Property(a => a.Id).HasColumnName("id");
            builder.Property(a => a.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
            builder.Property(a => a.LastName).HasColumnName("last_name").HasMaxLength(40).IsRequired();
            builder.Property(a => a.BirthDate).HasColumnName("birth_date").IsRequired();
        }
    }
}