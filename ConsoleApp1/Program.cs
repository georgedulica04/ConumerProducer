public class Program
{
    public static async Task Main()
    {
        List<int> list = new List<int>();

        var firstProducer = new Producer();
        var secondProducer = new Producer();

        var firstConsumer = new Consumer();
        var secondConsumer = new Consumer();

        var firstProducing = firstProducer.Produce(list);
        var secondProducing = firstProducer.Produce(list);

        await Task.WhenAll(firstProducing, secondProducing);
        Console.Write("The items of list are: ");
        foreach(var item in list)
        {
            Console.Write(item +" ");
        }

        var firstConsuming = firstConsumer.Consume(list);
        var secondConsuming = secondConsumer.Consume(list);
        await Task.WhenAll(firstConsuming, secondConsuming);

        Console.WriteLine("\n\nThe list is having: " + list.Count + " numbers now");
    }
}

public class Producer
{
    Random rand = new Random();

    public async Task Produce(List<int> list)
    {
        for (int i = 0; i < 10; i++)
        {
            lock (list)
            {
                int newNumber = rand.Next(1, 100);
                list.Add(newNumber);
            }
        }
    }
}

public class Consumer
{
    public async Task Consume(List<int> list)
    {
        while(list.Count > 0)
        { 
            lock (list)
            {
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}