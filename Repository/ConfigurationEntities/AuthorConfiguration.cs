
using DomainModelsLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.ConfigurationEntities
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors").HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(256).IsRequired();
        }
    }
}
