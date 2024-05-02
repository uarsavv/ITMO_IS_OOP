namespace Banks.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string msg)
            : base(msg)
        {
        }

    public static void ThrowIfNull(object xobject)
    {
        if (xobject == null)
        {
            throw new ValidationException("NRE");
        }
    }

    public static ValidationException ClientException()
         {
             return new ValidationException("this has already been introduced");
         }

    public static ValidationException EmptyException()
    {
        return new ValidationException("the string cannot be empty");
    }
}