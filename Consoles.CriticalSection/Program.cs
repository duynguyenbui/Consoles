namespace Consoles.CriticalSection;

internal class Program
{
    private static int x = 10;
    private static int y = 20;
    
    private static object locker = new object();
    
    static void Main(string[] args)
    {
        var t = new Thread(P)
        {
            IsBackground = true
        };
        t.Start();
        
        PrintXy();
        Swap();
        PrintXy();

        Console.ReadLine();
    }

    private static void Swap()
    {
        lock (locker)
        {
            var t = x;
            Thread.Sleep(300);
            x = y;
            Thread.Sleep(200);
            y = t;
        }
    }

    private static void PrintXy()
    {
        lock (locker)
        {
            Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId}: x = {x}, y = {y}");       
        }
    }

    private static void P()
    {
        while (true)
        {
            PrintXy();
            Thread.Sleep(100);
        }
    }
}