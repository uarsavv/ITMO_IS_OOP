using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Account;

public class DepositAccount : Account
{
    private DateTime _finishDate;
    private double _startBalance;
    private double _deposit;
    private double _deposit1 = 20000;
    private double _percent1 = 0.02;
    private double _deposit2 = 50000;
    private double _percent2 = 0.03;
    private double _deposit3 = 100000;
    private double _percent3 = 0.05;
    private DateTime _curDate;
    private int _errorDay = 0;
    private int _increaseDay = 1;
    private DateTime _curTime;
    private Client _client;
    public DepositAccount(double balance, Client client, Bank bank, DateTime finishDate, double deposit)
        : base(balance, client, bank)
    {
        ValidationException.ThrowIfNull(finishDate);
        ValidationException.ThrowIfNull(_startBalance);
        ValidationException.ThrowIfNull(deposit);
        _finishDate = finishDate;
        _startBalance = balance;
        _deposit = deposit;
        _client = client;
    }

    public void CountDeposit()
    {
        if (_deposit <= _deposit1)
        {
            Balance += _percent1 * _startBalance;
        }

        if (_deposit > _deposit1 && _deposit <= _deposit2)
        {
            Balance += _percent2 * _startBalance;
        }

        if (_deposit > _deposit2 && _deposit <= _deposit3)
        {
            Balance += _percent3 * _startBalance;
        }
    }

    public override bool Withdrawal(double amount)
    {
        if (_client.Doubtful() is false)
        {
            AccountExceptions.CheckDoubtful();
        }

        _curDate = DateTime.Now;
        if (_curDate < _finishDate)
        {
            AccountExceptions.ErrorDate();
        }

        AccountExceptions.ErrorTranslation(amount);
        AccountExceptions.CheckBalance(this, amount);
        Balance -= amount;
        return true;
    }

    public override bool Replenishment(double amount)
    {
        if (_client.Doubtful() is false)
        {
            AccountExceptions.CheckDoubtful();
        }

        AccountExceptions.ErrorTranslation(amount);
        Balance += amount;
        return true;
    }

    public override void FasterTime(int day)
    {
        while (_errorDay < day)
        {
            _errorDay += _increaseDay;
            _curTime = _curTime.AddDays(1);
            if (_curTime.Day == 1 && _curTime.Month == 1)
            {
                CountDeposit();
            }
        }
    }
}