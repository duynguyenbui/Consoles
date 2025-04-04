namespace Consoles.Events
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BankAccount account = new BankAccount("Nguyen", 500);
            //BankNotificationService bankNotificationService = new BankNotificationService();

            //account.OnBalanceLow += bankNotificationService.SendLowBalanceAlert;

            //account.Withdraw(200);
            //account.Withdraw(150);
            //account.Withdraw(100);

            var student = new Student("Nguyen Bui");
            var notifier = new NotificationService();

            student.OnHighScore += notifier.Congratulate;
            student.OnHighScore += notifier.TimeOff;
            student.OnResignation += notifier.TakeResignation;

            student.TakeExam("Toan", 8.5);
            student.TakeExam("Ly", 9.5);
            student.TakeResignation();
        }
    }
}
