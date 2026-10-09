using BarberShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Infrastructure.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired();

        builder.Property(c => c.UsuarioId)
            .IsRequired();

        builder.Property(c => c.DataNascimento)
            .IsRequired();

        builder.Property(c => c.Genero)
            .HasConversion<int>()
            .IsRequired();

        builder.OwnsOne(c => c.Telefone, telefone =>
        {
            telefone.Property(t => t.DDI)
                .HasMaxLength(3)
                .IsRequired();

            telefone.Property(t => t.DDD)
                .HasMaxLength(2)
                .IsRequired();

            telefone.Property(t => t.Numero)
                .HasMaxLength(9)
                .IsRequired();
        });

        builder.Navigation(c => c.Telefone)
            .IsRequired();
    }
}
