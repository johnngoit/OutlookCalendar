using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using OutlookCalendar.Models;
using System.IO;
using Newtonsoft.Json.Linq;

namespace OutlookCalendar.Controllers
{
    public class HomeController : Controller
    {

        string credentialsFile = "C:\\dev\\TeamIntegration\\OutlookCalendar\\Files\\credentials.json";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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

        /// <summary>
        /// 7djwrr.onmicrosoft.com
        /// http://localhost:5007/Home/Privacy
        /// </summary>
        /// <returns></returns>
        public ActionResult OauthRedirect()
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            var redirectUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?" +
                "&scope=" + credentials["scopes"].ToString() +
                "&response_type=code" +
                "&response_mode=query" +
                "&state=7djwrr" +
                "&redirect_uri=" + credentials["redirect_uri"].ToString() +
                "&client_id=" + credentials["client_id"].ToString();
            return Redirect(redirectUrl);
        }
    }
}