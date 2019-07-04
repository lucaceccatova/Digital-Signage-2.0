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
        private static string messaggio;
        private static bool categoryUtteranceInovked;
        private static bool carUtteranceInvoked;
        private static int idListCar;
        private static int timer;
        private static HubConnection connection;


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
                categoryUtteranceInovked = false;
                timer = 0;
            }
            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = skillRequest.Request as IntentRequest;
                
                switch (intentRequest.Intent.Name)
                {
                    //RIGUARDRE I METODI CHE RICHIAMANO IL GESTORE BL PER FILTRARE PER ID E NOME 
                    case "ShowVideoIntent":
                        if (intentRequest.Intent.Slots["categoria"].Value == null && intentRequest.Intent.Slots["VideoNames"].Value == null)
                        {
                            messaggio = $"Dimmi il nome del video che vuoi guardare";
                            timer = 5000;
                            //connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                            //await connection.StartAsync();
                            //await connection.InvokeAsync("sendAllVideo", GestoreBLL.GetAllVideos());
                            categoryUtteranceInovked = true;
                            idListCar = 0;
                        }
                        else if(intentRequest.Intent.Slots["categoria"].Value!=null)
                        {
                            
                            if (intentRequest.Intent.Slots["categoria"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                messaggio = $"Non ho video da mostrarti per la categoria {intentRequest.Intent.Slots["categoria"].Value}, prova dirmi il nome di un'altra tipologia di video";
                                categoryUtteranceInovked = false;
                                timer = 0;
                            }
                            else
                            {
                                messaggio = $"Dimmi il nome del video che vuoi guardare \nID CATEGORIA : " + intentRequest.Intent.Slots["categoria"].Resolution.Authorities[0].Values[0].Value.Id;
                                idListCar = int.Parse(intentRequest.Intent.Slots["categoria"].Resolution.Authorities[0].Values[0].Value.Id);
                                //RESTITUIRE VIDEO PER CATEGORIA
                                connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                await connection.StartAsync();
                                await connection.InvokeAsync("sendAllVideo", GestoreBLL.GetVideosByCategory(idListCar));
                                categoryUtteranceInovked = true;
                                timer = 5000;
                            }

                        }
                        else if (intentRequest.Intent.Slots["VideoNames"].Value != null && categoryUtteranceInovked == true)
                        {
                            
                            if (idListCar == 0 && !intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                messaggio = $"Buona visione";
                                timer = 0;
                                //connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                //await connection.StartAsync();
                                //await connection.InvokeAsync("sendVideo", GestoreBLL.GetVideosByName(intentRequest.Intent.Slots["VideoNames"].Value));
                            }
                            else if(!intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                
                                string[] tmpSplit = intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Values[0].Value.Id.Split(";");
                                if (int.Parse(tmpSplit[0]) == idListCar)
                                {
                                    messaggio = $"Buona visione CON ID";
                                    //connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                    //await connection.StartAsync();
                                    //await connection.InvokeAsync("sendVideo", GestoreBLL.GetVideosByName(intentRequest.Intent.Slots["VideoNames"].Value));
                                    timer = 0;
                                }
                                else
                                {
                                    messaggio = $"Il video che hai richiesto non è presente, dimmi il nome di un video valido";
                                    timer = 0;
                                }
                                
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
                    case "CustomizeCarIntent":
                        if (intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values.Length <= 1)
                        {
                            messaggio = $"Accedo all'Area Custom your car. \n Automobile: " + intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Name;
                            //PASSARE CON SIGNALR la macchina selezionata con tutte le sue gomme
                            //connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                            //await connection.StartAsync();
                            //await connection.InvokeAsync("sendCarTires", GestoreBLL.GetCarAndTires(int.Parse(intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Id)));
                        }
                        else
                        {
                            messaggio = $"Perfetto, ora mi dici il modello della tua " + intentRequest.Intent.Slots["auto"].Value + " ? grazie";
                        }
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;

                        break;
                    case "ReturnToSlideIntent":
                        if (categoryUtteranceInovked != false)
                        {
                            
                            
                            if (intentRequest.Intent.ConfirmationStatus != "DENIED"){
                                messaggio = $"Va bene, ritorno allo slider";
                                //connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                //await connection.StartAsync();
                                //await connection.InvokeAsync("returnToSlide",true);
                                categoryUtteranceInovked = false;
                            }
                            else
                            {

                                messaggio = $"Va bene";
                                //connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                //await connection.StartAsync();
                                //await connection.InvokeAsync("returnToSlide", false);
                                categoryUtteranceInovked = true;

                            }
                          
                        }
                        else
                        {
                            messaggio = $"Lo slider è gia sul display";     
                        }
                        timer = 0;
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        
                        break;
                    case "AMAZON.StopIntent":
                        messaggio = $"Disattivo Pirelli Voice Control";
                        timer = 0;
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = true;
                        break;
                    
                    case "ZZZ":
                        messaggio = $"Non ho capito, puoi ripetere perfavore ?";
                        timer = 0;
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                    default:
                        response = ResponseBuilder.Empty();
                        response.Response.ShouldEndSession = false;
                        break;
                }
            }
            else
            {
                messaggio = $"Non ho capito";
                response = ResponseBuilder.Tell(messaggio);
                response.Response.ShouldEndSession = false;
            }
            System.Threading.Thread.Sleep(timer);
            return new OkObjectResult(response);

        }
    }
}
