using Banks.Exceptions;

namespace Banks.Entities;

public class Client
{
    private string _firstName;
    private string _secondName;
    private string _address;
    private string _passportNumbers;
    private int _checkAdress = 1;
    private int _checkPassportNumbers = 11;
    private Guid id;

    public Client(string firstName, string secondName, string address, string passportNumbers)
    {
        ValidationException.ThrowIfNull(firstName);
        ValidationException.ThrowIfNull(secondName);
        _firstName = firstName;
        _secondName = secondName;
        id = Guid.NewGuid();
        if (address is not null)
        {
            _address = address;
        }

        if (passportNumbers is not null)
        {
            _passportNumbers = passportNumbers;
        }
    }

    public string FirstName { get; }
    public string SecondName { get; }
    public Guid Id { get => id; }

    public string Address
    {
        get => _address;
        set
        {
            if (_address is not null)
            {
                throw ValidationException.ClientException();
            }
            else if (value.Length != _checkAdress)
            {
                _address = value;
            }
            else
            {
                throw ValidationException.EmptyException();
            }
        }
    }

    public string PassportNumbers
    {
        get => _passportNumbers;
        set
        {
            if (_passportNumbers is not null)
            {
                throw ValidationException.ClientException();
            }
            else if (value.Length != _checkPassportNumbers)
            {
                _address = value;
            }
            else
            {
                throw ValidationException.EmptyException();
            }
        }
    }

    public void ChangePassportNumbers(string newPassportNumbers)
    {
        ValidationException.ThrowIfNull(newPassportNumbers);
        _passportNumbers = newPassportNumbers;
    }

    public void ChangeAddress(string newAdress)
    {
        ValidationException.ThrowIfNull(newAdress);
        _address = newAdress;
    }

    public bool Doubtful()
    {
        if (_address is null || _passportNumbers is null)
        {
            return false;
        }

        return true;
    }

    public ClientBuilder ToBuild(ClientBuilder client)
    {
        client.SetFirstName(_firstName)
            .SetSecondName(_secondName)
            .SetAddress(_address)
            .SetPassportNumbers(_passportNumbers);
        return client;
    }

    public class ClientBuilder
    {
        private string _firstName;
        private string _secondName;
        private string _address = null;
        private string _passportNumbers = null;

        public ClientBuilder SetFirstName(string firstName)
        {
            ValidationException.ThrowIfNull(firstName);
            _firstName = firstName;
            return this;
        }

        public ClientBuilder SetSecondName(string secondName)
        {
            ValidationException.ThrowIfNull(secondName);
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
            var client = new Client(_firstName, _secondName, _address, _passportNumbers);
            return client;
        }
    }
}