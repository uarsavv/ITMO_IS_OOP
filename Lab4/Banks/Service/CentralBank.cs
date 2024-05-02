using Banks.Entities;

namespace Banks.Service;

public class CentralBank
{
    private List<Bank> _allBanks;

    public CentralBank()
    {
        _allBanks = new List<Bank>();
    }

    public Bank AddBank(string name, double maxToTransfer, double limit, double percent, double comission)
    {
        Bank newBank = new Bank(name, maxToTransfer, limit, percent, comission);
        _allBanks.Add(newBank);
        return newBank;
    }

    public void FasterTime(int days)
    {
        foreach (Bank bank in _allBanks)
        {
            foreach (Account.Account account in bank.AllAccounts)
            {
                account.FasterTime(days);
            }
        }
    }
}