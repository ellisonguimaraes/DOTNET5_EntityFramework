using BooksProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksProject.Map
{
    public class IdentifierEntityTypeConfiguration : IEntityTypeConfiguration<Identifier>
    {
        public void Configure(EntityTypeBuilder<Identifier> builder)
        {
            // Entity Configuration
            builder.ToTable("tbl_identificadores");
            builder.HasKey(i => i.Id);

            // Property Configuration
            builder.Property(i => i.Id).HasColumnName("id").IsRequired();
            builder.Property(i => i.IdentifierType).HasColumnName("ident_type").HasPrecision(1).IsRequired();
            builder.Property(i => i.IdentifierNumber).HasColumnName("identifier").IsRequired();

            // Relationship
            builder.HasOne<Book>(i => i.Book)
                .WithOne(b => b.Identifier)
                .HasForeignKey<Book>(b => b.IdentifierId);
        }
    }
}