using Assignment.DBO.Tables;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    /// <summary>
    /// This is the Web API controller for Account
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class AccountsController : ControllerBase
    {
        //For dependency injection

        private readonly IAccountService _service; 
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAccountService service, ILogger<AccountsController> logger) //Construntor for dependency injection
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// This endpoint to get all data from the account table, 
        /// to view the status after any transaction.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-accounts")]
        public ActionResult GetAllAccounts()
        {
            try
            {
                var result = _service.GetAllAccounts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                return BadRequest("Error occured in the data fetching process!");
            }
            
        }

        /// <summary>
        /// This endpoint to validate any account info is proper or not.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="accountHolderName"></param>
        /// <returns>Along with the action result it send the bool value for a perticuler 
        /// info (a/c no & a/c name) if those are currect or not.</returns>
        [HttpGet("validate")]
        public ActionResult IsValidAccount(string accountNumber, string accountHolderName)
        {
            try
            {
                var result = _service.IsValidAccount(accountNumber, accountHolderName);
                return result ? Ok(result) : BadRequest("Account is not valid!");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Error occured in the data fetching process!");
            }
        }


        /// <summary>
        /// This endpoint is to add an new account to the table.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("add-account")]
        public ActionResult AddAccount(Account account)
        {
            try
            {
                var result = _service.AddAccounts(account);
                return result ? Ok(result) : BadRequest("Error in Account data saving process!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Error in Account data saving process!");
            }
        }
    }
}
