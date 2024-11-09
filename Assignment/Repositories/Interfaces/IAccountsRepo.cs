using Assignment.DBO.Tables;

namespace Assignment.Repositories.Interfaces
{
    public interface IAccountsRepo
    {
        List<Account> GetAllAccounts();

        bool AddAccounts(Account account);

        bool IsValidAccount(string accountNumber, string accountHolderName);

        Account GetAccount(string accountNumber, string accountHolderName);

    }
}
