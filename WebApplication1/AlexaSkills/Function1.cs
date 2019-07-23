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
                if (videosUtteranceInovked == false)
                {
                    response = ResponseBuilder.Tell("Benvenuto in Pirelli Voice Control");
                    response.Response.ShouldEndSession = false;
                }
                else if (videosUtteranceInovked == true)
                {
                    response = ResponseBuilder.Tell("AVVIO DELLA SKILL DALL'AREA VIDEO GALLERY");
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
                            
                        //----------------------    await connection.InvokeAsync("sendAllVideo", GestoreBLL.GetAllVideos()); //-- this will send to the client (Front-End) a List of Media (but only Videos)  
                            videosUtteranceInovked = true;
                        }
                        else if (intentRequest.Intent.Slots["numero"].Value != null && videosUtteranceInovked == true)// ----- Enter here only when user can tell to alexa to play a video 
                        {

                            //if (idCategory == 0 /*&& !intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH")*/) //-- Enter here when we are in the "All Videos" area and user gave an correct name of the video
                            //{
                                messaggio = $"Buona visione";
                                timer = 0;

                                    //FARMI INVIARE DA FRONT END CON SIGNALR UN BOOLEAN SE VIDEO ESISTE O NO 
                            
                        //--------------------------------------await connection.InvokeAsync("sendVideo", int.Parse(intentRequest.Intent.Slots["numero"].Value));
                        

                        
                        
                        
                        //------------------------------------------------------------------//
                        //}
                            //else/*if (!intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Status.Code.Equals("ER_SUCCESS_NO_MATCH"))*///-- Enter here when we user selected a category and then give us a correct video name
                            //{

                            //    /*string[] tmpSplit = intentRequest.Intent.Slots["VideoNames"].Resolution.Authorities[0].Values[0].Value.Id.Split(";"); //-- In alexa skill console, the 'VideoNames' slot has an id like: "X;Y" where X => id of category that the video is of AND Y => id of the video; So we need to split by ";" this format of the 'VideoNames' slot to have the both ID
                            //    if (int.Parse(tmpSplit[0]) == idCategory) //-- when the video that the user selected has the same category ID of the category that the user choosed before it will enter here and send to the front-end the video 
                            //    {*/

                            //    //BOOLEAN DA SIGNALR FRONTEND PER VERIFICARE SE VIDEO ESISTE O MENO
                            //        messaggio = $"Buona visione";
                            //        await connection.InvokeAsync("sendVideo", int.Parse(intentRequest.Intent.Slots["numero"].Value));
                            //        timer = 0;

                            //    //}
                            //    //else //-- enter here because user give us a video that isn't part of the category that the user choosed before
                            //    //{
                            //    //    messaggio = $"Il video che hai richiesto non � presente qui, Tutti i nomi dei video disponibili sono riportati sullo schermo";
                            //    //    timer = 0;
                            //    //}

                            //}
                            //else //-- Enter here when the video name that is given is incorrect
                            //{
                            //    messaggio = $"Non ho capito purtroppo, dimmi il nome del video che vuoi guardare";
                            //    timer = 0;
                            //}
                        //--------------------------------------------------------------------------------------------------------//
                        }
                        else
                        {
                            messaggio = $"Mi spiace ma in quest'area non � possibile mostrare il filmato che hai richiesto. Per poter accedere alla sezione video basta che mi chiedi:'Alexa, voglio vedere i video'";
                        }
                        response = ResponseBuilder.Tell(messaggio); //-- response will contain the message that we want send to Alexa 
                        response.Response.ShouldEndSession = false; //-- ShouldEndSession is set to false because otherwise alexa will close our skill and user can't continue to interact and have to open again the skill
                        break;
                    case "ShowTireIntent": //RIGUARDARE IN CASO DI TIPOLOGIA GOMMA ERRATA
                        if (videosUtteranceInovked != false)
                        {
                            /*messaggio = $"Hey ciao, se non ti dispiace mi puoi dire che auto possiedi o che vorresti ?";
                            timer = 4000;*/
                            ShowTireInvoked = true;
                            videosUtteranceInovked = false;
                            messaggio = "Visuallizo le ruote per type";
                            //---------------------- await connection.InvokeAsync("SendTiresByType", GestoreBLL.GetTires(intentRequest.Intent.Slots["TireType"].Value));
                            
                        
                        
                        
                        
                        // VISUALIZZARE LE RUOTE PER IL TYPE SELEZIONATO, RIGUARDARE TABELLE DB E GESTOREBL
                            //  await connection.InvokeAsync();
                        }
                        else
                        {
                            messaggio = "Mi spiace non ho capito (ShowTireIntent)";
                        }
                        response = ResponseBuilder.Tell(messaggio); //-- response will contain the message that we want send to Alexa 
                        response.Response.ShouldEndSession = false;
                        break;
                    case "ShowTireInfoIntent": //RIGUARDARE GESTOREBL E DB
                        if (ShowTireInvoked != false) //Enter here only if front-end is on the "Video Gallery" area (mean that ShowVideosIntent has to be invoked before)
                        {
                            //CHIEDERE A STEFANO SE ALEXA CHIEDE ALL'UTENTE RIGUARDO L'AUTO
                            messaggio = "MOSTRA UNA RUOTA IN SPECIFICO";
                            ShowTireInfoInvoked = true;
                            ShowTireInvoked = false;
                            //----------------------await connection.InvokeAsync("AskIdTire", intentRequest.Intent.Slots["numero"].Value);




                            //MANCA METODO CHE RESTITUISCE I VIDEO IN BASE ALLA GOMMA SCELTA E SE POSSIBILE ANCHE IN BASE ALL'AUTO.
                            //await connection.InvokeAsync("sendTireAndVideos", GestoreBLL.GetTireById(int.Parse(intentRequest.Intent.Slots["ruote"].Resolution.Authorities[0].Values[0].Value.Id))); //Sending the tire selected to the front-end
                        }
                        else
                        {
                            messaggio = $" � STATO INVOCATO SHOW TIRE INFO senza passare dall'area show tire";
                        }
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;  //-- ShouldEndSession is set to false because otherwise alexa will close our skill and user can't continue to interact and have to open again the skill
                        break;
                    case "CarIntent": //RIGUARDARE GESTOREBL E DB
                        if (ShowTireInvoked != false)
                        {
                            carUtteranceInvoked = true;
                            messaggio = "Sul display sono mostrate solo le ruote compatibili con la " + intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Name;
                            //---------------------- await connection.InvokeAsync("tireShow", GestoreBLL.GetTiresByCar(int.Parse(intentRequest.Intent.Slots["auto"].Resolution.Authorities[0].Values[0].Value.Id)));
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


                            //if (intentRequest.Intent.ConfirmationStatus != "DENIED")
                            //{ //Enter here If the user confirm to return to slider
                                messaggio = $"Va bene, ritorno allo slider";

                            //----------------------  await connection.InvokeAsync("returnToSlide", true);
                            videosUtteranceInovked = false;
                                ShowTireInfoInvoked = false;
                                ShowTireInvoked = false;

                           // }
                            //else //User at the confirmation prompt decided to not return to slider
                            //{

                            //    messaggio = $"Va bene";
                            //    //  connection = new HubConnectionBuilder().WithUrl("https://localhost:44303/voice").Build();
                            //    //  await connection.StartAsync();
                            //    await connection.InvokeAsync("returnToSlide", false);
                            //    videosUtteranceInovked = true;
                            //    carUtteranceInvoked = true;
                            //}

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
                        if (videosUtteranceInovked != false)
                        {
                            //----------------------await connection.InvokeAsync("sendPage", "next");
                            messaggio = $"next";
                        }
                        else
                        {
                            messaggio = $"Operazione non consentita";
                        }
                        timer = 0;
                        response = ResponseBuilder.Tell(messaggio);
                        response.Response.ShouldEndSession = false;
                        break;
                    case "PreviuosPageIntent":
                        if (videosUtteranceInovked != false)
                        {
                            //----------------------await connection.InvokeAsync("sendPage", "previus");
                            messaggio = $"previous";
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

