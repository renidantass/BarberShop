using System.Text.RegularExpressions;
using BarberShop.Domain.Common;

namespace BarberShop.Domain.ValueObjects;

public class Telefone
{
    public string DDI { get; private set; }
    public string DDD { get; private set; }
    public string Numero { get; private set; }

    private Telefone()
    {
    }

    private Telefone(string ddi, string ddd, string numero)
    {
        DDI = ddi;
        DDD = ddd;
        Numero = numero;
    }

    public static Resultado<Telefone> Criar(string ddi, string ddd, string numero)
    {
        ddi = SomenteDigitos(ddi);
        ddd = SomenteDigitos(ddd);
        numero = SomenteDigitos(numero);

        if (ddi.Length < 1 || ddi.Length > 3)
        {
            return Resultado<Telefone>.Falha("DDI inválido");
        }

        if (ddd.Length != 2)
        {
            return Resultado<Telefone>.Falha("DDD inválido");
        }

        if (numero.Length < 8 || numero.Length > 9)
        {
            return Resultado<Telefone>.Falha("Número inválido");
        }

        return Resultado<Telefone>.Ok(new Telefone(ddi, ddd, numero));
    }

    private static string SomenteDigitos(string valor)
    {
        return Regex.Replace(valor ?? string.Empty, @"\D", "");
    }

    public override string ToString()
    {
        return $"+{DDI} ({DDD}) {Numero}";
    }
}