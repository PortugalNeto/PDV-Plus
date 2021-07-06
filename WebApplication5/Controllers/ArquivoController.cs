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
            List<Arquivo> lstarq = new List<Arquivo>();
            Arquivo arq = new Arquivo();
            arq.SaveFromDirectory();
            lstarq = arq.GetAll();

            ViewBag.ListaArquivo = lstarq;
            return View();
        }

    }
}
