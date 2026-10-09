using BarberShop.Domain.Common;
using BarberShop.Domain.Enums;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Domain.Entities;

public class Cliente
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public Genero Genero { get; private set; }
    public Telefone Telefone { get; private set; }
    public string UsuarioId { get; private set; }
    public ICollection<Agendamento> Agendamentos { get; private set; } = new List<Agendamento>();
    
    private  Cliente()
    {
    }
    
    private Cliente(string nome, DateTime dataNascimento, Genero genero, Telefone telefone, string usuarioId)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Genero = genero;
        Telefone = telefone;
        UsuarioId = usuarioId;
    }
    
    public static Resultado<Cliente> Criar(string nome, DateTime dataNascimento, Genero genero, Telefone? telefone, string usuarioId)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return Resultado<Cliente>.Falha("Nome inválido");
        }

        if (dataNascimento > DateTime.UtcNow)
        {
            return Resultado<Cliente>.Falha("Data de nascimento inválida");
        }

        if (!Enum.IsDefined(genero))
        {
            return Resultado<Cliente>.Falha("Gênero inválido");
        }

        if (telefone == null)
        {
            return Resultado<Cliente>.Falha("Telefone inválido");
        }

        if (string.IsNullOrWhiteSpace(usuarioId))
        {
            return Resultado<Cliente>.Falha("Usuário inválido");
        }

        return Resultado<Cliente>.Ok(new Cliente(nome.Trim(), dataNascimento, genero, telefone, usuarioId));
    }
}