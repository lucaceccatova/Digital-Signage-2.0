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
        public static int timer;

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
                            messaggio = $"Dimmi il nome del video che vuoi guardare";
                            timer = 5000;
                            //////var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                            //////await connection.StartAsync();
                            //////await connection.InvokeAsync("SendMessage", GestoreBLL.GetAllVideos());
                            carUtteranceInovked = true;
                            idListCar = 0;
                        }
                        else if(intentRequest.Intent.Slots["auto"].Value!=null)
                        {
                            
                            if (intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                messaggio = $"Non ho video da mostrarti per le auto {intentRequest.Intent.Slots["auto"].Value}, prova dirmi il nome di un'altra automobile";
                                carUtteranceInovked = false;
                                timer = 0;
                            }
                            else
                            {
                                messaggio = $"Dimmi il nome del video che vuoi guardare \nID CATEGORIA : " + intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Id;
                                idListCar = int.Parse(intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Id);
                                //RESTITUIRE VIDEO PER ID AUTO
                                //////var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                //////await connection.StartAsync();
                                //////await connection.InvokeAsync("SendMessage", GestoreBLL.GetVideosByCategory(idListCar));
                                carUtteranceInovked = true;
                                timer = 5000;
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
                                timer = 0;
                            }
                            else if(!intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                //BO VA IN ERRORE DA SOLO
                                string[] tmpSplit = intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Values[0].Value.Id.Split(";");
                                if (int.Parse(tmpSplit[0]) == idListCar)
                                {
                                    messaggio = $"Buona visione CON ID";
                                    timer = 0;
                                }
                                else
                                {
                                    messaggio = $"Il video che hai richiesto non è presente, dimmi il nome di un video valido";
                                    timer = 0;
                                }
                                //var connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                //await connection.StartAsync();
                                //await connection.InvokeAsync("showVideo", GestoreBLL.GetVideo()); //CREARE METEODO CHE RESTITUSCE UN SINGOLO VIDEO dato id auto
                            }
                            else
                            {
                                messaggio = $"Non ho capito purtroppo, dimmi il nome del video che vuoi guardare";
                                timer = 0;
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
            System.Threading.Thread.Sleep(timer);
            return new OkObjectResult(response);

        }
    }
}
