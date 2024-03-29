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
        private static string messaggio; //-- it will contains the response that alexa will give to the user
        private static bool videosUtteranceInovked; //-- True => If ShowVideoIntent is invoked; False => If ShowVideoIntent is NOT invoked;
        private static bool carUtteranceInvoked; //-- True => If CustomizeCarIntent is invoked; False => If CustomizeCarIntent is NOT invoked;
        //private static int idCategory; //-- Contains the video's Category ID. It is set to 0 when it's shown on the screen 'all videos', and when we choose to see other categories it changes. 
        private static int timer; //-- used for sending the message to Alexa with a delay for making the interaction a little bit better. [ IT IS AN OPNTIONAL FEATURE ] 
        private static HubConnection connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build(); //-- Creating SignalR hub connection variable
        private static bool ShowTireInvoked;
        private static bool ShowTireInfoInvoked;

        [FunctionName("Alexa")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //-- Deserializing the json that alexa sent to us
            string json = await req.ReadAsStringAsync();
            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);
            var requestType = skillRequest.GetRequestType();

            SkillResponse response = null;


            if (requestType == typeof(LaunchRequest)) //-- Enter in this if when we tell to Alexa to open our skill: "Controllo Vocale"
            {
                if (videosUtteranceInovked != true && ShowTireInfoInvoked != true && ShowTireInvoked != true)
                {
                    response = ResponseBuilder.Tell("Benvenuto in Pirelli Voice Control");
                   
                    response.Response.ShouldEndSession = false;
                }
                else
                {
                    response = ResponseBuilder.Tell("Ciao");
                    response.Response.ShouldEndSession = false;
                }
                await connection.StartAsync(); //-- Starting our SignalR connnection only one time at the opening of our skill. It will be closed only when user will invoke the "AMAZON.StopIntent"
                timer = 0;
            }
            else if (requestType == typeof(IntentRequest)) //-- Enter here if user invoked an Intent
            {
                var intentRequest = skillRequest.Request as IntentRequest;

                //categoryUtteranceInovked = false;

                switch (intentRequest.Intent.Name)
                {
                    //RIGUARDRE I METODI CHE RICHIAMANO IL GESTORE BL PER FILTRARE PER ID E NOME 
                    case "ShowVideoIntent":
                        if (intentRequest.Intent.Slots["numero"].Value == null) //-- Enter in this if when the user want to see 'All videos' section
                        {

                            messaggio = $"Ciao, se vuoi ti mostro qualche video. Quale vuoi vedere ?";
                            timer = 2000;
                            
                            await connection.InvokeAsync("sendAllVideo", GestoreBLL.GetAllVideos()); //-- this will send to the client (Front-End) a List of Media (but only Videos)  
                            videosUtteranceInovked = true;
                        }
                        else if (intentRequest.Intent.Slots["numero"].Value != null && videosUtteranceInovked == true)// ----- Enter here only when user can tell to alexa to play a video 
                        {
                            timer = 0;
                            if (int.Parse(intentRequest.Intent.Slots["numero"].Value) < 6 || int.Parse(intentRequest.Intent.Slots["numero"].Value) <= 0)
                            { //FARMI INVIARE DA FRONT END CON SIGNALR UN BOOLEAN SE VIDEO ESISTE O NO 
                                messaggio = $"Buona visione";
                                
                                await connection.InvokeAsync("sendVideo", int.Parse(intentRequest.Intent.Slots["numero"].Value));
                            }
                            else
                            {
                                messaggio = $"Il Video " + int.Parse(intentRequest.Intent.Slots["numero"].Value) +"non � disponibile, quelli che puoi riprodurre sono sul display";
                            }
                        }
                        else
                        {
                            timer = 0;
                            messaggio = $"Mi spiace non ho video da mostrarti in quest'area. Se vuoi vedere i filmati basta che mi dici :'Alexa, voglio vedere i video'";
                        }
                        response = ResponseBuilder.Tell(messaggio); //-- response will contain the message that we want send to Alexa 
                        response.Response.ShouldEndSession = false; //-- ShouldEndSession is set to false because otherwise alexa will close our skill and user can't continue to interact and have to open again the skill
                        break;
                    case "ShowTireIntent": //RIGUARDARE IN CASO DI TIPOLOGIA GOMMA ERRATA
                            if (GestoreBLL.TireTypeExist(intentRequest.Intent.Slots["TireType"].Value)!=true)
                            {
                                var types = GestoreBLL.GetTiresType();
                                messaggio = "Hey potresti dirmi la tipologia dei pneumatici ? Te ne consiglio alcune : ";
                                if (types.Count >= 1 || types.Count <= 4)
                                {
                                    for (int i = 0; i < types.Count; i++)
                                    {
                                        messaggio += types[i] + " , ";
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i <4; i++)
                                    {
                                        messaggio += types[i] + " , ";
                                    }
                                }
                                timer = 0;
                            }
                            else
                            {
                                messaggio = "Hey puoi dirmi che auto hai ? oppure che auto vorresti avere ?";
                                timer = 6000;
                                ShowTireInvoked = true;
                                videosUtteranceInovked = false;
                                //messaggio = "Visuallizo le ruote per type";
                                await connection.InvokeAsync("SendTiresByType", GestoreBLL.GetTires(intentRequest.Intent.Slots["TireType"].Value));
                            }
                        response = ResponseBuilder.Tell(messaggio); //-- response will contain the message that we want send to Alexa 
                        response.Response.ShouldEndSession = false;
                        break;
                    case "ShowTireInfoIntent": //RIGUARDARE GESTOREBL E DB
                        if (ShowTireInvoked != false)
                        {
                            if (int.Parse(intentRequest.Intent.Slots["numero"].Value) <= 3 || int.Parse(intentRequest.Intent.Slots["numero"].Value) <= 0)
                            {
                                timer = 1000;
                                messaggio = "In questa schermata puoi vedere tutte le informazioni sul pneumatico scelto. Ah se vuoi posso mostrarti qualche video, basta che mi dici quale vuoi vedere";
                                ShowTireInfoInvoked = true;
                                ShowTireInvoked = false;
                                videosUtteranceInovked = true;
                                await connection.InvokeAsync("AskIdTire", int.Parse(intentRequest.Intent.Slots["numero"].Value));
                            }
                            else
                            {
                                messaggio = "Mi spiace il numero del pneumatico che hai richiesto � errato. I numeri disponibili sono riportati sul display";
                            }
                            //MANCA METODO CHE RESTITUISCE I VIDEO IN BASE ALLA GOMMA SCELTA E SE POSSIBILE ANCHE IN BASE ALL'AUTO.
                            //await connection.InvokeAsync("sendTireAndVideos", GestoreBLL.GetTireById(int.Parse(intentRequest.Intent.Slots["ruote"].Resolution.Authorities[0].Values[0].Value.Id))); //Sending the tire selected to the front-end
                        }
                        else
                        {
                            messaggio = $"Non ho capito mi spiace";
                        }
                        
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;  //-- ShouldEndSession is set to false because otherwise alexa will close our skill and user can't continue to interact and have to open again the skill
                        break;
                    case "CarIntent": //RIGUARDARE GESTOREBL E DB 
                        if (ShowTireInvoked != false)
                        {
                            if (intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values!=null)
                            {
                               /* if (GestoreBLL.CarExists(intentRequest.Intent.Slots["auto"].Value))
                                {*/
                                    if (intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values.Length > 1)
                                    {
                                        timer = 0;

                                        messaggio = "Potresti dirmi il modello della " + intentRequest.Intent.Slots["auto"].Value;
                                        var models = GestoreBLL.GetCarModels(intentRequest.Intent.Slots["auto"].Value);
                                        if (models.Count >= 3)
                                        {
                                            messaggio += " ? Ti consiglio alcuni modelli disponibili : " + models[0] + " ," + models[1] + " ," + models[2] + " ,";
                                        }
                                        else
                                        {
                                            messaggio += " ? Sono disponibili questi due modelli : " + models[0] + " ," + models[1];
                                        }

                                    }
                                    else
                                    {
                                        timer = 2000;
                                        carUtteranceInvoked = true;
                                        messaggio = "Sul display sono mostrate solo le ruote compatibili con la " + intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Name;
                                        await connection.InvokeAsync("SendTiresByType", GestoreBLL.GetTiresByCar(int.Parse(intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Id)));
                                    }

                                //}
                                
                            }
                            else
                            {
                                timer = 0;
                                var car = GestoreBLL.GetCars();
                                messaggio = "La " + intentRequest.Intent.Slots["auto"].Value + " non � disponibile. Puoi dirmi il nome di un'altra auto ? Se vuoi puoi scegliere una tra queste : ";
                                if (car.Count >= 3)
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        messaggio += car[i].brand + " " + car[i].invokeName + " ,";
                                    }
                                }
                                else if (car.Count == 2)
                                {
                                    for (int i = 0; i < 2; i++)
                                    {
                                        messaggio += car[i].brand + " " + car[i].invokeName + " ,";
                                    }
                                }
                                else
                                {
                                    messaggio = "La " + intentRequest.Intent.Slots["auto"].Value + "non � disponibile. Per il momento � disponibile solo questa auto: " + car[0].brand + " " + car[0].invokeName;
                                }
                            }

                        }
                        else
                        {
                            messaggio = "Mi spiace ma non ho capito che cosa vuoi";
                        }
                        
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                    case "ReturnToSlideIntent":
                            if (videosUtteranceInovked != false || ShowTireInvoked!= false || ShowTireInfoInvoked!= false) //Enter here when we are in other areas (Video gallery, Custom your car) and the user want to return to the slider
                            {
                            messaggio = $"Va bene, ritorno allo slider";
                            await connection.InvokeAsync("returnToSlide");
                            videosUtteranceInovked = false;
                            ShowTireInfoInvoked = false;
                            ShowTireInvoked = false;
                            carUtteranceInvoked = false;
                            }
                            else //If user invokes "ReturnToSlideIntent" from the slider area
                            {
                                messaggio = $"Lo slider � gia sul display";
                            }
                            timer = 0;
                            response = ResponseBuilder.Tell(messaggio);
                            response.Response.ShouldEndSession = false;  //-- ShouldEndSession is set to false because otherwise alexa will close our skill and user can't continue to interact and have to open again the skill
                        break;
                    case "NextPageIntent":
                        if (videosUtteranceInovked != false || ShowTireInfoInvoked != false)
                        {
                            await connection.InvokeAsync("sendPage", "next");
                            //messaggio = $"next";
                        }
                        else
                        {
                            messaggio = $"Operazione non consentita";
                        }
                        timer = 0;
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                    case "PreviousPageIntent":
                        if (videosUtteranceInovked != false || ShowTireInfoInvoked != false)
                        {
                            await connection.InvokeAsync("sendPage", "previus");
                            //messaggio = $"previous";
                        }
                        else
                        {
                            messaggio = $"Operazione non consentita";
                        }
                        timer = 0;
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;                        
                    case "AMAZON.StopIntent":
                        messaggio = $"Ciao, alla prossima";
                        timer = 0;
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = true; //-- ShouldEndSession is set to true because the user want to close the skill
                        await connection.StopAsync();
                        break;

                    case "ZZZ": //-- Intent created for word that alexa can't undurstand or utterance that are not allowed in this skill ( In english version exist a built-in intent called AMAZON.Fallback for this feature)
                        messaggio = $"Non ho capito, puoi ripetere perfavore ?";
                        timer = 0;
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                    default:
                        messaggio = $"Non ho capito purtroppo";
                        response = ResponseBuilder.Tell(messaggio);
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
            return new OkObjectResult(response); //Send the response to alexa

        }
    }
}

