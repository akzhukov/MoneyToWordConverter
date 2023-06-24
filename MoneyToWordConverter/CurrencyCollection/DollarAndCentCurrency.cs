using MoneyToWordConverter.NumberConverter;

namespace MoneyToWordConverter.CurrencyCollection;

internal class DollarAndCentCurrency : ICurrency
{
    private readonly Languages _language;

    private readonly IReadOnlyDictionary<Languages, (string Singular, string Plural)> _decimalCurrencyDict = new Dictionary<Languages, (string, string)>
    {
        { Languages.English, ("cent", "cents") }
    };

    private readonly IReadOnlyDictionary<Languages, (string Singular, string Plural)> _wholeCurrencyDict = new Dictionary<Languages, (string, string)>
    {
        { Languages.English, ("dollar", "dollars") }
    };

    public DollarAndCentCurrency(Languages language)
    {
        _language = language;
    }

    public string GetDecimalCurrencyName(int value)
    {
        if (!_decimalCurrencyDict.ContainsKey(_language))
            throw new ArgumentOutOfRangeException($"Currency name for language: {_language} was not found!");
        if (value == 0)
            return string.Empty;
        if (value == 1)
            return _decimalCurrencyDict[_language].Singular;
        return _decimalCurrencyDict[_language].Plural;
    }

    public string GetWholeCurrencyName(int value)
    {
        if (!_wholeCurrencyDict.ContainsKey(_language))
            throw new ArgumentOutOfRangeException($"Currency name for language: {_language} was not found!");
        if (value == 0)
            return string.Empty;
        if (value == 1)
            return _wholeCurrencyDict[_language].Singular;
        return _wholeCurrencyDict[_language].Plural;
    }
}
