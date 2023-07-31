namespace ConsoleApp1;

public class RandomGenerator<T>
{
    private Random rand = new Random();

    private int GenerateRandomInt() => rand.Next(1, 100);

    private double GenerateRandomDouble()
    {
        var randDouble = rand.NextDouble() * 100;
        var roundedValue = Math.Round(randDouble, 2);

        return roundedValue;
    }

    public T GetNumber()
    {
        if (typeof(T) == typeof(int))
        {
            return (T)(object)GenerateRandomInt();
        }
        else
        {
            return (T)(object)GenerateRandomDouble();
        }
    }
}