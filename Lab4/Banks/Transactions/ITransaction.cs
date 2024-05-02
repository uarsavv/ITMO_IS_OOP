namespace Banks.Entities;

public interface ITransaction
{
    void Cancel();
    bool Execute();
}