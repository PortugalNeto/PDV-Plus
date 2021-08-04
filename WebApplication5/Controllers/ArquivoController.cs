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
        public ActionResult Index(string estacao)
        {
            Arquivo arquivo = new Arquivo();
            Pdv pdv = new Pdv();
            
            List<Arquivo> lstArquivo = new List<Arquivo>();

            if (!string.IsNullOrEmpty(estacao))
            {
                lstArquivo = arquivo.GetLastComunicationByFilter(estacao);
            }
            else
            {
                lstArquivo = arquivo.GetLastComunication();
            }

            ViewBag.ListaArquivo = lstArquivo;
            ViewBag.ListaEstacao = new Pdv().GetAll().Select(x => x.Estacao).Distinct().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Salvar()
        {
            Arquivo arquivo = new Arquivo();
            arquivo.SaveFromDirectory();

            return Redirect("index");
        }
    }
}
