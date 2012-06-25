using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Json;
using System.Web.Script.Serialization;

namespace MvcMobile.Controllers
{
    public class Person
    {

        public string givenName { get; set; }
        public string sn { get; set; }
        public string cn { get; set; }
        public string eduPersonPrincipalName { get; set; }
    

    }

    public class personList
    {
        public List<Person> Person { get; set; }
    }

    public class SearchController : AsyncController
    {
        public void IndexAsync(string fname="")
        {

            AsyncManager.OutstandingOperations.Increment();


            AsyncManager.Parameters["personlist"] = "na";
            AsyncManager.Parameters["search_terms"] = fname;

           
           
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

                            var personresults = readTask.Result[0];
                            Dictionary<string, string> dictionary = new Dictionary<string, string>();

                            foreach (var personresult in personresults)
                                {
                                    if (personresult.Value.Count > 0)
                                    {
                                        dictionary.Add(personresult.Value[0]["cn"].ToString().Replace("\"", ""), personresult.Value[0]["eduPersonPrincipalName"].ToString().Replace("\"", "")); 
                                    }
                                }



                            AsyncManager.Parameters["personlist"] = dictionary;
                            AsyncManager.OutstandingOperations.Decrement();
                        });
                  

                });
                
               

        }

        public ActionResult IndexCompleted(Dictionary<string, string> personlist, string search_terms)
        {
             ViewBag.JSON = personlist;
            ViewBag.search_terms = search_terms;
            ViewBag.pageid = "searchpage";
           return View();
        }
    }
}
