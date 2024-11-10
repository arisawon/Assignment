using Assignment.DBO;
using Assignment.DBO.Tables;
using Assignment.Models;
using Assignment.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repositories.Implements.MSSql
{
    public class TransactionRepoMsSql : ITransactionsRepo
    {

        private readonly DbAa587cAssesmentContext _database;
        private readonly ILogger<TransactionRepoMsSql> _logger;

        public TransactionRepoMsSql(DbAa587cAssesmentContext database, ILogger<TransactionRepoMsSql> logger)
        {
            _database = database;
            _logger = logger;
        }
        public bool AddTransaction(Transaction transaction)
        {
            try
            {
                var newIdParam = new SqlParameter
                {
                    ParameterName = "@NewId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output,
                    Value = -1
                };

                var fromAccount = new SqlParameter
                {
                    ParameterName = "@FromAccount",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = transaction.FromAccount
                };

                var toAccount = new SqlParameter
                {
                    ParameterName = "@ToAccount",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = transaction.ToAccount
                };

                var amount = new SqlParameter
                {
                    ParameterName = "@Amount",
                    SqlDbType = System.Data.SqlDbType.Decimal,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = transaction.Amount
                };

                var desc = new SqlParameter
                {
                    ParameterName = "@TransactionDescription",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = transaction.TransactionDescription
                };

                if (transaction != null && !string.IsNullOrWhiteSpace(transaction.ToAccount) && !string.IsNullOrWhiteSpace(transaction.FromAccount))
                {
                    FormattableString sql = $"EXEC TransactionInsert @FromAccount = {fromAccount}, @ToAccount = {toAccount}, @Amount = {amount}, @TransactionDescription = {desc}, @NewId = {newIdParam} OUTPUT";
                    _database.Database.ExecuteSqlInterpolated(sql);
                    int newId = (int)newIdParam.Value;
                    if (newId > 0)
                        return true;
                    else
                    {
                        _logger.LogInformation("Possible error in SP in AddTransaction --> TransactionRepoMsSql");
                        return false;
                    }
                }
                else
                {
                    _logger.LogInformation("The parameter object is NULL in AddTransaction --> TransactionRepoMsSql");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public List<Transaction> GetAllTransactions()
        {
            var result = _database.Transactions.ToList();
            return result;
        }
    }
}
