namespace Consoles.EventDirvenProgramming
{
    public class UserEnterNumberEvent: EventArgs
    {
        public int data;
    }

    public class UserInput()
    {
        public event EventHandler OnEnterNumberEvent;

        public void EnterNumber()
        {
            do
            {
                Console.Write($"Enter a number: ");
                string s = Console.ReadLine();
                if(!string.IsNullOrEmpty(s))
                {
                    int i = int.Parse(s);
                    OnEnterNumberEvent.Invoke(this, new UserEnterNumberEvent
                    {
                        data = i
                    });
                }
            }
            while (true);
        }
    }

    public class SqrtCalculator
    {
        public void SubcribeEvent(UserInput userInput)
        {
            userInput.OnEnterNumberEvent += Sqrt;
        }

        public void Sqrt(object? sender, EventArgs e)
        {
            var userEnterNumberEvent = (UserEnterNumberEvent)e;

            Console.WriteLine($"Sqrt of {userEnterNumberEvent.data} is {Math.Sqrt(userEnterNumberEvent.data)}");
        }

    }

    public class PowCalculator
    {
        public void SubcribeEvent(UserInput userInput)
        {
            userInput.OnEnterNumberEvent += Pow;
        }

        public void Pow(object? sender, EventArgs e)
        {
            var userEnterNumberEvent = (UserEnterNumberEvent)e;

            Console.WriteLine($"Pow of {userEnterNumberEvent.data} is {Math.Pow(userEnterNumberEvent.data, 2)}");
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // publisher
            UserInput userInput = new UserInput();

            // Subscriber
            var sqrtCalculator = new SqrtCalculator();
            sqrtCalculator.SubcribeEvent(userInput);

            // Subscriber
            var powCalculator = new PowCalculator();
            powCalculator.SubcribeEvent(userInput);

            // publish an event
            userInput.EnterNumber();
        }
    }
}
