using ConsoleApp1;

public class Program
{
    public static async Task Main()
    {
        const int numberOfProducers = 2;
        const int numberOfConsumers = 2;
        
        ThreadSafeList<int> list = new ThreadSafeList<int>();
        List<Task> workers = new List<Task>();
        Producer<int> producerOfInt = new Producer<int>();
        Consumer<int> consumerOfInt = new Consumer<int>();
        RandomGenerator<int> randomGeneratorOfInt = new RandomGenerator<int>();

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        for (int i = 0; i < numberOfProducers; i++)
        {
            var producing = producerOfInt.Produce(list, cancellationTokenSource.Token, randomGeneratorOfInt);
            workers.Add(producing);
        }
        for (int i = 0; i < numberOfConsumers; i++)
        {
            workers.Add(consumerOfInt.Consume(list, cancellationTokenSource.Token));
        }

        var readOnlyConsumerTask = consumerOfInt.ReadOnlyConsumer(list, cancellationTokenSource.Token);
        workers.Add(readOnlyConsumerTask);

        var timer1 = new Sanitizer<int>();
        timer1.Start(list);

        await Task.Delay(5 * 1000);

        timer1.Stop();
        cancellationTokenSource.Cancel();
        await Task.WhenAll(workers);

        Console.WriteLine("\nProcess of generating random int values is completed");

        ThreadSafeList<double> listOfDouble = new ThreadSafeList<double>();
        Producer<double> producerOfDouble = new Producer<double>();
        Consumer<double> consumerOfDouble = new Consumer<double>();
        RandomGenerator<double> randomGeneratorOfDouble = new RandomGenerator<double>();

        CancellationTokenSource cancellationTokenSource2 = new CancellationTokenSource();

        for (int i = 0; i < numberOfProducers; i++)
        {
            var producing = producerOfDouble.Produce(listOfDouble, cancellationTokenSource2.Token, randomGeneratorOfDouble);
            workers.Add(producing);
        }
        for (int i = 0; i < numberOfConsumers; i++)
        {
            workers.Add(consumerOfDouble.Consume(listOfDouble, cancellationTokenSource.Token));
        }

        var readOnlyConsumerTask2 = consumerOfDouble.ReadOnlyConsumer(listOfDouble, cancellationTokenSource2.Token);
        workers.Add(readOnlyConsumerTask2);

        var timer2 = new Sanitizer<double>();
        timer2.Start(listOfDouble);

        await Task.Delay(5 * 1000);
        timer2.Stop();
        cancellationTokenSource2.Cancel();

        await Task.WhenAll(workers);

        Console.WriteLine("\nProcess of generating random double values is completed");
    }
}