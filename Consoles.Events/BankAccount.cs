namespace Consoles.Events;

public class BankAccount
{
    public string Owner { get; set; }
    public decimal Balance { get; private set; }

    public delegate void BalanceLowHandler(object sender, decimal currentBalance);

    public event BalanceLowHandler OnBalanceLow;

    public BankAccount(string owner, decimal initialBalance)
    {
        Owner = owner;
        Balance = initialBalance;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("So Tien Rut Khong Hop Le");
            return;
        }

        if (amount > Balance)
        {
            Console.WriteLine("Khong Du Tien Trong Tai Khoan.");
            return;
        }

        Balance -= amount;
        Console.WriteLine($"{Owner} Vua Rut {amount:C}. So Du Con Lai: {Balance:C}");

        if (Balance < 100)
        {
            OnBalanceLow?.Invoke(this, Balance);
        }
    }
}