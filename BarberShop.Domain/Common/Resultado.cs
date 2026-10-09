namespace BarberShop.Domain.Common;

public class Resultado
{
    public bool Sucesso { get; private set; }
    public string? Mensagem { get; private set; }
    protected Resultado(bool sucesso, string? mensagem = "")
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }
    
    public static Resultado Ok(string? mensagem = "")
    {
        return new Resultado(true, mensagem);
    }
    
    public static Resultado Falha(string mensagem)
    {
        return new Resultado(false, mensagem);
    }
}

public class Resultado<T> : Resultado
{
    public T? Retorno { get; private set; }
    private Resultado(bool sucesso, T? retorno, string? mensagem) : base(sucesso, mensagem)
    {
        Retorno = retorno;
    }

    public static Resultado<T> Ok(T retorno, string? mensagem = "")
    {
        return new Resultado<T>(true, retorno, mensagem);
    }
    
    public new static Resultado<T> Falha(string mensagem)
    {
        return new Resultado<T>(false, default, mensagem);
    }
}