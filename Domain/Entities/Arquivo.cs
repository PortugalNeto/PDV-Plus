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
        public int Sequencial { get; set; } 

        public void SaveFromDirectory()      // Vai às pastas e grava a InfoFile em banco
        {
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            string root = @"\\127.0.0.1\Imp\Empresa1\Garagem";
            string dirYear = DateTime.Now.ToString("yyyy");
            string dirMonth = DateTime.Now.ToString("MM");
            string dirMonth1before = DateTime.Now.AddMonths(-1).ToString("MM");
            string dirMonth2before = DateTime.Now.AddMonths(-2).ToString("MM");
            string dirnow = Path.Combine(root, dirYear, dirMonth);
            string dir1monthbefore = Path.Combine(root, dirYear, dirMonth1before);
            string dir2monthbefore = Path.Combine(root, dirYear, dirMonth2before);


            DirectoryInfo dir1 = new DirectoryInfo(root);    
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
                arquivo.Sequencial = Convert.ToInt32(item.Name.Substring(39, 6));
                arquivo.Codigo = (Convert.ToInt32(item.Name.Substring(27, 8))).ToString();
                arquivo.Estacao = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Estacao).FirstOrDefault();
                arquivo.Id_pdv = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Id).FirstOrDefault();
                arquivo.Status = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Status).FirstOrDefault();

                if (!lstNomeArquivo.Contains(arquivo.Nome))
                {
                    BancoDeDados.Save(arquivo.Nome, arquivo.Data, arquivo.Id_pdv, arquivo.Sequencial);
                }
            }


            foreach (FileInfo item in dir2.GetFiles("*.pdv*", SearchOption.AllDirectories))
            {
                arquivo.Nome = item.Name;
                string dataHoraArquivo = item.Name.Substring(62, 6) + " " + item.Name.Substring(71, 6);
                arquivo.Data = DateTime.ParseExact(dataHoraArquivo, "yyMMdd HHmmss", CultureInfo.InvariantCulture);
                arquivo.Sequencial = Convert.ToInt32(item.Name.Substring(39, 6));
                arquivo.Codigo = (Convert.ToInt32(item.Name.Substring(27, 8))).ToString();
                arquivo.Estacao = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Estacao).FirstOrDefault();
                arquivo.Id_pdv = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Id).FirstOrDefault();
                arquivo.Status = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Status).FirstOrDefault();

                if (!lstNomeArquivo.Contains(arquivo.Nome))
                {
                    BancoDeDados.Save(arquivo.Nome, arquivo.Data, arquivo.Id_pdv, arquivo.Sequencial);
                }
            }

            foreach (FileInfo item in dir3.GetFiles("*.pdv*", SearchOption.AllDirectories))
            {
                arquivo.Nome = item.Name;
                string dataHoraArquivo = item.Name.Substring(62, 6) + " " + item.Name.Substring(71, 6);
                arquivo.Data = DateTime.ParseExact(dataHoraArquivo, "yyMMdd HHmmss", CultureInfo.InvariantCulture);
                arquivo.Sequencial = Convert.ToInt32(item.Name.Substring(39, 6));
                arquivo.Codigo = (Convert.ToInt32(item.Name.Substring(27, 8))).ToString();
                arquivo.Estacao = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Estacao).FirstOrDefault();
                arquivo.Id_pdv = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Id).FirstOrDefault();
                arquivo.Status = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Status).FirstOrDefault();

                if (!lstNomeArquivo.Contains(arquivo.Nome))
                {
                    BancoDeDados.Save(arquivo.Nome, arquivo.Data, arquivo.Id_pdv, arquivo.Sequencial);
                }
            }

            foreach (FileInfo item in dir4.GetFiles("*.pdv*", SearchOption.AllDirectories))
            {
                arquivo.Nome = item.Name;
                string dataHoraArquivo = item.Name.Substring(62, 6) + " " + item.Name.Substring(71, 6);
                arquivo.Data = DateTime.ParseExact(dataHoraArquivo, "yyMMdd HHmmss", CultureInfo.InvariantCulture);
                arquivo.Sequencial = Convert.ToInt32(item.Name.Substring(39, 6));
                arquivo.Codigo = (Convert.ToInt32(item.Name.Substring(27, 8))).ToString();
                arquivo.Estacao = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Estacao).FirstOrDefault();
                arquivo.Id_pdv = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Id).FirstOrDefault();
                arquivo.Status = lstPdv.Where(x => x.Codigo.Equals(arquivo.Codigo)).Select(x => x.Status).FirstOrDefault();

                if (!lstNomeArquivo.Contains(arquivo.Nome))
                {
                    BancoDeDados.Save(arquivo.Nome, arquivo.Data, arquivo.Id_pdv, arquivo.Sequencial);
                }
            }
        }

        public List<Arquivo> GetAll()       // Vai ao banco e pega todos os últimos registros de cada PDV
        {
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();
            DataTable dtGetAll = BancoDeDados.GetAll();

            //DTO
            List<Arquivo> lstArquivo = dtGetAll.AsEnumerable().
            Select(x => new Arquivo
            {
                Id = x.Field<int>("Id"),
                Nome = x.Field<string>("Nome"),
                Data = x.Field<DateTime>("Data"), 
                Sequencial = x.Field<int>("Sequencial"),
                Codigo = x.Field<string>("Codigo"),
                Numero = x.Field<string>("Numero"),
                Estacao = x.Field<string>("Estacao"),
                Status = x.Field<string>("Status"),
            }).ToList();

            return lstArquivo;
        }

        public List<Arquivo> GetLastComunicationAll()
        {
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            List<Arquivo> lstArquivos = BancoDeDados.GetAll().AsEnumerable().
                Select(x => new Arquivo
            {
                Id = x.Field<int>("Id"), 
                Nome = x.Field<string>("Nome"),
                Data = x.Field<DateTime>("Data"),
                Sequencial = x.Field<int>("Sequencial"),
                Codigo = x.Field<string>("Codigo"),
                Numero = x.Field<string>("Numero"),
                Estacao = x.Field<string>("Estacao"),
                Status = x.Field<string>("Status"),

            }).ToList();
            
            var filterLastComunication = lstArquivos.GroupBy(d => d.Codigo)
                    .SelectMany(g => g.OrderByDescending(d => d.Data).Take(1)).ToList();

            return filterLastComunication;
        }

        public List<Arquivo> GetLastComunicationByCodigoAndPeriod(string codigo, DateTime dataInicio, DateTime dataFim)
        {
            List<Arquivo> lstArquivo = new List<Arquivo>();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            //TODO - Fazer um filtro por estação 
            List<Arquivo> lstArquivos = BancoDeDados.GetByPeriodo(dataInicio, dataFim).AsEnumerable().
                Where(x => x.Field<string>("Codigo") == codigo).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

            var filterLastComunication = lstArquivos.OrderByDescending(x => x.Data).Take(1).ToList();
                    

            return filterLastComunication;
        }

        public List<Arquivo> GetLastComunicationByEstacao(string codigo)
        {
            Arquivo arq = new Arquivo();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            //TODO - Fazer um filtro por estação 
            List<Arquivo> lstArquivos = BancoDeDados.GetAll().AsEnumerable().
                Where(x => x.Field<string>("Codigo") == codigo).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

            var last = lstArquivos.OrderByDescending(x => x.Data).Take(1).ToList();


            return last;
        }
        
        public Arquivo GetLastComunicationByCodigo(string codigo)
        {
            Arquivo arq = new Arquivo();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            //TODO - Fazer um filtro por estação 
            List<Arquivo> lstArquivos = BancoDeDados.GetAll().AsEnumerable().
                Where(x => x.Field<string>("Codigo") == codigo).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

            var last = lstArquivos.OrderByDescending(x => x.Data).FirstOrDefault();


            return last;
        }

        public List<Arquivo> GetLastComunicationByEstacao(string estacao, DateTime dataInicio, DateTime dataFim) 
        {
            List<Arquivo> lstArquivo = new List<Arquivo>();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            if (dataInicio == null || dataFim == null)
            {
                lstArquivo = BancoDeDados.GetAll().AsEnumerable().
                Where(x => x.Field<string>("Estacao") == estacao).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

                var last = lstArquivo.OrderByDescending(x => x.Data).Take(1).ToList();
                return last;
            }
            List<Arquivo> lstArquivos = BancoDeDados.GetByPeriodo(dataInicio, dataFim).AsEnumerable().
                Where(x => x.Field<string>("Estacao") == estacao).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

            var filterLastComunication = lstArquivos.GroupBy(d => d.Codigo)
                    .SelectMany(g => g.OrderByDescending(d => d.Data).Take(1)).ToList();

            return filterLastComunication;
        }

        public List<Arquivo> GetFirstComunicationByEstacao(string estacao, DateTime dataInicio, DateTime dataFim)
        {
            List<Arquivo> lstArquivo = new List<Arquivo>();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            //TODO - Fazer um filtro por estação 
            List<Arquivo> lstArquivos = BancoDeDados.GetByPeriodo(dataInicio, dataFim).AsEnumerable().
                Where(x => x.Field<string>("Estacao") == estacao).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

            var filterLastComunication = lstArquivos.GroupBy(d => d.Codigo)
                    .SelectMany(g => g.OrderBy(d => d.Data).Take(1)).ToList();

            return filterLastComunication;
        }

        public Arquivo GetFirstComunicationByCode(string codigo)
        {
            List<Arquivo> lstArquivo = new List<Arquivo>();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            //TODO - Fazer um filtro por estação 
            List<Arquivo> lstArquivos = BancoDeDados.GetAll().AsEnumerable().
                Where(x => x.Field<string>("Codigo") == codigo).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

            var first = lstArquivos.OrderBy(x => x.Data).FirstOrDefault();


            return first;
        }

        public List<Arquivo> ArquivosByCode(string estacao, string codigo, DateTime dataInicio, DateTime dataFim)
        {
            List<Arquivo> lstArquivo = new List<Arquivo>();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            //TODO - Fazer um filtro por estação 
            List<Arquivo> lstArquivos = BancoDeDados.GetByPeriodo(dataInicio, dataFim).AsEnumerable().
                Where(x => x.Field<string>("Codigo") == codigo).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

            var filterCodeByEstacao = lstArquivos.GroupBy(d => d.Id_pdv)
                    .SelectMany(g => g.OrderBy(d => d.Data)).ToList();

            return filterCodeByEstacao;
        }

        public List<Arquivo> ArquivosPeriodoByEstacaoENumero(string estacao, string numero, DateTime dataInicio, DateTime dataFim)
        {
            List<Arquivo> lstArquivo = new List<Arquivo>();
            ArquivoDataAccess BancoDeDados = new ArquivoDataAccess();
            BancoDeDados.OpenConnection();

            //TODO - Fazer um filtro por estação 
            List<Arquivo> lstArquivos = BancoDeDados.GetByPeriodo(dataInicio, dataFim).AsEnumerable().
                Where(x => x.Field<string>("Estacao") == estacao).Where(y => y.Field<string>("Numero") == numero).
                Select(x => new Arquivo
                {
                    Id = x.Field<int>("Id"),
                    Nome = x.Field<string>("Nome"),
                    Data = x.Field<DateTime>("Data"),
                    Sequencial = x.Field<int>("Sequencial"),
                    Codigo = x.Field<string>("Codigo"),
                    Numero = x.Field<string>("Numero"),
                    Estacao = x.Field<string>("Estacao"),
                    Status = x.Field<string>("Status"),
                }).ToList();

            var filterPeriodoEstacaoENumero = lstArquivos.GroupBy(d => d.Id_pdv)
                    .SelectMany(g => g.OrderBy(d => d.Data)).ToList();

            return filterPeriodoEstacaoENumero;
        }

        public Arquivo RequestSequencialArquivoDTO(string estacao, string codigo)
        {
            Arquivo arquivo = new Arquivo();

            arquivo.Estacao = estacao;
            arquivo.Codigo = codigo;

            return arquivo;
        }

    }
}

