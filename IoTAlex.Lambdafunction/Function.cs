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

    public class Function
    {
        
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            // your function logic goes here
            // check what type of a request it is like an IntentRequest or a LaunchRequest
            var requestType = input.GetRequestType();

            if (requestType == typeof(IntentRequest))
            {
                // do some intent-based stuff
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
            speech.Text = GetUmbracoString().Result;

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

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            return msg;
        }
    }
}
