using BooksProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksProject.Map
{
    public class GenderEntityTypeConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            // Entity Configuration
            builder.ToTable("tbl_generos");
            builder.HasKey(g => g.Id);

            // Property Configuration
            builder.Property(g => g.Id).HasColumnName("id");
            builder.Property(g => g.Name).HasColumnName("name").HasMaxLength(40).IsRequired();
        }
    }
}