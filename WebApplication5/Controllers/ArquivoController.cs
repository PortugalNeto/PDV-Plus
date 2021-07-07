using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database;
using System.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using Entities;

namespace WebApplication5.Controllers
{
    public class ArquivoController : Controller
    {
        
        [HttpGet]
        public ActionResult IndexArquivo()
        {
            Arquivo arquivo = new Arquivo();
            arquivo.SaveFromDirectory();
            List<Arquivo> lstArquivo = arquivo.GetLastComunication();

            ViewBag.ListaArquivo = lstArquivo;
            return View();
        }

    }
}
