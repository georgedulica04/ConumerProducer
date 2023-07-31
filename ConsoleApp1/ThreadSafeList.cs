namespace ConsoleApp1;

public class ThreadSafeList<T>
{
    public static List<T> _list = new List<T>();
    private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

    public void Add(T value)
    {
        _lock.EnterWriteLock();
        try
        {
            _list.Add(value);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public T Pop()
    {
        _lock.EnterWriteLock();
        try
        {
            if (!_list.Any()) return default;

            T first = _list.First();
            _list.Remove(first);
            return first;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
    public IEnumerable<T> GetAll()
    {
        _lock.EnterReadLock();
        try
        {
            return _list.ToList();

        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void Clear()
    {
        _lock.EnterWriteLock();
        try
        {
            _list.Clear();
            Console.WriteLine("The list is clear");
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
}