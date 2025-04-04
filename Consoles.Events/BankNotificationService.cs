namespace Consoles.Events
{
    class BankNotificationService
    {
        public void SendLowBalanceAlert(object sender, decimal balance)
        {
            Console.WriteLine($"⚠️ CANH BAO: So du thap! Chi con {balance:C} trong tai khoan.");
        }
    }
}
