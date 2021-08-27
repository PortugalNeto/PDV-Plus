using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCadastroLeitura.Controllers
{
    public class SequencialController : Controller
    {
        [HttpGet]
        public ActionResult Index(string estacao, string codigo)
        {
            Arquivo arquivo = new Arquivo();
            Pdv pdv = new Pdv();
            List<Arquivo> lstArquivo = new List<Arquivo>();
            List<Pdv> lstpdv = new List<Pdv>();
            lstpdv = pdv.GetAll();

            if ((estacao == "Selecione a Estação" || estacao == null)  && codigo == null)
            {
                lstArquivo = arquivo.GetAll();
                ViewBag.ListaArquivo = lstArquivo;
                ViewBag.ListaEstacao = new Pdv().GetAll().Select(x => x.Estacao).Distinct().ToList();
                ViewBag.ListaCodigo = new Pdv().GetAll().Select(y => y.Codigo).Distinct().ToList();
                ViewBag.ListaPdv = lstpdv;

                return View();
            }

            else if(estacao != null && codigo == null)
            {
                lstArquivo = arquivo.GetAll();
                ViewBag.ListaEstacao = new Pdv().GetAll().Select(x => x.Estacao).Distinct().ToList();
                ViewBag.ListaCodigo = new Pdv().GetAll().Where(x => x.Estacao == estacao).Select(y => y.Codigo).Distinct().ToList();
                ViewBag.ListaArquivo = lstArquivo;
                ViewBag.ListaPdv = lstpdv;
                return View();
            }

            else
            {
                return Redirect("/pdvplus/sequencial/listar?codigo=" + codigo);
            }
        }

        [HttpGet]
        public ActionResult Listar(string codigo)
        {
            Arquivo arq = new Arquivo();
            List<Arquivo> lstArquivo = new List<Arquivo>();

            lstArquivo = arq.GetAll().Where(x => x.Codigo == codigo).ToList();
            ViewBag.ListaArquivo = lstArquivo;

            return View();
        }
	}
}