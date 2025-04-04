namespace Consoles.EventWaitHandle;

class Program
{
    static BlockingQueue<string> queue = new();
    
    static void Main(string[] args)
    {
        var t = new Thread(EnQueueThread)
        {
            IsBackground = true
        };
        
        t.Start();
        
        var t2 = new Thread(EnQueueThread)
        {
            IsBackground = true
        };
        
        t2.Start();
        
        string? s = string.Empty;

        do
        {
            Console.Write("S: ");
            s = Console.ReadLine();

            if (!string.IsNullOrEmpty(s))
            {
                queue.EnQueue(s);
            }

        } while (!string.IsNullOrEmpty(s));
    }

    static void EnQueueThread()
    {
        while (true)
        {
            var s = queue.DeQueue();
            Console.WriteLine($"\nD: {s}");
        }
    }
}