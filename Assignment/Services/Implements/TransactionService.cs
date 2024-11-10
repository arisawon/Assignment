using Assignment.DBO.Tables;
using Assignment.Helper;
using Assignment.Models;
using Assignment.Repositories.Interfaces;

namespace Assignment.Services.Implements
{
    public class TransactionService : Interfaces.ITransactionService
    {

        private readonly ITransactionsRepo _transaction;
        private readonly IAccountsRepo _account;
        private readonly EncryptionService _encrypt;

        public TransactionService(ITransactionsRepo transaction, IAccountsRepo account, EncryptionService encrypt)
        {
            _transaction = transaction;
            _account = account;
            _encrypt = encrypt;
        }
        public bool AddTransaction(TransactionWithoutDate transaction)
        {
            var fromAccount = _account.GetAccount(_encrypt.EncryptData(transaction.FromAccount), transaction.FromAccountHolderName);
            var toAccount = _account.GetAccount(_encrypt.EncryptData(transaction.ToAccount), transaction.ToAccountHolderName);

            if(fromAccount != null && toAccount != null) //validate both the account with number and name
            {
                Transaction transactionWithDate = new Transaction();
                transactionWithDate.TransactionId = transaction.TransactionId;
                transactionWithDate.FromAccount = _encrypt.EncryptData(transaction.FromAccount);
                transactionWithDate.ToAccount = _encrypt.EncryptData(transaction.ToAccount);
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
            var result = _transaction.GetAllTransactions();
            foreach(var transaction in result)
            {
                transaction.FromAccount = _encrypt.DecryptData(transaction.FromAccount);
                transaction.ToAccount = _encrypt.DecryptData(transaction.ToAccount);
            }
            return result;
        }
    }
}
