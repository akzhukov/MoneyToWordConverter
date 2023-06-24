namespace MoneyToWordConverter.CurrencyCollection;

internal interface ICurrency
{
    string GetWholeCurrencyName(int value);
    string GetDecimalCurrencyName(int value);
}
