using Assignment.DBO.Tables;
using Assignment.Models;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{

    /// <summary>
    /// This is the web API controller for Transaction table, which 
    /// will have all the details for a perticuler transaction.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class TransactionsController : ControllerBase
    {

        //For dependency injection

        private readonly ITransactionService _service;
        private readonly Helper.ValidateHelper _validate;
        private readonly ILogger<AccountsController> _logger;

        //Constructor for dependency injuction
        public TransactionsController(ITransactionService service, Helper.ValidateHelper validate, ILogger<AccountsController> logger)
        {
            _service = service;
            _logger = logger;
            _validate = validate;
        }

        /// <summary>
        /// This endpoint to get all the data store in the transaction table in database.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-tracsactions")]
        public ActionResult GetAllTransactions()
        {
            try
            {
                var result = _service.GetAllTransactions();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Error occured in the data fetching process!");
            }
        }


        /// <summary>
        /// This endpoint is for performing monitory payment 
        /// from one account to another.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>along with the action result it also return the bool value if it success or not</returns>
        [HttpPost("add-transaction")]
        public ActionResult AddTransaction(TransactionWithoutDate transaction)
        {
            try
            {
                
                if (_validate.validateAccountBalance(transaction)) //For all types of data validation
                {
                    var result = _service.AddTransaction(transaction);
                    return result ? Ok(result) : BadRequest("Error in Transaction data saving process!");
                }
                else
                {
                    _logger.LogInformation("Balance low!!",transaction);
                    return BadRequest("Balance is low to allow the transaction!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Error in Transaction data saving process!");
            }
        }

    }
}
