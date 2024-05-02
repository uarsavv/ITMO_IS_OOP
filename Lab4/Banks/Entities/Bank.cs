using System.Diagnostics.Contracts;
using Banks.Account;
using Banks.Exceptions;

namespace Banks.Entities;

public class Bank : IObservable
{
    private Guid _id;
    private double _maxToTransfer;
    private string _name;
    private double _limit;
    private double _percent;
    private double _comission;
    private List<Client> _allClients;
    private List<Account.Account> _allAccounts;
    private List<IObserver> _observers;

    public Bank(string name, double maxToTransfer, double limit, double percent, double comission)
    {
        _id = Guid.NewGuid();
        ValidationException.ThrowIfNull(name);
        ValidationException.ThrowIfNull(maxToTransfer);
        ValidationException.ThrowIfNull(limit);
        ValidationException.ThrowIfNull(percent);
        ValidationException.ThrowIfNull(comission);
        _name = name;
        _maxToTransfer = maxToTransfer;
        _limit = limit;
        _percent = percent;
        _comission = comission;
        _allClients = new List<Client>();
        _allAccounts = new List<Account.Account>();
        _observers = new List<IObserver>();
    }

    public double MaxToTransfer { get => _maxToTransfer; }
    public List<Account.Account> AllAccounts { get => _allAccounts; }

    public Client AddClient(string firstName, string secondName, string passportNumbers, string address)
    {
        Client newClient = new Client.ClientBuilder()
            .SetFirstName(firstName)
            .SetSecondName(secondName)
            .SetPassportNumbers(passportNumbers)
            .SetAddress(address)
            .Build();
        if (_allClients.Contains(newClient) is false)
        {
            _allClients.Add(newClient);
        }

        return newClient;
    }

    public void ChangePassportNumbers(Guid id, string passportNumbers)
    {
        Client currentClient = _allClients.SingleOrDefault(item => item.Id == id);
        if (currentClient is null)
        {
            BankException.ErrorValidation(currentClient);
        }

        currentClient.ChangePassportNumbers(passportNumbers);
    }

    public void ChangeAddress(Guid id, string address)
    {
        Client currentClient = _allClients.SingleOrDefault(item => item.Id == id);
        if (currentClient is null)
        {
            BankException.ErrorValidation(currentClient);
        }

        currentClient.ChangeAddress(address);
    }

    public Account.Account AddCreditAccount(Guid id, double balance)
    {
        Client currentClient = _allClients.SingleOrDefault(item => item.Id == id);
        if (currentClient is null)
        {
            BankException.ErrorValidation(currentClient);
        }

        var newCreditAccount = new CreditAccount(balance, currentClient, this, _comission);
        _allAccounts.Add(newCreditAccount);
        return newCreditAccount;
    }

    public Account.Account AddDebitAccount(Guid id, double balance)
    {
        Client currentClient = _allClients.FirstOrDefault(item => item.Id == id);
        if (currentClient is null)
        {
            BankException.ErrorValidation(currentClient);
        }

        var newDebitAccount = new DebitAccount(balance, currentClient, this, _percent);
        _allAccounts.Add(newDebitAccount);
        return newDebitAccount;
    }

    public Account.Account AddDepositAccount(Guid id, double balance, double deposit, DateTime time)
    {
        Client currentClient = _allClients.FirstOrDefault(item => item.Id == id);
        if (currentClient is null)
        {
            BankException.ErrorValidation(currentClient);
        }

        var newDepositAccount = new DepositAccount(balance, currentClient, this, time, deposit);
        _allAccounts.Add(newDepositAccount);
        return newDepositAccount;
    }

    public ITransaction Replenishment(Guid id, double amount)
    {
        Account.Account account = _allAccounts.FirstOrDefault(item => item.Id == id);
        if (account is null)
        {
            BankException.ErrorValidation(account);
        }

        account.Replenishment(amount);
        ReplenishmentTransaction transaction = new ReplenishmentTransaction(amount, account);
        account.AddToList(transaction);
        return transaction;
    }

    public ITransaction Withdrawal(Guid id, double amount)
    {
        Account.Account account = _allAccounts.SingleOrDefault(item => item.Id == id);
        if (account is null)
        {
            BankException.ErrorValidation(account);
        }

        account.Withdrawal(amount);
        ReplenishmentTransaction transaction1 = new ReplenishmentTransaction(amount, account);
        account.AddToList(transaction1);
        return transaction1;
    }

    public ITransaction Transfer(Guid id1, Guid id2, double amount)
    {
        Account.Account account1 = _allAccounts.FirstOrDefault(item => item.Id == id1);
        Account.Account account2 = _allAccounts.FirstOrDefault(item => item.Id == id2);
        if (account1 is null || account2 is null)
        {
            BankException.ErrorValidation(account1);
            BankException.ErrorValidation(account2);
        }

        if (amount > _maxToTransfer)
        {
            BankException.CheckMaxToTransfer();
        }

        AccountExceptions.CheckBalance(account1, amount);

        account1.Withdrawal(amount);
        account2.Replenishment(amount);
        TransferTransaction transaction2 = new TransferTransaction(amount, account1, account2);
        account1.AddToList(transaction2);
        account2.AddToList(transaction2);
        return transaction2;
    }

    public void AddObserver(IObserver obs)
    {
        BankException.ErrorValidation(obs);
        _observers.Add(obs);
    }

    public void RemoveObserver(IObserver obs)
    {
       BankException.ErrorValidation(obs);
       _observers.Remove(obs);
    }

    public void NotifyObservers(Account.Account account, double amount)
    {
        foreach (var observer in _observers)
        {
            observer.Update(account, amount);
        }
    }

    public void CancelTransaction(Account.Account account1, Account.Account account2, ITransaction transaction)
    {
        if (account1.AllTransaction.Contains(transaction) is true && account2.AllTransaction.Contains(transaction) is true)
        {
           transaction.Cancel();
        }
    }
}