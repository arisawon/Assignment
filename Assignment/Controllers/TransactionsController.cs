using Assignment.DBO.Tables;
using Assignment.Models;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class TransactionsController : ControllerBase
    {

        private readonly ITransactionService _service;
        private readonly Helper.ValidateHelper _validate;
        private readonly ILogger<AccountsController> _logger;

        public TransactionsController(ITransactionService service, Helper.ValidateHelper validate, ILogger<AccountsController> logger)
        {
            _service = service;
            _logger = logger;
            _validate = validate;
        }

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

        [HttpPost("add-transaction")]
        public ActionResult AddTransaction(TransactionWithoutDate transaction)
        {
            try
            {
                
                if (_validate.validateAccountBalance(transaction))
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
