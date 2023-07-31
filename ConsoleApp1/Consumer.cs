namespace ConsoleApp1;

public class Consumer<T>
{
    public async Task Consume(ThreadSafeList<T> threadSafeList, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500);

            var first = threadSafeList.Pop();
            if (first is 0)
                Console.WriteLine("No element is found");
            else Console.WriteLine($"\t\tConsumed: {first}");
        }
    }

    public async Task ReadOnlyConsumer(ThreadSafeList<T> threadSafeList, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(100);
            var list = threadSafeList.GetAll();

            if (!list.Any())
                Console.WriteLine("The list is empty");
            else
            {
                Console.Write("\nThe list contains: ");
                foreach (var item in list)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
