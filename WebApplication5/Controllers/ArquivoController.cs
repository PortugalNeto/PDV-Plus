using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database;
using System.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ArquivoController : Controller
    {
        //
        // GET: /Arquivo/
        public ActionResult IndexArquivo()
        {
            //Arquivo arquivo = new Arquivo();
            //arquivo.GetArquivo();
            return View();
        }

        //
        // GET: /Arquivo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Arquivo/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Arquivo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
