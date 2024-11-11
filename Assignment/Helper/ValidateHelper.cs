using Assignment.Models;
using Assignment.Services.Interfaces;

namespace Assignment.Helper
{
    /// <summary>
    /// This helper class to implement all custom & business 
    /// logic relating to validation of data.
    /// </summary>
    public class ValidateHelper
    {

        //For dependency injection
        private readonly ITransactionService _tranService;
        private readonly IAccountService _acctService;

        public ValidateHelper(ITransactionService tranService, IAccountService acctService)
        {
            _acctService = acctService;
            _tranService = tranService;
        }

        /// <summary>
        /// Custome validation logic 
        /// related to account balance.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool validateAccountBalance(TransactionWithoutDate transaction)
        {
            var frmAccount = _acctService.GetAccount(transaction.FromAccount, transaction.FromAccountHolderName);
            if (frmAccount != null && frmAccount.Balance >= transaction.Amount)
                return true;
            else
                return false;
        }

        /// <summary>
        /// For validation the transaction data coming from user.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Bool</returns>
        public bool validateTransaction(TransactionWithoutDate transaction)
        {
            bool isValidated = true;

            if(!validateAccountBalance(transaction))
                isValidated = false;

            //here we can implement various types of business logic validation if required

            return isValidated;
        }
    }
}
