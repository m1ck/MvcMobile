using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Json;

namespace MvcMobile.Controllers
{
    public class SearchController : AsyncController
    {
        public void IndexAsync(string fname="")
        {

            AsyncManager.OutstandingOperations.Increment();


            AsyncManager.Parameters["headlines"] = "asdasd";
            AsyncManager.Parameters["search_terms"] = fname;

           
            string line = "";
            string _address = "http://netdevone.gsu.edu/directory/api/Person?givenName="+fname+"&sn=";
           
            HttpClient client = new HttpClient();
            // Send a request asynchronously continue when complete 
            client.GetAsync(_address).ContinueWith(
                (requestTask) =>
                {
                    // Get HTTP response from completed task. 
                    HttpResponseMessage response = requestTask.Result;


                    // Check that response was successful or throw exception 
                    response.EnsureSuccessStatusCode();

                    // Read response asynchronously as JsonValue and write out top facts for each country 
                    response.Content.ReadAsAsync<JsonArray>().ContinueWith(
                        (readTask) =>
                        {

                            var country = readTask.Result[0];
                           
                               
                            /*
                                foreach (var country2 in country)
                                {
                                   
                                    line = line + "" + country2.Value[0]["cn"];

                                    line = line + "" + country2.Value[0].Count;
                                }
                            */


                            AsyncManager.Parameters["headlines"] = country.ToString();
                            AsyncManager.OutstandingOperations.Decrement();
                        });
                  

                });
                
               

        }

        public ActionResult IndexCompleted(string headlines, string search_terms)
        {

            ViewBag.JSON = headlines;
            ViewBag.search_terms = search_terms;
           // return headlines;
            return View();
        }
    }
}
