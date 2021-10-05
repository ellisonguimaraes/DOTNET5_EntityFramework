using BooksProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksProject.Map
{
    public class EditorEntityTypeConfiguration : IEntityTypeConfiguration<Editor>
    {
        public void Configure(EntityTypeBuilder<Editor> builder)
        {
            // Entity Configuration
            builder.ToTable("tbl_editoras");
            builder.HasKey(e => e.Id);

            // Property Configuration
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(35).IsRequired();
        }
    }
}