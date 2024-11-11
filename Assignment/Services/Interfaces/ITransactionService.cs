using Assignment.DBO.Tables;
using Assignment.Models;

namespace Assignment.Services.Interfaces
{
    /// <summary>
    /// This is the interface to communicate with the data layer
    /// using the process of dependency injection. So that 
    /// we can have an loosly cuppled environment.
    /// </summary>
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        bool AddTransaction(TransactionWithoutDate transaction);
    }
}
