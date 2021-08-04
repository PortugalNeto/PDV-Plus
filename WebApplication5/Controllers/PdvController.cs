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
        public ActionResult Cadastro(string Estacao, string Pdv, string Codigo, string Status)
        {
            Pdv pdv = new Pdv();
            pdv.Estacao = Estacao;
            pdv.Numero = Pdv;
            pdv.Codigo = Codigo;
            pdv.Status = Status;

            if (!pdv.ValidaPdv())
            {
                pdv.Save();
                return Redirect("/pdvplus/pdv/lista");
            }
            else
            {
                ViewBag.Response = "Código já Cadastrado!";
            }

            return View();
            //return Redirect("/pdv/lista");
        }

        [HttpGet]
        public ActionResult Lista(string estacao)
        {
            Pdv pdv = new Pdv();
            pdv.Estacao = estacao;

            List<Pdv> lstPdv = new List<Pdv>();
            
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
            Pdv pdv = new Pdv();
            List<Pdv> lstPdv = new List<Pdv>();
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
        public ActionResult Edita(int id, string Estacao, string Pdv, string Codigo, string Status)
        {
            List<Pdv> lstPdv = new List<Pdv>();
            lstPdv = new Pdv().GetAll();
            foreach (var pdv in lstPdv)
            {
                if (pdv.Id == id)
                {
                    pdv.Update(id, Estacao, Pdv, Codigo, Status);

                }
            }          
            
            return Redirect("/pdvplus/pdv/lista");
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            Pdv pdv = new Pdv();
            List<Pdv> lstPdv = new List<Pdv>();
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
            List<Pdv> lstPdv = new List<Pdv>();
            Pdv pdv_exclui = new Pdv();
            lstPdv = pdv_exclui.GetAll();
            foreach (var i in lstPdv)
            {
                if (i.Id == id)
                {
                    i.Delete(id);
                }
            }
            return Redirect("/pdvplus/pdv/lista");
        }
	}
}




            
