namespace Banks.Entities;

public interface IObservable
{
    void AddObserver(IObserver obs);
    void RemoveObserver(IObserver obs);
    void NotifyObservers(Account.Account account, double amount);
}