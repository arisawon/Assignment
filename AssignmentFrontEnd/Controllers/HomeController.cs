using AssignmentFrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssignmentFrontEnd.Controllers
{

    /// <summary>
    /// This is the MVC controller (not Web API)
    /// which will be responsible for all the views.
    /// </summary>
    public class HomeController : Controller
    {
        //For dependency injection
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        //Constroctor
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


        /// <summary>
        /// This async action is basically to show 
        /// all accounts data in a view.
        /// </summary>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> Index()
        {
            string apiUrl = "https://localhost:7242/api/Accounts/get-all-accounts";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseData = await response.Content.ReadAsStringAsync();

                // Optionally, deserialize the JSON response if necessary
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Account>>(responseData);

                // Return the response data to the view
                return View(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data from API.");
            }

        }


        /// <summary>
        /// This async action is basically to show 
        /// all transactions data in a view.
        /// </summary>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> AllTransactions()
        {
            string apiUrl = "https://localhost:7242/api/Transactions/get-all-tracsactions";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseData = await response.Content.ReadAsStringAsync();

                // Optionally, deserialize the JSON response if necessary
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Transaction>>(responseData);

                // Return the response data to the view
                return View(data);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Error retrieving data from API.");
            }
            return View();
        }


        /// <summary>
        /// This action is for the view to perform the 
        /// amount transfer from one account to another.
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult Transaction()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
