using System.Runtime.CompilerServices;
using Banks.Exceptions;

namespace Banks.Entities;

public class DebitAccount : Account.Account
{
    private double _percent;
    private DateTime _curDate;
    private int _increaseDay = 1;
    private double _curAmount = 0;
    private int _zeroing = 0;
    private DateTime _curTime;
    private Client _client;
    public DebitAccount(double balance, Client client, Bank bank, double percent)
        : base(balance, client, bank)
    {
        ValidationException.ThrowIfNull(percent);
        _percent = percent;
        _curDate = DateTime.Now;
        _client = client;
    }

    public double CountAmount()
    {
        _curAmount = _curAmount + (Balance * _percent / 365);
        return _curAmount;
    }

    public void AddAmount()
    {
        Balance += _curAmount;
        _curAmount = _zeroing;
    }

    public override bool Withdrawal(double amount)
    {
        AccountExceptions.ErrorTranslation(amount);
        if (_client.Doubtful() is false)
        {
            AccountExceptions.CheckDoubtful();
        }

        AccountExceptions.CheckBalance(this, amount);

        Balance -= amount;
        return true;
    }

    public override bool Replenishment(double amount)
    {
        AccountExceptions.ErrorTranslation(amount);
        if (_client.Doubtful() is false)
        {
            AccountExceptions.CheckDoubtful();
        }

        Balance += amount;
        return true;
    }

    public override void FasterTime(int day)
    {
        int errorDay = 0;
        while (errorDay < day)
        {
            errorDay += _increaseDay;
            CountAmount();
            _curTime = _curTime.AddDays(1);
            if (_curTime.Day == 1)
            {
                AddAmount();
            }
        }
    }
}