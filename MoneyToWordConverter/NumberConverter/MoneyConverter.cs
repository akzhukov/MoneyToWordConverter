using MoneyToWordConverter.CurrencyCollection;
using MoneyToWordConverter.LanguagePacks;
using System.Globalization;
using System.Text;

namespace MoneyToWordConverter.NumberConverter;

public class MoneyConverter
{
    private readonly ICurrency _currency;
    private readonly INumberToWordsConverter _numberToWordsConverter;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="language">Language to convert</param>
    /// <param name="currency">money currency</param>
    public MoneyConverter(Languages language, Currencies currency)
    {
        _currency = GetCurrency(currency, language);
        _numberToWordsConverter = GetNumberConverter(language);
    }

    /// <summary>
    /// The value will rounded to two decimal places.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string Convert(string value)
    {
        try
        {
            var decimalValue = decimal.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
            return Convert(decimalValue);
        }
        catch (Exception)
        {
            throw new InvalidDataException($"Failed to convert {value} to decimal");
        }
    }

    /// <summary>
    /// The value will rounded to two decimal places.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string Convert(decimal value)
    {
        value = Math.Round(value, 2);
        if (!IsValidNumberRange(value))
        {
            return "Invalid number range. Number must be between 0 and 2_000_000_000";
        }

        StringBuilder result = new();
        int wholePart = (int)value;
        int decimalsPart = (int)((value - wholePart) * 100);

        if (wholePart > 0)
        {
            result.Append(_numberToWordsConverter.ConvertNumber(wholePart));
            result.Append(' ');
            result.Append(_currency.GetWholeCurrencyName(wholePart).ToUpper());
        }

        if (decimalsPart > 0)
        {
            if (wholePart > 0)
                result.Append(" AND ");
            result.Append(_numberToWordsConverter.ConvertNumber(decimalsPart));
            result.Append(' ');
            result.Append(_currency.GetDecimalCurrencyName(decimalsPart).ToUpper());
        }

        return result.ToString();
    }

    private bool IsValidNumberRange(decimal value)
    {
        return value >= 0 && value <= 2_000_000_000;
    }

    private INumberToWordsConverter GetNumberConverter(Languages language) => language switch
    {
        Languages.English => new NumberToEnglishWordsConverter(),
        _ => new NumberToEnglishWordsConverter(),
    };

    private ICurrency GetCurrency(Currencies currency, Languages language) => currency switch
    {
        Currencies.DollarAndCents => new DollarAndCentCurrency(language),
        _ => new DollarAndCentCurrency(language),
    };
}