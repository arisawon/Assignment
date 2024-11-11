using Assignment.DBO.Tables;

namespace Assignment.Repositories.Interfaces
{

    /// <summary>
    /// This is the interface to communicate the repository 
    /// by using the dependency injection. So if in feature we 
    /// can change our repository as & when required.
    /// </summary>
    public interface ITransactionsRepo
    {
        List<Transaction> GetAllTransactions();
        bool AddTransaction(Transaction transaction);
    }
}
