using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace WebServiceApp.Controllers
{
    public class OrderEntryController : ApiController
    {
       
        public string POST(object payload = null)
        {

            HttpContent content = null;

            string responsePayload = "";
            // Serialize the payload if one is present
            if (payload != null)
            {
                var payloadString = JsonConvert.SerializeObject(payload);
                content = new StringContent(payloadString, Encoding.UTF8, "application/json");
            }

            // Create the Web API client with the appropriate authentication
            using (var httpClientHandler = new HttpClientHandler { Credentials = new NetworkCredential("WEBAPI", "WEBAPI") })
            using (var httpClient = new HttpClient(httpClientHandler))
            {
                Console.WriteLine("\n{0} {1}", method.Method, requestUri);

                // Create the Web API request
                var request = new HttpRequestMessage(method, requestUri)
                {
                    Content = content
                };

                // Send the Web API request
                try
                {
                    var response = await httpClient.SendAsync(request);
                    responsePayload = await response.Content.ReadAsStringAsync();

                    var statusNumber = (int)response.StatusCode;
                    Console.WriteLine("\n{0} {1}", statusNumber, response.StatusCode);

                    if (statusNumber < 200 || statusNumber >= 300)
                    {
                        Console.WriteLine(responsePayload);
                        throw new ApplicationException(statusNumber.ToString());
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("\n{0} Exception caught.", e);
                    Console.WriteLine("\n\nPlease ensure the service root URI entered is valid.");
                    Console.WriteLine("\n\nPress any key to end.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            return string.IsNullOrWhiteSpace(responsePayload) ? null : JsonConvert.DeserializeObject(responsePayload);
        }
     }
}
