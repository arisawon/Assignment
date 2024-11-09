using Assignment.DBO.Tables;
using Assignment.Models;

namespace Assignment.Services.Interfaces
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        bool AddTransaction(TransactionWithoutDate transaction);
    }
}
