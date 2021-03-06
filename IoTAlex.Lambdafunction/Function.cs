using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace IoTAlex.Lambdafunction
{
    using System.Net.Http;
    using System.Net.Http.Headers;

    using Alexa.NET;
    using Alexa.NET.Request;
    using Alexa.NET.Request.Type;
    using Alexa.NET.Response;

    using Newtonsoft.Json.Linq;

    public class Function
    {
        
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            // your function logic goes here
            // check what type of a request it is like an IntentRequest or a LaunchRequest
            var requestType = input.GetRequestType();

            if (requestType == typeof(IntentRequest))
            {
                var intentRequest = input.Request as IntentRequest;
                var responseBodyIntent = new Alexa.NET.Response.ResponseBody();
                var speechIntent = new Alexa.NET.Response.PlainTextOutputSpeech();
                var skillResponseIntent = new Alexa.NET.Response.SkillResponse();
                // check the name to determine what you should do
                if (intentRequest.Intent.Name.Equals("umbracoRootknoten"))
                {
                   
                    var informationsIntent = GetUmbracoRootNode().Result.Replace("\"", "");
                    speechIntent.Text = informationsIntent;

                    // create the response
                   
                    responseBodyIntent.OutputSpeech = speechIntent;
                    responseBodyIntent.ShouldEndSession = true; // this triggers the reprompt
                   
                    skillResponseIntent.Response = responseBodyIntent;
                    skillResponseIntent.Version = "1.0";

                    return skillResponseIntent;
                }

                if (intentRequest.Intent.Name.Equals("erstelleUmbracoRootknoten"))
                {
                    var number = intentRequest.Intent.Slots["rootknotenalias"].Value;

                    if (number != null)
                    {
                        var informationsIntent = CreateNode(number).Result.Replace("\"", "");
                        speechIntent.Text = informationsIntent;


                        responseBodyIntent.OutputSpeech = speechIntent;
                        responseBodyIntent.ShouldEndSession = true; // this triggers the reprompt

                        skillResponseIntent.Response = responseBodyIntent;
                        skillResponseIntent.Version = "1.0";

                        return skillResponseIntent;
                    }
                    speechIntent.Text = "Ung�ltig Eingabe Sie Vollidiot!";


                    responseBodyIntent.OutputSpeech = speechIntent;
                    responseBodyIntent.ShouldEndSession = true; // this triggers the reprompt

                    skillResponseIntent.Response = responseBodyIntent;
                    skillResponseIntent.Version = "1.0";

                    return skillResponseIntent;

                }
            }
            else if (requestType == typeof(Alexa.NET.Request.Type.LaunchRequest))
            {
                // default launch path executed
            }
            else if (requestType == typeof(AudioPlayerRequest))
            {
                // do some audio response stuff
            }

            var speech = new Alexa.NET.Response.PlainTextOutputSpeech();
            var informations = GetUmbracoString().Result.Replace("\"", "");
            speech.Text = informations;

            // create the response
            var responseBody = new Alexa.NET.Response.ResponseBody();
            responseBody.OutputSpeech = speech;
            responseBody.ShouldEndSession = true; // this triggers the reprompt

            var skillResponse = new Alexa.NET.Response.SkillResponse();
            skillResponse.Response = responseBody;
            skillResponse.Version = "1.0";

            return skillResponse;
    }

        private async Task<string> GetUmbracoString()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            Task<string> stringTask = client.GetStringAsync("http://iotalexaweb20170708022524.azurewebsites.net/umbraco/api/alexa/GetActualUmbracoVersion");
            var responseText = await stringTask;
            return responseText;
        }

        private async Task<string> CreateNode(string nodeIndex)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            Task<string> stringTask = client.GetStringAsync("http://iotalexaweb20170708022524.azurewebsites.net/umbraco/api/alexa/CreateRootNode?index=" + nodeIndex);
            var responseText = await stringTask;
            return responseText;
        }

        private async Task<string> GetUmbracoRootNode()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            Task<string> stringTask = client.GetStringAsync("http://iotalexaweb20170708022524.azurewebsites.net/umbraco/api/alexa/GetPossibleRootNodes");
            var responseText = await stringTask;
            return responseText;
        }
    }
}
