namespace Base.Core.Providers;

public interface IPasswordHashProvider
{
    string Encrypt(string password);
    bool Verify(string enteredPassword, string hashedPassword);
}
