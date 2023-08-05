
using DomainModelsLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.ConfigurationEntities
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books").HasKey(x => x.Id);

            builder.Property(x => x.Title).HasColumnType("varchar").HasMaxLength(256).IsRequired();

            builder.HasOne(x=>x.author)
                   .WithMany(x=>x.Books)
                   .HasForeignKey(x=>x.AuthorId).IsRequired();


        }
    }
}
