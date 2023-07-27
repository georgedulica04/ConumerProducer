public class Program
{
    public static async Task Main()
    {
        const int numberOfProducers = 2;
        const int numberOfConsumers = 2;

        List<int> list = new List<int>();
        List<Task> workers = new List<Task>();
        Producer producer = new Producer();
        Consumer consumer = new Consumer();

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        for(int i=0; i < numberOfProducers; i++)
        {
            var producing = producer.Produce(list, cancellationTokenSource.Token);
            workers.Add(producing);
        }

        for (int i = 0; i < numberOfConsumers; i++)
        {
            workers.Add(consumer.Consume(list, cancellationTokenSource.Token));
        }

        await Task.Delay(5 * 1000);
        cancellationTokenSource.Cancel();
      
        await Task.WhenAll(workers);        

        Console.WriteLine("\nThe list is having: " + list.Count + " numbers now");
    }
}

public class Producer
{
    Random rand = new Random();

    public async Task Produce(List<int> list, CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(250);
            lock (list)
            {
                int newNumber = rand.Next(1, 100);
                Console.WriteLine($"Produced: {newNumber}");
                list.Add(newNumber);
            }
        }
    }
}

public class Consumer
{
    public async Task Consume(List<int> list, CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500);
            lock (list)
            {
                if (!list.Any())
                {
                    continue;
                }

                var firstElement = list.First();
                list.Remove(firstElement);
                Console.WriteLine($"\t\tConsumed: {firstElement}");
            }
        }
    }
}