namespace Banks.Entities;

public class ClientBuilder
{
    private string _firstName;
    private string _secondName;
    private string _address = null;
    private string _passportNumbers = null;

    public ClientBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public ClientBuilder SetSecondName(string secondName)
    {
        _secondName = secondName;
        return this;
    }

    public ClientBuilder SetAddress(string address)
    {
        _address = address;
        return this;
    }

    public ClientBuilder SetPassportNumbers(string passport)
    {
        _passportNumbers = passport;
        return this;
    }

    public Client Build()
    {
        return new Client(_firstName, _secondName, _address, _passportNumbers);
    }
}