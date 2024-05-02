using Banks.Exceptions;

namespace Banks.Entities;

public class ReplenishmentTransaction : ITransaction
{
    private double _amount;
    private Account.Account _account;
    private bool _status;
    private Guid _id;
    public ReplenishmentTransaction(double amount, Account.Account account)
    {
        AccountExceptions.Validation(amount);
        AccountExceptions.Validation(account);
        _amount = amount;
        _account = account;
        _id = Guid.NewGuid();
    }

    public Guid Id { get => _id; }

    public void Cancel()
    {
        _account.Withdrawal(_amount);
    }

    public bool Execute()
    {
        _status = _account.Replenishment(_amount);
        return _status;
    }
}