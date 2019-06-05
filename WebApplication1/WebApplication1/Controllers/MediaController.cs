using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;

namespace WebApplication1.Controllers
{ 
           [Route("api/media")]
        [ApiController]
        public class MediaController : ControllerBase
        {
            // GET api/values
            [HttpGet]
            [Route("GetAll")]
            public List<Media> GetAll()
            {
                DAL.DB_Access g = new DB_Access();
                var todos = g.GetTodos();
                return todos;
            }
        }
}