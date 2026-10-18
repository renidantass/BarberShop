using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entities;

public class Agendamento
{
    public int Id { get; private set; }
    public DateTime IniciaEm { get; private set; }
    public DateTime TerminaEm { get; set; }
    public int BarbeiroId { get; private set; }
    public Barbeiro Barbeiro { get; private set; }
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public ICollection<Servico> Servicos { get; private set; }
    public decimal PrecoCobrado { get; private set; }

    private Agendamento()
    {
    }

    private Agendamento(DateTime inicio, DateTime fim, int barbeiroId, int clienteId, IList<Servico> servicos)
    {
        IniciaEm = inicio;
        TerminaEm = fim;
        BarbeiroId = barbeiroId;
        ClienteId = clienteId;
        Servicos = servicos;
        PrecoCobrado = servicos.Sum(s => s.Preco);
    }
    
    public static Resultado<Agendamento> Criar(DateTime inicio, DateTime fim, int barbeiroId, int clienteId, IList<Servico> servicos)
    {
        DateTime agora = DateTime.UtcNow;
        
        if (inicio < agora.AddHours(1))
        {
            return Resultado<Agendamento>.Falha("Data inválida, intervalo mínimo de 1 hora de antecedência");
        }

        if (fim < inicio)
        {
            return Resultado<Agendamento>.Falha("Data inválida, não pode terminar antes de ter começado");
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
        
        return Resultado<Agendamento>.Ok(new Agendamento(inicio, fim, barbeiroId, clienteId, servicos));
    }
}