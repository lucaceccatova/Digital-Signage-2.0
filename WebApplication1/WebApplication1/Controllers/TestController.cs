using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        // GET: Test


        [Route("api/test/getdati")]
        [HttpGet]
        public List<MediaBL> GetMedia()
        {

            return GestoreBLL.GetMedia();
        }

        [Route("api/test/getlistaById/{id}")]
        [HttpGet]
        public List<MediaBL> GetListaById(int id)
        {
            return GestoreBLL.GetListById(id);
        }

        [Route("api/test/getlista")]
        [HttpGet]
        public List<ListaMediaBL> GetLista()
        {
            return GestoreBLL.GetLista();
        }

        [Route("api/test/addmedia")]
        [HttpPost]
        public void AddMedia([FromBody]MediaBL m) // Per il momento ritorna void, si considera anche il ritorno di un bool  
        {
            GestoreBLL.AddMedia(m);
        }
        [Route("api/test/eliminaSlide/{id}")]
        [HttpGet]
        public void EliminaSlide(int id)
        {
            GestoreBLL.EliminaSlide(id);
        }

        [Route("api/test/eliminaLista/{id}")]
        [HttpGet]
        public void EliminaLista(int id)
        {
            GestoreBLL.EliminaLista(id);
        }

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
    }
}