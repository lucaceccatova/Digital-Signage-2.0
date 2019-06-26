using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;
using Alexa.NET;
using WebApplication1.Models;
using BLL;
using Microsoft.AspNetCore.SignalR.Client;

namespace AlexaSkills
{
    public static class Function1
    {
        [FunctionName("Alexa")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string json = await req.ReadAsStringAsync();
            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

            var requestType = skillRequest.GetRequestType();

            SkillResponse response = null;

            if (requestType == typeof(LaunchRequest))
            {
                response = ResponseBuilder.Tell("Welcome to AppConsult!");
                response.Response.ShouldEndSession = false;
            }
            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = skillRequest.Request as IntentRequest;

                if (intentRequest.Intent.Name == "SlideIntent")
                {
                    string output = $"Vado alla slide richiesta";
                    //BLL.IDalexa.id = 3;
                    //VoiceHub x = new VoiceHub();
                    //x.SendMessage(2);
                    var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                    //("https://localhost:44303/");
                    // var myhub = connection.CreateHubProxy("voice");

                    await connection.StartAsync();
                    await connection.InvokeAsync("SendMessage",1);
                   // await myhub.Invoke("sendMessage", 300);

                    
                    response = ResponseBuilder.Tell(output);

                    //call to webapplication1 to invoke VoiceHub.cs
                }
            }
            return new OkObjectResult(response);

        }
    }
}
