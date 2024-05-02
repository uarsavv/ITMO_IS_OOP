using Banks.Exceptions;

namespace Banks.Entities;

public class TransferTransaction : ITransaction
{
    private double _amount;
    private Account.Account _account1;
    private Account.Account _account2;
    private Guid _id;
    public TransferTransaction(double amount, Account.Account account1, Account.Account account2)
    {
        AccountExceptions.ErrorTranslation(amount);
        ValidationException.ThrowIfNull(account1);
        ValidationException.ThrowIfNull(account2);
        _id = Guid.NewGuid();
        _amount = amount;
        _account1 = account1;
        _account2 = account2;
    }

    public Guid Id { get => _id; }
    public void Cancel()
    {
        _account1.Replenishment(_amount);
    }

    public bool Execute()
    {
        if (_account1.BankCurrent.MaxToTransfer < _amount)
        {
            BankException.CheckMaxToTransfer();
        }

        if (_account1.Withdrawal(_amount) is true)
        {
            _account2.Replenishment(_amount);
        }

        return true;
    }
}