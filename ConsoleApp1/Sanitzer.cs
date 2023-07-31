using ConsoleApp1;

public class Sanitizer<T>
{
    Timer _timer;
    ThreadSafeList<T> _list = null!;

    public void Start(ThreadSafeList<T> list)
    {
        _list = list;
        _timer = new Timer(new TimerCallback(CleanUp), null, 1000, 2000);
    }

    public void CleanUp(object timerState)
    {
        _list.Clear();
    }

    public void Stop()
    {
        _timer.Dispose();
    }
}