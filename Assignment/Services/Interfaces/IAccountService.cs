using Assignment.DBO.Tables;

namespace Assignment.Services.Interfaces
{

    /// <summary>
    /// This is the interface to communicate with the data layer
    /// using the process of dependency injection. So that 
    /// we can have an loosly cuppled environment.
    /// </summary>
    public interface IAccountService
    {
        List<Account> GetAllAccounts();

        bool AddAccounts(Account account);

        bool IsValidAccount(string accountNumber, string accountHolderName);

        Account GetAccount(string accountNumber, string accountHolderName);

    }
}
