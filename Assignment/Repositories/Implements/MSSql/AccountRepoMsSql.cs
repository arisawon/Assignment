using Assignment.DBO;
using Assignment.DBO.Tables;
using Assignment.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Assignment.Repositories.Implements.MSSql
{
    /// <summary>
    /// This is the class in the data layer to do the 
    /// database operations using LINQ and DBContext.
    /// Tjis class in implemented from the interface 
    /// named IAccountsRepo. This will be used in service 
    /// class by dependency injection. 
    /// </summary>
    public class AccountRepoMsSql : IAccountsRepo
    {
        private readonly DbAa587cAssesmentContext _database;
        private readonly ILogger<AccountRepoMsSql> _logger;

        public AccountRepoMsSql(DbAa587cAssesmentContext database, ILogger<AccountRepoMsSql> logger)
        {
            _database = database;
            _logger = logger;
        }

        /// <summary>
        /// For add a new record in the accounts table in database 
        /// by using Stored Procedure.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool AddAccounts(Account account)
        {
            try
            {
                //Parameters for the Stored Procedure
                var newIdParam = new SqlParameter
                {
                    ParameterName = "@NewId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output,
                    Value = -1
                };

                var accNumber = new SqlParameter
                {
                    ParameterName = "@AccountNumber",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = account.AccountNumber
                };

                var accName = new SqlParameter
                {
                    ParameterName = "@AccountHolderName",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = account.AccountHolderName
                };

                var balance = new SqlParameter
                {
                    ParameterName = "@Balance",
                    SqlDbType = System.Data.SqlDbType.Decimal,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = account.Balance
                };

                //Execution logic for the Store Procedure 
                if (account !=null && !string.IsNullOrWhiteSpace(account.AccountNumber) && !string.IsNullOrWhiteSpace(account.AccountHolderName))
                {
                    FormattableString sql = $"EXEC AccountsInsert @AccountNumber = {accNumber}, @AccountHolderName = {accName}, @Balance = {balance}, @NewId = {newIdParam} OUTPUT";
                    _database.Database.ExecuteSqlInterpolated(sql);
                    int newId = (int)newIdParam.Value;
                    if(newId > 0)
                        return true;
                    else
                    {
                        _logger.LogInformation("Possible error in SP in AddAccounts --> AccountRepoMsSql");
                        return false;
                    }
                }
                else
                {
                    _logger.LogInformation("The parameter object is NULL in AddAccounts --> AccountRepoMsSql");
                    return false;
                }
            }
            catch(SqlException ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                return false;
            }
        }


        /// <summary>
        /// This method is to get a particuler account data 
        /// from database by using LINQ
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="accountHolderName"></param>
        /// <returns>The account object</returns>
        public Account GetAccount(string accountNumber, string accountHolderName)
        {
            if(!string.IsNullOrWhiteSpace(accountNumber) && !string.IsNullOrWhiteSpace(accountHolderName))
            {
                var result = _database.Accounts.Where(e => (e.AccountNumber.Trim() == accountNumber.Trim()) && (e.AccountHolderName.Trim() == accountHolderName.Trim())).SingleOrDefault();
                return result;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// This method to get all data from accounts 
        /// table in database by LINQ.
        /// </summary>
        /// <returns>List of account objects</returns>
        public List<Account> GetAllAccounts()
        {
            var allAccounts = _database.Accounts.ToList();
            return allAccounts;
        }


        /// <summary>
        /// This method for validating an account information 
        /// from database using LINQ.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="accountHolderName"></param>
        /// <returns>Bool as per test</returns>
        public bool IsValidAccount(string accountNumber, string accountHolderName)
        {
            var result  = _database.Accounts.Where(e => (e.AccountNumber.Equals(accountNumber) && (e.AccountHolderName.Equals(accountHolderName)))).SingleOrDefault();

            if(result != null && result.AccountNumber.Equals(accountNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
