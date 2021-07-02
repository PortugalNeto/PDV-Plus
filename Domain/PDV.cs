using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class PDV
    {
        public int Id { get; set; }
        public string Codigo  { get; set; }
        public string PDV_nome { get; set; }
        public string Estacao { get; set; }

        public void Save()
        {
            BdPdv bdpdv = new BdPdv();
            bdpdv.OpenConnection();
            bdpdv.SetPDV(this.Estacao, this.PDV_nome, this.Codigo);
        }

        public void Update(int id, string Estacao, string Pdv, string Codigo)
        {
            BdPdv bdpdv = new BdPdv();
            bdpdv.OpenConnection();
            bdpdv.UpdatePDV(id, Estacao, Pdv, Codigo);
        }

        public List<PDV> GetAll()
        {
            BdPdv bd = new BdPdv();
            bd.OpenConnection();
            DataTable dtGetAll = bd.GetAllPDV();


            //TODO - Pode ser um DTO
            List<PDV> lstPdv = dtGetAll.AsEnumerable().
            Select(x => new PDV
            {
                Id = x.Field<int>("Id"),
                Codigo = x.Field<string>("Codigo"),
                PDV_nome = x.Field<string>("PDV_nome"),
                Estacao = x.Field<string>("Estacao"),
            }).ToList();
            
            return lstPdv;
        }


        public List<PDV> GetByEstacao(string estacao)
        {
            BdPdv bd = new BdPdv();
            bd.OpenConnection();
            DataTable dtGetByEstacao = bd.GetPDVByEstacao(estacao);

            if (dtGetByEstacao != null)
            {
                List<PDV> lstPdv = dtGetByEstacao.AsEnumerable().
                    Select(x => new PDV
                {
                    Id = x.Field<int>("Id"),
                    Codigo = x.Field<string>("Codigo"),
                    PDV_nome = x.Field<string>("PDV_nome"),
                    Estacao = x.Field<string>("Estacao"),
                }).ToList();

                return lstPdv;
            }

            else
            {
                DataTable dtGetAll = bd.GetAllPDV();


                //TODO - Pode ser um DTO
                List<PDV> lstPdv = dtGetAll.AsEnumerable().
                Select(x => new PDV
                {
                    Id = x.Field<int>("Id"),
                    Codigo = x.Field<string>("Codigo"),
                    PDV_nome = x.Field<string>("PDV_nome"),
                    Estacao = x.Field<string>("Estacao"),
                }).ToList();

                return lstPdv;
            }
            //TODO - Pode ser um DTO
            

            
        }

        public void Delete(int id)
        {
            BdPdv bdpdv = new BdPdv();
            bdpdv.OpenConnection();
            bdpdv.DeletePDV(id);
        }
    }
}

