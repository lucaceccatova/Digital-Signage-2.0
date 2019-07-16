using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using DAL;
using System.Net.Http.Headers;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {

        [Route("api/getdati")]
        [HttpGet]
        public List<Media> GetMedia()
        {

            return GestoreBLL.GetMedia();
        }

        [Route("api/getcategorybyid/{id}")]
        [HttpGet]
        public List<Media> GetListaById(int id)
        {
            return GestoreBLL.GetVideosByCategoryId(id);
        }

        [Route("api/getCategories")]
        [HttpGet]
        public List<ListaMedia> GetLista()
        {
            return GestoreBLL.GetCategories();
        }

        [Route("api/addmedia")]
        [HttpPost]
        public void AddMedia([FromBody] Media m)
        {
            GestoreBLL.AddMedia(m);
        }

        [Route("api/addcategory")]
        [HttpPost]
        public void AddCategory([FromBody] ListaMedia cat)
        {
            GestoreBLL.AddCategory(cat);
        }

        [Route("api/addcar")]
        [HttpPost]
        public void AddCar([FromBody] Car auto)
        {
            GestoreBLL.AddCar(auto);
        }

        [Route("api/addtire")]
        [HttpPost]
        public void AddCar([FromBody] Tire ruota)
        {
            GestoreBLL.AddTire(ruota);
        }

        [Route("api/deletemedia/{id}")]
        [HttpGet]
        public void DeleteMedia(int id)
        {
            GestoreBLL.DeleteMedia(id);
        }

        [Route("api/deletecategory/{id}")]
        [HttpGet]
        public void DeleteCategory(int id)
        {
            GestoreBLL.DeleteCategory(id);
        }

        [Route("api/deletetire/{id}")]
        [HttpDelete]
        public void DeleteTire(int id)
        {
            GestoreBLL.DeleteTire(id);
        }

        [Route("api/deletecar/{id}")]
        [HttpGet]
        public void DeleteCar(int id)
        {
            GestoreBLL.DeleteCar(id);
        }

        //-------------------------------------------------------------------------------------------------------//


        //[Route("api/test/addmedia")]
        //[HttpPost]
        //public void AddMedia([FromBody]byte[] m) // Per il momento ritorna void, si considera anche il ritorno di un bool  
        //{
        //   //  = m;
        //  //FileContentResult f = File.WriteAllBytes(@"C: \Users\leon.rahman", m.file);
        ////    GestoreBLL.AddMedia(m);
        //}
        //[Route("api/test/eliminaSlide/{id}")]
        //[HttpGet]
        //public void EliminaSlide(int id)
        //{
        //    GestoreBLL.EliminaSlide(id);
        //}

        //[Route("api/test/eliminaLista/{id}")]
        //[HttpGet]
        //public void DeleteCategory(int id)
        //{
        //    GestoreBLL.DeleteCategory(id);
        //}
        //--------------------------------------------------------------------------------------------------------------------------------//







        //[Route("")]
        //    // GET: Test/Details/5
        //    public ActionResult Details(int id)
        //    {
        //        return View();
        //    }

        //    // GET: Test/Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Test/Create
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create(IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add insert logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: Test/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Test/Edit/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add update logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: Test/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Test/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add delete logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //[Route("api/upload")]
        //[HttpPost]
        //public IActionResult Upload()
        //{
        //    try
        //    {
        //        var file = Request.Form.Files[0];
        //        //var x = Request.Form.Keys.Contains("ok").ToString();
        //        var folderName = Path.Combine("Resources", "Images");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            return Ok(new { dbPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpPost, DisableRequestSizeLimit]
        [Route("api/upload")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}