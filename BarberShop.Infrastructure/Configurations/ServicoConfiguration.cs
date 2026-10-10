using BarberShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Infrastructure.Configurations
{
    public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Preco)
                .HasPrecision(16, 2)
                .IsRequired();

            builder.Property(s => s.Detalhes)
                .HasMaxLength(500);
        }
    }
}
