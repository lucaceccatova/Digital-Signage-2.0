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
                carUtteranceInvoked = false;
                timer = 0;
            }
            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = skillRequest.Request as IntentRequest;
                
                //categoryUtteranceInovked = false;

                switch (intentRequest.Intent.Name)
                {
                    //RIGUARDRE I METODI CHE RICHIAMANO IL GESTORE BL PER FILTRARE PER ID E NOME 
                    case "ShowVideoIntent":
                        
                        if (intentRequest.Intent.Slots["categoria"].Value == null && intentRequest.Intent.Slots["VideoNames"].Value == null)
                        {
                            carUtteranceInvoked = false;
                            messaggio = $"Dimmi il nome del video che vuoi guardare";
                            timer = 3000;
                            //if (carUtteranceInvoked == false)
                            //{
                                connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                await connection.StartAsync();
                                await connection.InvokeAsync("sendAllVideo", GestoreBLL.GetAllVideos());
                            //}
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
                                carUtteranceInvoked = false;
                                messaggio = $"Dimmi il nome del video che vuoi guardare \n CATEGORIA : " + intentRequest.Intent.Slots["categoria"].Resolution.Authorities[0].Values[0].Value.Name;
                                idListCar = int.Parse(intentRequest.Intent.Slots["categoria"].Resolution.Authorities[0].Values[0].Value.Id);
                                //RESTITUIRE VIDEO PER CATEGORIA
                                connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                await connection.StartAsync();
                                await connection.InvokeAsync("sendAllVideo", GestoreBLL.GetVideosByCategory(idListCar));
                                categoryUtteranceInovked = true;
                                timer = 3000;
                            }
                           

                        }
                        else if (intentRequest.Intent.Slots["VideoNames"].Value != null && categoryUtteranceInovked == true && carUtteranceInvoked == false)//&& carUtteranceInvoked == false
                        {
                            
                            if (idListCar == 0 && !intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                messaggio = $"Buona visione";
                                timer = 0;
                                connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                await connection.StartAsync();
                                await connection.InvokeAsync("sendVideo", GestoreBLL.GetVideosByName(intentRequest.Intent.Slots["VideoNames"].Value));
                            }
                            else if(!intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))
                            {
                                
                                string[] tmpSplit = intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Values[0].Value.Id.Split(";");
                                if (int.Parse(tmpSplit[0]) == idListCar)
                                {
                                    messaggio = $"Buona visione";
                                    connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                    await connection.StartAsync();
                                    await connection.InvokeAsync("sendVideo", GestoreBLL.GetVideosByName(intentRequest.Intent.Slots["VideoNames"].Value));
                                    timer = 0;
                                }
                                else
                                {
                                    messaggio = $"Il video che hai richiesto non è presente, mi dici un'altro nome";
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
                        else
                        {
                            messaggio = $"I video non sono disponibili nell'area CUSTOM YOUR CAR";
                        }
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                    case "CustomizeCarIntent":
                        if (intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values.Length <= 1)
                        {
                            messaggio = $"Accedo all'Area Custom your car. \n Automobile: " + intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Name;
                            carUtteranceInvoked = true;
                            //PASSARE CON SIGNALR la macchina selezionata con tutte le sue gomme
                            connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                            await connection.StartAsync();
                            await connection.InvokeAsync("sendCarTires", GestoreBLL.GetCarAndTires(int.Parse(intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Id)));
                        }
                        else
                        {
                            messaggio = $"Perfetto, ora mi dici il modello della tua " + intentRequest.Intent.Slots["auto"].Value + " ? grazie";
                        }
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;

                        break;
                    case "CustomizeTireIntent": //SISTEMARE LE VALIDATION
                        if (carUtteranceInvoked != false)
                        {
                            messaggio = $"ho ricevuto un tier";
                            connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                            await connection.StartAsync();
                            await connection.InvokeAsync("sendTire", GestoreBLL.GetTireById(int.Parse(intentRequest.Intent.Slots["ruote"].Resolution.Authorities[0].Values[0].Value.Id)));
                        }
                        else
                        {
                            messaggio = $"Non ho capito scusa";
                        }
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                    case "ReturnToSlideIntent":
                        if (categoryUtteranceInovked != false || carUtteranceInvoked!=false)
                        {
                            
                            
                            if (intentRequest.Intent.ConfirmationStatus != "DENIED"){
                                messaggio = $"Va bene, ritorno allo slider";
                                connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                await connection.StartAsync();
                                await connection.InvokeAsync("returnToSlide",true);
                                categoryUtteranceInovked = false;
                                carUtteranceInvoked = false;
                            }
                            else
                            {

                                messaggio = $"Va bene";
                                connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                                await connection.StartAsync();
                                await connection.InvokeAsync("returnToSlide", false);
                                categoryUtteranceInovked = true;
                                carUtteranceInvoked = true;

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
