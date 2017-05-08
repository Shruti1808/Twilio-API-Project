using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Twilio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ////Make a connection with the server where the API is located.
            //var client = new RestClient("https://api.twilio.com/2010-04-01");

            ////Create Request
            //var request = new RestRequest("Accounts/ACad57e78560296f8759b89a2b8366bed5/Messages", Method.POST);

            ////Add Parameters to the request
            //request.AddParameter("To", "+14255232414");
            //request.AddParameter("From", "+14252766673");
            //request.AddParameter("Body", "Hello world!");

            ////Providing Credentials to the client
            //client.Authenticator = new HttpBasicAuthenticator("ACad57e78560296f8759b89a2b8366bed5", "be41a3e4d55288d7c7fdb80cd5fa6fec");

            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response);
            //});
            //Console.ReadLine();


            //Below is how to handle a response from an API call

            var client = new RestClient("https://api.twilio.com/2010-04-01");

            //Create a GET request :
            var request = new RestRequest("Accounts/ACad57e78560296f8759b89a2b8366bed5/Messages.json", Method.GET);
            // here .json is used  to get the response in JSON format.

            //Providing Credentials to the client:

            client.Authenticator = new HttpBasicAuthenticator("ACad57e78560296f8759b89a2b8366bed5", "be41a3e4d55288d7c7fdb80cd5fa6fec");

            var response = new RestResponse();

            //The request is made with an asynchronous method, and Task.Run with Wait() allows us to await asynchronous calls in a "synchronous" way.
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            
            Console.WriteLine(response.Content);
            Console.ReadLine();
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
