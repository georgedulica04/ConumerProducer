namespace ConsoleApp1;

public class Producer<T>
{
    public async Task Produce(ThreadSafeList<T> threadSafeList, CancellationToken cancellationToken, RandomGenerator<T> randomGenerator)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(250);

            T newNumber = randomGenerator.GetNumber();
            Console.WriteLine($"\tProduced: {newNumber}");
            threadSafeList.Add(newNumber);
        }
    }
}