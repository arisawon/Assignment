using Assignment.DBO.Tables;
using Assignment.Models;
using Assignment.Repositories.Interfaces;

namespace Assignment.Services.Implements
{
    public class TransactionService : Interfaces.ITransactionService
    {

        private readonly ITransactionsRepo _transaction;
        private readonly IAccountsRepo _account;

        public TransactionService(ITransactionsRepo transaction, IAccountsRepo account)
        {
            _transaction = transaction;
            _account = account;
        }
        public bool AddTransaction(TransactionWithoutDate transaction)
        {
            var fromAccount = _account.GetAccount(transaction.FromAccount, transaction.FromAccountHolderName);
            var toAccount = _account.GetAccount(transaction.ToAccount, transaction.ToAccountHolderName);

            if(fromAccount != null && toAccount != null) //validate both the account with number and name
            {
                Transaction transactionWithDate = new Transaction();
                transactionWithDate.TransactionId = transaction.TransactionId;
                transactionWithDate.FromAccount = transaction.FromAccount;
                transactionWithDate.ToAccount = transaction.ToAccount;
                transactionWithDate.Amount = transaction.Amount;
                transactionWithDate.TransactionDate = DateTime.Now;
                transactionWithDate.TransactionDescription = transaction.TransactionDescription;

                return _transaction.AddTransaction(transactionWithDate);
            }
            else
                return false;
            
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transaction.GetAllTransactions();
        }
    }
}
