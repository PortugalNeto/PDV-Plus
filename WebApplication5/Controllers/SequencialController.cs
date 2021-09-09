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

            else
            {
                ViewBag.ListaEstacao = new Pdv().GetAll().Select(x => x.Estacao).Distinct().ToList();
 
                var lstNumeros = new Pdv().GetAll().Where(x => x.Estacao == estacao).Select(y => y.Numero).ToList().OrderBy(x => x);
                
                //ViewBag.ListaNumero = new List<string>();
                //ViewBag.ListaArquivo = lstArquivo;
                ViewBag.ListaPdv = lstpdv;
                
                return Json(lstNumeros, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Analisar(string estacao, string numero, DateTime dataInicio, DateTime dataFim)
        {
            Pdv pdv = new Pdv();
            var estePdv = pdv.GetByEstacaoENumero(estacao, numero);
            Arquivo arq = new Arquivo();
            List<Arquivo> lstArquivo = new List<Arquivo>();


            lstArquivo = arq.ArquivosPeriodoByEstacaoENumero(estacao, numero, dataInicio, dataFim);

            if (lstArquivo.Count() == 0)
            {
                ViewBag.Mensagem = "Este PDV não possui arquivos registrados em banco no período selecionado.";
                return View();
            }

            else
            {
                ViewBag.ListaArquivo = lstArquivo;
                return View();
            }

        }
        
        [HttpGet]
        public ActionResult Atencao()
        {
            Pdv pdv = new Pdv();
            List<Pdv> lstpdv = new List<Pdv>();
            lstpdv = pdv.GetAll();
            List<Pdv> lstPulo = new List<Pdv>();
            foreach (var item in lstpdv)
            {
                if (item.HasArquivo(item.Codigo))
                {
                if (item.VerificaPulo(item.Codigo))
                    lstPulo.Add((Pdv)item);
                }
            }

            ViewBag.ListaPulos = lstPulo;

            return View();
        }
	}
}