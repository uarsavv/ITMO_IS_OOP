using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Account;

public class CreditAccount : Account
{
    private double _limit;
    private double _commission;
    private DateTime _curDate;
    private DateTime _dateForComission;
    private int _increase = 1;
    private int _disIncrease = -1;
    private int _difference = 0;
    private Client _client;
    private DateTime _curTime;
    public CreditAccount(double balance, Client client, Bank bank, double commission)
        : base(balance, client, bank)
    {
        _limit = Balance;
        Balance = 0;
        _commission = commission;
        _curDate = DateTime.Now;
        _client = client;
    }

    public void CountComission()
    {
        _dateForComission = DateTime.Now;
        if (_dateForComission >= _curDate.AddMonths(_increase))
        {
            _curDate = _curDate.AddMonths(_disIncrease);
            _curDate = _curDate.AddMonths(_increase);
            if (Balance < _limit)
            {
                Balance -= _commission;
            }
        }
        else
        {
            _curDate = _curDate.AddMonths(_disIncrease);
        }
    }

    public override bool Withdrawal(double amount)
    {
        AccountExceptions.ErrorTranslation(amount);
        if (_client.Doubtful() is false)
        {
            AccountExceptions.CheckDoubtful();
        }

        if (Balance + _limit >= _difference)
        {
            Balance -= amount;
        }

        return true;
    }

    public override bool Replenishment(double amount)
    {
        AccountExceptions.ErrorTranslation(amount);
        if (_client.Doubtful() is false)
        {
            AccountExceptions.CheckDoubtful();
        }

        Balance -= amount;
        return true;
    }

    public override void FasterTime(int day)
    {
        int errorDay = 0;
        int increaseDay = 1;
        while (errorDay < day)
        {
            errorDay += increaseDay;
            CountComission();
            _curTime = _curTime.AddDays(1);
            if (_curTime.Day == 1)
            {
            }
        }
    }
}