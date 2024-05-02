using Banks.Exceptions;

namespace Banks.Entities;

public class Withdrawal : ITransaction
{
    private Account.Account _account;
    private double _amount;
    private bool _status;
    private Guid _id;
    public Withdrawal(double amount, Account.Account account)
    {
        AccountExceptions.Validation(amount);
        AccountExceptions.Validation(account);
        _id = Guid.NewGuid();
        _account = account;
        _amount = amount;
    }

    public double Amount { get => _amount; }
    public Guid Id { get => _id; }

    public void Cancel()
    {
        _account.Replenishment(_amount);
    }

    public bool Execute()
    {
        _status = _account.Withdrawal(_amount);
        return _status;
    }
}