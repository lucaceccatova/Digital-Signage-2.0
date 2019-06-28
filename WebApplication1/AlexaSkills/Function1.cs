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
        public static string messaggio;

        //public static string tempIntent;

        public static bool carUtteranceInovked;
        public static int idListCar;

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
                response = ResponseBuilder.Tell("Benvenuto in Pirelli Voice Control");
                response.Response.ShouldEndSession = false;
            }
            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = skillRequest.Request as IntentRequest;
                
                switch (intentRequest.Intent.Name)
                {
                    case "ShowVideoIntent":
                        if (intentRequest.Intent.Slots["auto"].Value == null && intentRequest.Intent.Slots["VideoNames"].Value == null)
                        {
                            messaggio = $"Va bene, adesso ti mostro tutti i video ";
                            
                            var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                            //await connection.StartAsync();
                            //await connection.InvokeAsync("SendMessage",GestoreBLL.GetAllVideos());
                            carUtteranceInovked = true;
                            idListCar = 0;
                        }
                        else if(intentRequest.Intent.Slots["auto"].Value!=null)
                        {
                            //response = ResponseBuilder.Tell("Video");
                            if (intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                messaggio = $"Non ho video da mostrarti per le auto {intentRequest.Intent.Slots["auto"].Value}, prova dirmi il nome di un'altra automobile";
                                carUtteranceInovked = false;
                            }
                            else
                            {
                                messaggio = $"ok adesso ti mostro i video" + intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Id;
                                //RESTITUIRE VIDEO PER ID AUTO
                                //var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                //await connection.StartAsync();
                                //await connection.InvokeAsync("SendMessage",GestoreBLL.GetListById(1));
                                carUtteranceInovked = true;
                                idListCar = int.Parse(intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Id);
                            }

                        }
                        else if (intentRequest.Intent.Slots["VideoNames"].Value != null && carUtteranceInovked == true)
                        {
                            //RESTITUIRE SINGOLO VIDEO PER ID AUTO e NOME
                            if (idListCar == 0 && !intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                messaggio = $"Buona visione";
                                //var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                //await connection.StartAsync();
                                //await connection.InvokeAsync("showVideo",GestoreBLL.GetVideo()); //CREARE METEODO CHE RESTITUSCE UN SINGOLO VIDEO
                            }
                            else if(!intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                //BO VA IN ERRORE DA SOLO

                                messaggio = $"Buona visione CON ID";
                                //var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                //await connection.StartAsync();
                                //await connection.InvokeAsync("showVideo", GestoreBLL.GetVideo()); //CREARE METEODO CHE RESTITUSCE UN SINGOLO VIDEO dato id auto
                            }
                            else
                            {
                                messaggio = $"Non ho capito purtroppo, dimmi il nome del video che vuoi guardare";
                            }
                            //PRENDE PER (MOSTRA TUTTI I VIDEO) ANCHE QUANDO INSERISCO UN MOSTRA MASERATI SU CICUITO
                            
                            
                        }
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                    case "AMAZON.StopIntent":
                        messaggio = $"Disattivo Pirelli Voice Control";
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = true;
                        break;
                    
                    case "ZZZ":
                        messaggio = $"Non ho capito, puoi ripetere perfavore ?";
                        //var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                        ////await connection.StartAsync();
                        ////await connection.InvokeAsync("SendMessage",1);
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                }
            }

            return new OkObjectResult(response);

        }
    }
}
