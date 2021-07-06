using WebApplication5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database;
using System.Data;
using Entities;

namespace WebApplication5.Controllers
{
    public class PdvController : Controller
    {
        //
        // GET: /Pdv/
        [HttpGet]
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(string Estacao, string Pdv, string Codigo)
        {


            PDV pdv = new PDV();
            pdv.Estacao = Estacao;
            pdv.PDV_nome = Pdv;
            pdv.Codigo = Codigo;

            pdv.SavePDV();
            return Redirect("/pdv/lista");
        }

        [HttpGet]
        public ActionResult Lista(string estacao)
        {
            PDV pdv = new PDV();
            pdv.Estacao = estacao;

            List<PDV> lstPdv = new List<PDV>();
            
            if (string.IsNullOrEmpty(estacao))
            {
                lstPdv = pdv.GetAll(); 
            }
            else
            {
                lstPdv = pdv.GetByEstacao(estacao); 
            }

            ViewBag.ListaPdv = lstPdv;

            return View();
        }

        [HttpGet]
        public ActionResult Edita(int id)
        {
            PDV pdv = new PDV();
            List<PDV> lstPdv = new List<PDV>();
            lstPdv = pdv.GetAll();
            foreach (var i in lstPdv)
            {
                if (i.Id == id)
                {
                    pdv = i;
                }
            }

            ViewBag.pdv = pdv;

            return View(pdv);
        }

        [HttpPost]
        public ActionResult Edita(int id, string Estacao, string Pdv, string Codigo)
        {
            PDV pdv = new PDV();
            List<PDV> lstPdv = new List<PDV>();
            lstPdv = pdv.GetAll();
            foreach (var i in lstPdv)
            {
                if (i.Id == id)
                {
                    i.Update(id, Estacao, Pdv, Codigo);

                }
            }          

            
            return Redirect("/pdv/lista");
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            PDV pdv = new PDV();
            List<PDV> lstPdv = new List<PDV>();
            lstPdv = pdv.GetAll();
            foreach (var i in lstPdv)
            {
                if (i.Id == id)
                {
                    pdv = i;
                }
            }
            ViewBag.pdv = pdv;
            return View(pdv);
        }

        [HttpPost]
        public ActionResult ExcluirConfirma(int id)
        {
            List<PDV> lstPdv = new List<PDV>();
            PDV pdv_exclui = new PDV();
            lstPdv = pdv_exclui.GetAll();
            foreach (var i in lstPdv)
            {
                if (i.Id == id)
                {
                    i.Delete(id);
                }
            }
            return Redirect("/pdv/lista");
        }
	}
}




            
