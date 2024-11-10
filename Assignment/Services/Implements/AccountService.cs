using Assignment.DBO.Tables;
using Assignment.Helper;
using Assignment.Repositories.Interfaces;
using Ganss.Xss;

namespace Assignment.Services.Implements
{
    public class AccountService : Interfaces.IAccountService
    {
        private readonly IAccountsRepo _account;
        private readonly EncryptionService _encrypt;
        private readonly InputSanitizationService _sanitizer;

        public AccountService(IAccountsRepo account, EncryptionService encrypt, InputSanitizationService sanitizer)
        {
            _account = account;
            _encrypt = encrypt;
            _sanitizer = sanitizer;

        }
        public bool AddAccounts(Account account)
        {
            account.AccountNumber = _sanitizer.Sanitize(account.AccountNumber);
            account.AccountHolderName = _sanitizer.Sanitize(account.AccountHolderName);
            account.AccountNumber = _encrypt.EncryptData(account.AccountNumber);
            return _account.AddAccounts(account);
        }

        public Account GetAccount(string accountNumber, string accountHolderName)
        {
            accountNumber = _sanitizer.Sanitize(accountNumber);
            accountHolderName = _sanitizer.Sanitize(accountHolderName);
            accountNumber = _encrypt.EncryptData(accountNumber);
            var result =  _account.GetAccount(accountNumber, accountHolderName);
            result.AccountNumber = _encrypt.DecryptData(result.AccountNumber);
            return result;
        }

        public List<Account> GetAllAccounts()
        {
            var result = _account.GetAllAccounts();
            foreach (var account in result)
            {
                account.AccountNumber = _encrypt.DecryptData(account.AccountNumber);
            }
            return result;
        }

        public bool IsValidAccount(string accountNumber, string accountHolderName)
        {
            accountNumber = _sanitizer.Sanitize(accountNumber);
            accountHolderName = _sanitizer.Sanitize(accountHolderName);
            accountNumber = _encrypt.EncryptData(accountNumber);
            var result =  _account.IsValidAccount(accountNumber, accountHolderName);
            return result;
        }
    }
}
