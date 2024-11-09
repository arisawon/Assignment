using Assignment.DBO.Tables;

namespace Assignment.Repositories.Interfaces
{
    public interface ITransactionsRepo
    {
        List<Transaction> GetAllTransactions();
        bool AddTransaction(Transaction transaction);
    }
}
