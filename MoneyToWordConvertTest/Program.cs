using MoneyToWordConverter.NumberConverter;

var converter = new MoneyConverter(Languages.English, Currencies.DollarAndCents);

while (true)
{
    Console.Write("Enter the number: ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        break;
    }
    try
    {
        Console.WriteLine(converter.Convert(input));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

IList<string> strings = new List<string>();
IList<object> objects = strings;

public class Singleton
{
    private static Singleton _instance;

    private object
    private Singleton() { }

    public static Singleton GetInstance()
    {
        lock (_locker)
        {
            if (_instance is null)
                _instance = new Singleton();
            return _instance;
        }
    }
}
//object i = 2;
//IList
//void fun(ref MoneyConverter mc)
//{
//    mc = new MoneyConverter();
//}

//user id name
//order orderId userId

 $@"SELECT Name
    FROM user u
        JOIN order o ON u.id= o.userId
    GROUP BY
    u.ID, u.Name
HAVING COUNT(*) > 1"