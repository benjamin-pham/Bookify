namespace Bookify.Domain.Shared;
public record Currency
{
    public static readonly Currency None = new Currency("");
    public static readonly Currency Usd = new Currency("USD");
    public static readonly Currency Eur = new Currency("EUR");
    private Currency(string code) => Code = code;
    public string Code { get; set; }
    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(x => x.Code == code) ??
            throw new ApplicationException("The currency code is invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All = new[]
    {
        Usd,
        Eur
    };

}