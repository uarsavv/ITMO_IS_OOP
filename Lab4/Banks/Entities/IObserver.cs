namespace Banks.Entities;

public interface IObserver
{
    void Update(Account.Account account, double amount);
}