﻿using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Pdv
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Numero { get; set; }
        public string Estacao { get; set; }

        public void Save()
        {
            PdvDataAccess BancoDeDados = new PdvDataAccess();
            BancoDeDados.OpenConnection();
            BancoDeDados.Save(this.Estacao, this.Numero, this.Codigo);
        }

        public void Update(int id, string Estacao, string Pdv, string Codigo)
        {
            PdvDataAccess BancoDeDados = new PdvDataAccess();
            BancoDeDados.OpenConnection();
            BancoDeDados.Update(id, Estacao, Pdv, Codigo);
        }

        public List<Pdv> GetAll()
        {
            PdvDataAccess BancoDeDados = new PdvDataAccess();
            BancoDeDados.OpenConnection();
            DataTable dtGetAll = BancoDeDados.GetAll();

            List<Pdv> lstPdv = dtGetAll.AsEnumerable().
            Select(x => new Pdv
            {
                Id = x.Field<int>("Id"),
                Codigo = x.Field<string>("Codigo"),
                Numero = x.Field<string>("Numero"),
                Estacao = x.Field<string>("Estacao"),
            }).ToList();
            
            return lstPdv;
        }

        //Metodo Inativo - TODO
        public List<Pdv> GetByEstacao(string estacao)
        {
            PdvDataAccess BancoDeDados = new PdvDataAccess();
            BancoDeDados.OpenConnection();
            DataTable dtGetByEstacao = BancoDeDados.GetByEstacao(estacao);

            if (dtGetByEstacao != null)
            {
                List<Pdv> lstPdv = dtGetByEstacao.AsEnumerable().
                    Select(x => new Pdv
                {
                    Id = x.Field<int>("Id"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                }).ToList();

                return lstPdv;
            }

            else
            {
                DataTable dtGetAll = BancoDeDados.GetAll();

                //TODO - Pode ser um DTO
                List<Pdv> lstPdv = dtGetAll.AsEnumerable().
                Select(x => new Pdv
                {
                    Id = x.Field<int>("Id"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                }).ToList();

                return lstPdv;
            }
        }

        public void Delete(int id)
        {
            PdvDataAccess BancoDeDados = new PdvDataAccess();
            BancoDeDados.OpenConnection();
            BancoDeDados.Delete(id);
        }

        public bool ValidaPdv()
        {
            PdvDataAccess BancoDeDados = new PdvDataAccess();
            BancoDeDados.OpenConnection();

            return BancoDeDados.GetByCode(this.Codigo);
        }
    }
}

