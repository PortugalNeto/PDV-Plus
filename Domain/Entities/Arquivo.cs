using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class Arquivo : Pdv
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public int Id_pdv { get; set; }

        public void SaveFromDirectory()      // Vai às pastas e grava a InfoFile em banco
        {
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            string root = @"\\10.144.63.24\Imp\Empresa1\Garagem";
            string dirYear = DateTime.Now.ToString("yyyy");
            string dirMonth = DateTime.Now.ToString("MM");
            string dirMonth1before = DateTime.Now.AddMonths(-1).ToString("MM");
            string dirMonth2before = DateTime.Now.AddMonths(-2).ToString("MM");
            string dirnow = Path.Combine(root, dirYear, dirMonth);
            string dir1monthbefore = Path.Combine(root, dirYear, dirMonth1before);
            string dir2monthbefore = Path.Combine(root, dirYear, dirMonth2before);



            DirectoryInfo dir1 = new DirectoryInfo(root);    // Diretório Exemplo!!!
            DirectoryInfo dir2 = new DirectoryInfo(dirnow);
            DirectoryInfo dir3 = new DirectoryInfo(dir1monthbefore);
            DirectoryInfo dir4 = new DirectoryInfo(dir2monthbefore);
            

            Pdv pdv = new Pdv();
            List<Pdv> lstPdv = pdv.GetAll();

            Arquivo arquivo = new Arquivo();

            List<string> lstNomeArquivo = BancoDeDados.GetAll().AsEnumerable().
               Select(x => x.Field<string>("Nome")).ToList();

            foreach (FileInfo item in dir1.GetFiles("*.pdv*", SearchOption.TopDirectoryOnly))
            {
                arquivo.Nome = item.Name;
                string dataHoraArquivo = item.Name.Substring(62, 6) + " " + item.Name.Substring(71, 6);
                arquivo.Data = DateTime.ParseExact(dataHoraArquivo, "yyMMdd HHmmss", CultureInfo.InvariantCulture);
                arquivo.Codigo = item.Name.Substring(27, 8);
                arquivo.Estacao = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Estacao).FirstOrDefault();
                arquivo.Id_pdv = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Id).FirstOrDefault();

                if (!lstNomeArquivo.Contains(arquivo.Nome))
                {
                    BancoDeDados.Save(arquivo.Nome, arquivo.Data, arquivo.Codigo, arquivo.Estacao, arquivo.Id_pdv);
                }
            }

            foreach (FileInfo item in dir2.GetFiles("*.pdv*", SearchOption.AllDirectories))
            {
                arquivo.Nome = item.Name;
                string dataHoraArquivo = item.Name.Substring(62, 6) + " " + item.Name.Substring(71, 6);
                arquivo.Data = DateTime.ParseExact(dataHoraArquivo, "yyMMdd HHmmss", CultureInfo.InvariantCulture);
                arquivo.Codigo = item.Name.Substring(27, 8);
                arquivo.Estacao = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Estacao).FirstOrDefault();
                arquivo.Id_pdv = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Id).FirstOrDefault();

                if (!lstNomeArquivo.Contains(arquivo.Nome))
                {
                    BancoDeDados.Save(arquivo.Nome, arquivo.Data, arquivo.Codigo, arquivo.Estacao, arquivo.Id_pdv);
                }
            }

            foreach (FileInfo item in dir3.GetFiles("*.pdv*", SearchOption.AllDirectories))
            {
                arquivo.Nome = item.Name;
                string dataHoraArquivo = item.Name.Substring(62, 6) + " " + item.Name.Substring(71, 6);
                arquivo.Data = DateTime.ParseExact(dataHoraArquivo, "yyMMdd HHmmss", CultureInfo.InvariantCulture);
                arquivo.Codigo = item.Name.Substring(27, 8);
                arquivo.Estacao = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Estacao).FirstOrDefault();
                arquivo.Id_pdv = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Id).FirstOrDefault();

                if (!lstNomeArquivo.Contains(arquivo.Nome))
                {
                    BancoDeDados.Save(arquivo.Nome, arquivo.Data, arquivo.Codigo, arquivo.Estacao, arquivo.Id_pdv);
                }
            }

            foreach (FileInfo item in dir4.GetFiles("*.pdv*", SearchOption.AllDirectories))
            {
                arquivo.Nome = item.Name;
                string dataHoraArquivo = item.Name.Substring(62, 6) + " " + item.Name.Substring(71, 6);
                arquivo.Data = DateTime.ParseExact(dataHoraArquivo, "yyMMdd HHmmss", CultureInfo.InvariantCulture);
                arquivo.Codigo = item.Name.Substring(27, 8);
                arquivo.Estacao = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Estacao).FirstOrDefault();
                arquivo.Id_pdv = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Id).FirstOrDefault();

                if (!lstNomeArquivo.Contains(arquivo.Nome))
                {
                    BancoDeDados.Save(arquivo.Nome, arquivo.Data, arquivo.Codigo, arquivo.Estacao, arquivo.Id_pdv);
                }
            }
        }

        public List<Arquivo> GetAll()       // Vai ao banco e pega todos os últimos registros de cada PDV
        {
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();
            DataTable dtGetAll = BancoDeDados.GetAll();

            //TODO - Pode ser um DTO
            List<Arquivo> lstArquivo = dtGetAll.AsEnumerable().
            Select(x => new Arquivo
            {
                Id = x.Field<int>("Id"),
                Nome = x.Field<string>("Nome"),
                Data = x.Field<DateTime>("Data"), 
                Codigo = x.Field<string>("Codigo"),
                Numero = x.Field<string>("Numero"),
                Estacao = x.Field<string>("Estacao"),

            }).ToList();

            return lstArquivo;
        }

        public List<Arquivo> GetLastComunication()
        {
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            List<Arquivo> lstArquivos = BancoDeDados.GetAll().AsEnumerable().
                Select(x => new Arquivo
            {
                Id = x.Field<int>("Id"), 
                Nome = x.Field<string>("Nome"),
                Data = x.Field<DateTime>("Data"),
                Codigo = x.Field<string>("Codigo"),
                Numero = x.Field<string>("Numero"),
                Estacao = x.Field<string>("Estacao"),

            }).ToList();
            
            var filterLastComunication = lstArquivos.GroupBy(d => d.Codigo)
                    .SelectMany(g => g.OrderByDescending(d => d.Data).Take(1)).ToList();

            return filterLastComunication;
        }

        public List<Arquivo> GetLastComunicationByFilter(string estacao)
        {
            List<Arquivo> lstArquivo = new List<Arquivo>();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            //TODO - Fazer um filtro por estação 
            List<Arquivo> lstArquivos = BancoDeDados.GetAll().AsEnumerable().
                Where(x => x.Field<string>("Estacao") == estacao).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                }).ToList();

            var filterLastComunication = lstArquivos.GroupBy(d => d.Codigo)
                    .SelectMany(g => g.OrderByDescending(d => d.Data).Take(1)).ToList();

            return filterLastComunication;
        }
    }
}

