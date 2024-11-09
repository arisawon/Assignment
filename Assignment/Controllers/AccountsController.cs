using Assignment.DBO.Tables;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _service;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAccountService service, ILogger<AccountsController> logger)
        {
            _service = service;
            _logger = logger;
        }


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
