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
        
        public ActionResult Index(string estacao, string numero)
        {
            Arquivo arquivo = new Arquivo();
            Pdv pdv = new Pdv();
            List<Arquivo> lstArquivo = new List<Arquivo>();
            List<Pdv> lstpdv = new List<Pdv>();
            lstpdv = pdv.GetAll();

            if ((estacao == "Selecione a Estação" || estacao == null)  && numero == null)
            {
                lstArquivo = arquivo.GetAll();
                ViewBag.ListaArquivo = lstArquivo;
                ViewBag.ListaEstacao = new Pdv().GetAll().Select(x => x.Estacao).Distinct().ToList();
                ViewBag.ListaNumero = new List<string>();
                ViewBag.ListaPdv = lstpdv;

                return View();
            }

            else if(estacao != null && numero == null)
            {
                ViewBag.ListaEstacao = new Pdv().GetAll().Select(x => x.Estacao).Distinct().ToList();
                var lstnumero = new Pdv().GetAll().Where(x => x.Estacao == estacao).Select(y => y.Numero).ToList();
                ViewBag.ListaNumero = new List<string>();
                ViewBag.ListaArquivo = lstArquivo;
                ViewBag.ListaPdv = lstpdv;
                return Json(lstnumero, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Redirect("/pdvplus/sequencial/listar?numero=" + numero);
            }
        }

        
        

        [HttpGet]
        public ActionResult Analisar(string estacao, string codigo, DateTime dataInicio, DateTime dataFim)
        {
            Arquivo arq = new Arquivo();
            List<Arquivo> lstArquivo = new List<Arquivo>();

            lstArquivo = arq.ArquivosByCode(estacao, codigo, dataInicio, dataFim);
            ViewBag.ListaArquivo = lstArquivo;

            return View();
        }
	}
}