using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entities;

public class Agendamento
{
    public int Id { get; private set; }
    public DateTime Data { get; private set; }
    public int BarbeiroId { get; private set; }
    public Barbeiro Barbeiro { get; private set; }
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public ICollection<Servico> Servicos { get; private set; }
    public decimal PrecoCobrado { get; private set; }

    private Agendamento()
    {
    }

    private Agendamento(DateTime data, int barbeiroId, int clienteId, IList<Servico> servicos)
    {
        Data = data;
        BarbeiroId = barbeiroId;
        ClienteId = clienteId;
        Servicos = servicos;
        PrecoCobrado = servicos.Sum(s => s.Preco);
    }
    
    public static Resultado<Agendamento> Criar(DateTime data, int barbeiroId, int clienteId, IList<Servico> servicos)
    {
        DateTime hoje = DateTime.UtcNow;
        
        if (data < hoje.AddHours(1))
        {
            return Resultado<Agendamento>.Falha("Data inválida, intervalo mínimo de 1 hora de antecedência");
        }
        
        if (barbeiroId <= 0)
        {
            return Resultado<Agendamento>.Falha("Barbeiro inválido");
        }

        if (clienteId <= 0)
        {
            return Resultado<Agendamento>.Falha("Cliente inválido");
        }
        
        if (servicos == null)
        {
            return Resultado<Agendamento>.Falha("Serviço inválido");
        }
        
        foreach(Servico servico in servicos)
        {
            if (servico.Preco <= 0)
            {
                return Resultado<Agendamento>.Falha("Preço do serviço inválido");
            }
        }
        
        return Resultado<Agendamento>.Ok(new Agendamento(data, barbeiroId, clienteId, servicos));
    }
}