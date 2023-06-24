using System.Text;

namespace MoneyToWordConverter.LanguagePacks;

internal class NumberToEnglishWordsConverter : INumberToWordsConverter
{
    public string ConvertNumber(int number)
    {
        StringBuilder result = new();

        var thousands = SplitNumberByThousands(number);
        int order = thousands.Length;
        foreach (var currentThousand in thousands)
        {
            if (currentThousand == 0)
                continue;
            if (currentThousand < 100)
                ConvertNumberLessThan100(currentThousand, result);
            else
            {
                result.Append(ConvertDigit(currentThousand / 100));
                result.Append(" hundred");
                if (currentThousand % 100 != 0)
                {
                    result.Append(" and ");
                    ConvertNumberLessThan100(currentThousand % 100, result);
                }
            }
            result.Append(' ');
            result.Append(ConvertThousandsOrder(order--));
            result.Append(", ");
        }

        return result.ToString().Trim(' ').Trim(',').Trim(' ');
    }

    private int[] SplitNumberByThousands(int number)
    {
        List<int> result = new();

        int currentThousand = number % 1000;

        while (number > 0)
        {
            result.Add(currentThousand);
            number /= 1000;
            currentThousand = number % 1000;
        }

        return result.Reverse<int>().ToArray();
    }

    private void ConvertNumberLessThan100(int number, StringBuilder stringBuilder)
    {
        if (number < 10)
            stringBuilder.Append(ConvertDigit(number));
        else if (number < 20)
            stringBuilder.Append(ConvertNumberBetween10and19(number));
        else if (number < 100)
        {
            stringBuilder.Append(ConvertTens(number / 10));
            if (number % 10 != 0)
            {
                stringBuilder.Append(' ');
                stringBuilder.Append(ConvertDigit(number % 10));
            }
        }
    }

    private string ConvertDigit(int digit) => digit switch
    {
        0 => "",
        1 => "one",
        2 => "two",
        3 => "three",
        4 => "four",
        5 => "five",
        6 => "six",
        7 => "seven",
        8 => "eight",
        9 => "nine",
        _ => throw new ArgumentOutOfRangeException(nameof(digit)),
    };

    private string ConvertNumberBetween10and19(int number) => number switch
    {
        10 => "ten",
        11 => "eleven",
        12 => "twelve",
        13 => "thirteen",
        14 => "fourteen",
        15 => "fifteen",
        16 => "sixteen",
        17 => "seventeen",
        18 => "eighteen",
        19 => "nineteen",
        _ => throw new ArgumentOutOfRangeException(nameof(number)),
    };

    private string ConvertTens(int number) => number switch
    {
        2 => "twenty",
        3 => "thirty",
        4 => "fourty",
        5 => "fifty",
        6 => "sixty",
        7 => "seventy",
        8 => "eighty",
        9 => "ninety",
        _ => throw new ArgumentOutOfRangeException(nameof(number)),
    };

    private string ConvertThousandsOrder(int order) => order switch
    {
        1 => "",
        2 => "thousand",
        3 => "million",
        4 => "billion",
        _ => throw new ArgumentOutOfRangeException(nameof(order)),
    };
}
