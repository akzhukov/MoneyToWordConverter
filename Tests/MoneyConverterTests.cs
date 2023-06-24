using MoneyToWordConverter.NumberConverter;
using Xunit;

namespace Tests;

public class MoneyConverterTests
{
    private readonly MoneyConverter _moneyConverter = new(Languages.English, Currencies.DollarAndCents);

    [Fact]
    public void NormalMoneyConverts()
    {
        Assert.Equal("one million, three hundred and fifty seven thousand, two hundred and fifty six DOLLARS AND thirty two CENTS",
            _moneyConverter.Convert(1357256.32m));
    }

    [Fact]
    public void EdgeValues()
    {
        Assert.Equal("", _moneyConverter.Convert(0));
        Assert.Equal("one DOLLAR", _moneyConverter.Convert(1));
        Assert.Equal("two billion DOLLARS", _moneyConverter.Convert(2_000_000_000));
        Assert.Equal("Invalid number range. Number must be between 0 and 2_000_000_000", _moneyConverter.Convert(2_000_000_001));
    }

    [Fact]
    public void CharCases()
    {
        Assert.Equal("one DOLLAR", _moneyConverter.Convert(1));
    }

    [Fact]
    public void PluralAndSingularNumbers()
    {
        Assert.Equal("one DOLLAR", _moneyConverter.Convert(1));
        Assert.Equal("two DOLLARS", _moneyConverter.Convert(2));
        Assert.Equal("one CENT", _moneyConverter.Convert(0.01m));
        Assert.Equal("two CENTS", _moneyConverter.Convert(0.02m));
    }
}
