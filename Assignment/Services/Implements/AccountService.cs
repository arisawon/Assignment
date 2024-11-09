using Assignment.DBO.Tables;
using Assignment.Repositories.Interfaces;

namespace Assignment.Services.Implements
{
    public class AccountService : Interfaces.IAccountService
    {
        private readonly IAccountsRepo _account;

        public AccountService(IAccountsRepo account)
        {
            _account = account;
        }
        public bool AddAccounts(Account account)
        {
            return _account.AddAccounts(account);
        }

        public Account GetAccount(string accountNumber, string accountHolderName)
        {
            return _account.GetAccount(accountNumber, accountHolderName);
        }

        public List<Account> GetAllAccounts()
        {
            return _account.GetAllAccounts();
        }

        public bool IsValidAccount(string accountNumber, string accountHolderName)
        {
            return _account.IsValidAccount(accountNumber, accountHolderName);
        }
    }
}
