using System.Diagnostics;

namespace Consoles.Tasks
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var t1 = Delay1Async();
            var t2 = Delay2Async();
            var t3 = Delay3Async();

            var tr = Task.Run(RunMethod);

            var t123 = Task.WhenAll([t1, t2, t3, tr]);

            await Task.WhenAny(t123, tr);

            var t = CalculateResult(10);
            Console.WriteLine(await t);

            stopwatch.Stop();

            Console.WriteLine($"Elasped time: {stopwatch.Elapsed}");
        }

        static async Task Delay1Async()
        {
            Console.WriteLine("Delay 1...");
            await Task.Delay(1000);
            Console.WriteLine("Delay 1 done...");
        }

        static async Task Delay2Async()
        {
            Console.WriteLine("Delay 2...");
            await Task.Delay(2000);
            Console.WriteLine("Delay 2 done...");
        }

        static async Task Delay3Async()
        {
            Console.WriteLine("Delay 3...");
            await Task.Delay(3000);
            Console.WriteLine("Delay 3 done...");
        }

        static void RunMethod()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"i = {i}");
                Task.Delay(500).Wait();
            }
        }

        static Task<double> CalculateResult(int n)
        {
            if(n == 0)
            {
                return Task.FromResult(0.0);
            }

            return Task.FromResult(Math.Pow(10, n));
        }
    }
}
