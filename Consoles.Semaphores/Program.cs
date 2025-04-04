namespace Consoles.Semaphores;

internal class Program
{
    private static int MAX = 3;
    private static Random _random = new();
    private static int _itemsInBox = 0;
    private static Semaphore _semaphore = new(MAX, MAX);
    private static AutoResetEvent _fullItemAutoResetEvent = new(false);
    
    static void Main(string[] args)
    {

        for (var i = 1; i <= 2; i++)
        {
            var t = new Thread(MoveItemThread)
            {
                IsBackground = true
            };
            t.Start(i.ToString());
        }

        var tt = new Thread(ReplaceBox)
        {
            IsBackground = true
        };
        
        tt.Start();
        
        Console.ReadLine();
    }

    private static void MoveItemThread(object? o)
    {
        var armNumber = o?.ToString() ?? "none";
        while (true)
        {
            _semaphore.WaitOne();
            
            Console.WriteLine($"Arm: {armNumber} - Thread: {Environment.CurrentManagedThreadId} - Moving item...");
            Thread.Sleep(_random.Next(1000, 5000));
            MoveItem();
            Thread.Sleep(_random.Next(1000, 5000));
            Console.WriteLine($"Arm: {armNumber} - Thread: {Environment.CurrentManagedThreadId} - Item moved.");
        }
    }

    private static void MoveItem()
    {
        _itemsInBox++;
        Console.WriteLine($"Current items in box: {_itemsInBox}");
        if (_itemsInBox >= MAX)
        {
            _fullItemAutoResetEvent.Set();
        }
    }

    private static void ReplaceBox()
    {
        while (true)
        {
            _fullItemAutoResetEvent.WaitOne();
            
            Console.WriteLine("Replace new box");
            _itemsInBox = 0;
            _semaphore.Release();
        }
    }
}