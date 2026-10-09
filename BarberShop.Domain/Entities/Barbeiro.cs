using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entities;

public class Barbeiro
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string UsuarioId { get; private set; }
    public ICollection<Agendamento> Agendamentos { get; private set; } = new List<Agendamento>();

    private Barbeiro()
    {
        
    }

    private Barbeiro(string nome, string usuarioId)
    {
        Nome = nome;
        UsuarioId = usuarioId;
    }

    public static Resultado<Barbeiro> Criar(string nome, string usuarioId)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return Resultado<Barbeiro>.Falha("Nome inválido");
        }

        if (string.IsNullOrWhiteSpace(usuarioId))
        {
            return Resultado<Barbeiro>.Falha("Usuário inválido");
        }

        return Resultado<Barbeiro>.Ok(new Barbeiro(nome.Trim(), usuarioId));
    }
}