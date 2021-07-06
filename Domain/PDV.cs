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

        public void SavePDV()
        {
            BancoDeDados BancoDeDados = new BancoDeDados();
            BancoDeDados.OpenConnection();
            BancoDeDados.SetPDV(this.Estacao, this.PDV_nome, this.Codigo);
        }

        public void Update(int id, string Estacao, string Pdv, string Codigo)
        {
            BancoDeDados BancoDeDados = new BancoDeDados();
            BancoDeDados.OpenConnection();
            BancoDeDados.UpdatePDV(id, Estacao, Pdv, Codigo);
        }

        public List<PDV> GetAll()
        {
            BancoDeDados bd = new BancoDeDados();
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
            BancoDeDados bd = new BancoDeDados();
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
            BancoDeDados BancoDeDados = new BancoDeDados();
            BancoDeDados.OpenConnection();
            BancoDeDados.DeletePDV(id);
        }
    }
}

