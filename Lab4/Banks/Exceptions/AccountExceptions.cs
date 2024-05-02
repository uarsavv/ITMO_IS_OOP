namespace Banks.Exceptions;

public class AccountExceptions : Exception
{
    public AccountExceptions(string msg)
        : base(msg)
    {
    }

    public static void ErrorTranslation(double xobject)
    {
        double errorAmount = 0;
        if (xobject <= errorAmount)
        {
            throw new AccountExceptions("Incorrect amount");
        }
    }

    public static void ErrorDate()
    {
        throw new AccountExceptions("the account has not been closed yet");
    }

    public static void Validation(object xobject)
    {
        if (xobject == null)
        {
            throw new AccountExceptions("Incorrect object");
        }
    }

    public static void CheckDoubtful()
    {
        throw new AccountExceptions("Doubtful account");
    }

    public static void CheckBalance(Account.Account account, double amount)
    {
        if (account.Balance1 < amount)
        {
            throw new AccountExceptions("insufficient funds");
        }
    }
}