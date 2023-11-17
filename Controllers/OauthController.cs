using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace OutlookCalendar.Controllers
{
    public class OauthController : Controller
    {
        string credentialsFile = "C:\\dev\\TeamIntegration\\OutlookCalendar\\Files\\credentials.json";
        string tokensFile = "C:\\dev\\TeamIntegration\\OutlookCalendar\\Files\\tokens.json";
        public IActionResult Callback(string code, string state, string error)
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));

            if (!string.IsNullOrWhiteSpace(code)) { 
                RestClient restClient = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
                RestRequest request = new RestRequest();

                request.AddParameter("client_id", credentials["client_id"].ToString());
                request.AddParameter("scope", credentials["scopes"].ToString());
                request.AddParameter("redirect_uri", credentials["redirect_uri"].ToString());
                request.AddParameter("code", code); 
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("client_secret", credentials["client_secret"].ToString());
                
                restClient.BuildUri(request);
                var response = restClient.Post(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.File.WriteAllText(tokensFile,response.Content);
                    return RedirectToAction("Index", "Home");
                } else
                {
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Error");
        }
    }
}
