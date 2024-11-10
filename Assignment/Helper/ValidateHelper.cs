using Assignment.Models;
using Assignment.Services.Interfaces;

namespace Assignment.Helper
{
    public class ValidateHelper
    {
        private readonly ITransactionService _tranService;
        private readonly IAccountService _acctService;

        public ValidateHelper(ITransactionService tranService, IAccountService acctService)
        {
            _acctService = acctService;
            _tranService = tranService;
        }
        public bool validateAccountBalance(TransactionWithoutDate transaction)
        {
            var frmAccount = _acctService.GetAccount(transaction.FromAccount, transaction.FromAccountHolderName);
            if (frmAccount != null && frmAccount.Balance >= transaction.Amount)
                return true;
            else
                return false;
        }

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
