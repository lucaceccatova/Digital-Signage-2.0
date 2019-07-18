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
        public List<listMedia> GetLista()
        {
            return GestoreBLL.GetCategories();
        }

        [Route("api/addmedia")]
        [HttpPost]
        public IActionResult AddMedia([FromBody] Media m)
        {
            try
            {
                GestoreBLL.AddMedia(m);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }

        [Route("api/addcategory")]
        [HttpPost]
        public IActionResult AddCategory([FromBody] listMedia cat)
        {
            try
            {

            GestoreBLL.AddCategory(cat);
            return Ok();
            }
            catch
            {
                return StatusCode(500,"Internal server error");
            }
        }

        [Route("api/addcar")]
        [HttpPost]
        public IActionResult AddCar([FromBody] Car auto)
        {
            try
            {
                GestoreBLL.AddCar(auto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500,"internal server error");
            }


        }


        [Route("api/addtire")]
        [HttpPost]
        public IActionResult AddCar([FromBody] Tire ruota)
        {
            try
            {
                GestoreBLL.AddTire(ruota);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
       }

        [Route("api/deletemedia/{id}")]
        [HttpGet]
        public IActionResult DeleteMedia(int id)
        {
            try
            {
                GestoreBLL.DeleteMedia(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server erroe");
            }
        }

        [Route("api/deletecategory/{id}")]
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                GestoreBLL.DeleteCategory(id);
                return Ok();

            }catch(Exception ex)
            {
                return StatusCode(500, "Intenal server error");
            }
        }

        [Route("api/deletetire/{id}")]
        [HttpGet]
        public IActionResult DeleteTire(int id)
        {
            try
            {
                GestoreBLL.DeleteTire(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/deletecar/{id}")]
        [HttpGet]
        public IActionResult DeleteCar(int id)
        {
            try
            {
                GestoreBLL.DeleteCar(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/updatecategory")]
        [HttpPost]
        public IActionResult UpdateCategoty([FromBody]listMedia cat)
        {
            try
            {
                GestoreBLL.UpdateCategory(cat);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/updatemedia")]
        [HttpPost]
        public IActionResult UpdateMedia([FromBody]Media med)
        {
            try
            {
                GestoreBLL.UpdateMedia(med);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/updatecar")]
        [HttpPost]
        public IActionResult UpdateCar([FromBody]Car auto)
        {
            try
            {
                GestoreBLL.UpdateCar(auto);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("api/updatetire")]
        [HttpPost]
        public IActionResult UpdateTire([FromBody]Tire tire)
        {
            try
            {
                GestoreBLL.UpdateTire(tire);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
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
                var folderName = Path.Combine("Images");
                var pathToSave = Path.Combine(@"C:\Users\leon.rahman\Desktop", folderName);

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