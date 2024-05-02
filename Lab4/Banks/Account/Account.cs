using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Account;

public abstract class Account
{
    private double _balanceCur;
    private Client _clientCur;
    private DateTime _currentTime;
    private bool _checkDoubtful;
    private Bank _bankCur;
    private List<ITransaction> _allTransactions;
    protected Account(double balance, Client client, Bank bank)
    {
        ValidationException.ThrowIfNull(balance);
        ValidationException.ThrowIfNull(client);
        ValidationException.ThrowIfNull(bank);
        _balanceCur = balance;
        _clientCur = client;
        _bankCur = bank;
        _currentTime = DateTime.Now;
        _checkDoubtful = ((client.Address is null) || (client.PassportNumbers is null)) ? false : true;
        _allTransactions = new List<ITransaction>();
    }

    public Guid Id { get; private set; }
    public Client CurClient { get => _clientCur; }
    public double Balance1 { get => _balanceCur; }
    public Bank BankCurrent { get => _bankCur; }
    public List<ITransaction> AllTransaction { get => _allTransactions; }
    public double Balance { get; set; }
    protected Client CurrentClient { get; set; }
    protected DateTime Time { get; set; }
    protected bool CheckDoubtful { get; set; }
    protected Bank OwnBank { get; set; }
    public abstract bool Withdrawal(double amount); // снятие
    public abstract bool Replenishment(double amount); // пополнение
    public abstract void FasterTime(int day);

    public void AddToList(ITransaction transaction)
    {
        AccountExceptions.Validation(transaction);
        _allTransactions.Add(transaction);
    }
}