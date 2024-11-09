using Assignment.DBO.Tables;

namespace Assignment.Services.Interfaces
{
    public interface IAccountService
    {
        List<Account> GetAllAccounts();

        bool AddAccounts(Account account);

        bool IsValidAccount(string accountNumber, string accountHolderName);

        Account GetAccount(string accountNumber, string accountHolderName);

    }
}
