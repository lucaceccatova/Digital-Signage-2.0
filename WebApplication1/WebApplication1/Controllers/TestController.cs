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

        [Route("api/test/getlista/{id}")]
        [HttpGet]
        public List<MediaBL> GetListaMedia(int id)
        {
            return GestoreBLL.GetLista(id);
        }
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