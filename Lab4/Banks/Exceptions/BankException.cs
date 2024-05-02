namespace Banks.Exceptions;

public class BankException : Exception
{
    public BankException(string msg)
            : base(msg)
        {
        }

    public static void ErrorValidation(object xobject)
    {
        if (xobject == null)
        {
            throw new BankException("such a client is not registered");
        }
    }

    public static void CheckMaxToTransfer()
    {
        throw new BankException("the maximum transfer limit has been exceeded");
    }
}