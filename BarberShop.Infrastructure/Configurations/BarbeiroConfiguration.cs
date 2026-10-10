using BarberShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Infrastructure.Configurations
{
    public class BarbeiroConfiguration : IEntityTypeConfiguration<Barbeiro>
    {
        public void Configure(EntityTypeBuilder<Barbeiro> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.UsuarioId)
                .IsRequired();
        }
    }
}
