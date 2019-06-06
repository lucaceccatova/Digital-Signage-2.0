using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
namespace WebApplication1.Controllers
{
    [Route("api/media")]

    [ApiController]
    public class MediaController : ControllerBase
    {
        // GET: api/Medisa
        [HttpGet]
        [Route("get")]
        public List<Media> GetAll()
        {
            DAL.DB_Access g = new DB_Access();
            var todos = g.GetTodos();
            return todos;
        }
    }

    //// GET: api/Medisa/5
    //[HttpGet("{id}", Name = "Get")]
    //    public string Get(int id)
    //    {
    //        return "value";
    //    }

    //    // POST: api/Medisa
    //    [HttpPost]
    //    public void Post([FromBody] string value)
    //    {
    //    }

    //    // PUT: api/Medisa/5
    //    [HttpPut("{id}")]
    //    public void Put(int id, [FromBody] string value)
    //    {
    //    }

    //    // DELETE: api/ApiWithActions/5
    //    [HttpDelete("{id}")]
    //    public void Delete(int id)
    //    {
    //    }
    //}
}
