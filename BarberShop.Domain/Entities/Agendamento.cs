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
    public int ServicoId { get; private set; }
    public Servico Servico { get; private set; }
    public decimal PrecoCobrado { get; private set; }

    private Agendamento()
    {
    }

    private Agendamento(DateTime data, int barbeiroId, int clienteId, int servicoId, decimal precoCobrado)
    {
        Data = data;
        BarbeiroId = barbeiroId;
        ClienteId = clienteId;
        ServicoId = servicoId;
        PrecoCobrado = precoCobrado;
    }
    
    public static Resultado<Agendamento> Criar(DateTime data, int barbeiroId, int clienteId, Servico? servico)
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
        
        if (servico == null)
        {
            return Resultado<Agendamento>.Falha("Serviço inválido");
        }
        
        if (servico.Preco <= 0)
        {
            return Resultado<Agendamento>.Falha("Preço inválido");
        }
        
        return Resultado<Agendamento>.Ok(new Agendamento(data, barbeiroId, clienteId, servico.Id, servico.Preco));
    }
}