using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entities;

public class Servico
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public string? Detalhes { get; private set; }
    
    private Servico()
    {
        
    }

    private Servico(string nome, decimal preco, string? detalhes)
    {
        Nome = nome;
        Preco = preco;
        Detalhes = detalhes;
    }

    public static Resultado<Servico> Criar(string nome, decimal preco, string? detalhes)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return Resultado<Servico>.Falha("Nome inválido");
        }
        
        if (preco <= Decimal.Zero)
        {
            return Resultado<Servico>.Falha("Preço inválido");
        }

        detalhes = string.IsNullOrWhiteSpace(detalhes) ? null : detalhes.Trim();

        if (detalhes?.Length > 500)
        {
            return Resultado<Servico>.Falha("Detalhes muito longo");
        }

        return Resultado<Servico>.Ok(new Servico(nome.Trim(), preco, detalhes));
    }
}