using Assignment.DBO.Tables;
using Assignment.Helper;
using Assignment.Repositories.Interfaces;
using Ganss.Xss;

namespace Assignment.Services.Implements
{
    /// <summary>
    /// This the service class implemented from IAccountService
    /// in this layer we can implement all our business logic.
    /// </summary>
    public class AccountService : Interfaces.IAccountService
    {

        //For dependency injection
        private readonly IAccountsRepo _account;
        private readonly EncryptionService _encrypt;
        private readonly InputSanitizationService _sanitizer;

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="account"></param>
        /// <param name="encrypt"></param>
        /// <param name="sanitizer"></param>
        public AccountService(IAccountsRepo account, EncryptionService encrypt, InputSanitizationService sanitizer)
        {
            _account = account;
            _encrypt = encrypt;
            _sanitizer = sanitizer;

        }

        /// <summary>
        /// Method for add account business logic.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Bool</returns>
        public bool AddAccounts(Account account)
        {
            account.AccountNumber = _sanitizer.Sanitize(account.AccountNumber);
            account.AccountHolderName = _sanitizer.Sanitize(account.AccountHolderName);
            account.AccountNumber = _encrypt.EncryptData(account.AccountNumber);
            return _account.AddAccounts(account);
        }


        /// <summary>
        /// Method for gat a particuler account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="accountHolderName"></param>
        /// <returns>account object</returns>
        public Account GetAccount(string accountNumber, string accountHolderName)
        {
            //applying the sanitizer
            accountNumber = _sanitizer.Sanitize(accountNumber);
            accountHolderName = _sanitizer.Sanitize(accountHolderName);
            //encryption process
            accountNumber = _encrypt.EncryptData(accountNumber);
            //Data fetching
            var result =  _account.GetAccount(accountNumber, accountHolderName);
            result.AccountNumber = _encrypt.DecryptData(result.AccountNumber);
            return result;
        }

        /// <summary>
        /// Method to get all data related to account
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAllAccounts()
        {
            var result = _account.GetAllAccounts();
            foreach (var account in result)
            {
                account.AccountNumber = _encrypt.DecryptData(account.AccountNumber);
            }
            return result;
        }

        /// <summary>
        /// Method to validate an account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="accountHolderName"></param>
        /// <returns></returns>
        public bool IsValidAccount(string accountNumber, string accountHolderName)
        {
            //applying Sanitizer
            accountNumber = _sanitizer.Sanitize(accountNumber);
            accountHolderName = _sanitizer.Sanitize(accountHolderName);
            //applying the encryption logic
            accountNumber = _encrypt.EncryptData(accountNumber);
            //data communication
            var result =  _account.IsValidAccount(accountNumber, accountHolderName);
            return result;
        }
    }
}
