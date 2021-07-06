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
    public class Arquivo
    {
        public int Id { get; set; }
        public string Nome  { get; set; }
        public DateTime Data { get; set; }

        public void SaveFromDirectory()      // Vai às pastas e grava a InfoFile em banco
            
        {
            BancoDeDados Bd = new BancoDeDados();
            Bd.OpenConnection();
            DirectoryInfo dir = new DirectoryInfo(@"C:\Origem");    // Diretório Exemplo!!!
            Arquivo Arq = new Arquivo();

            foreach (FileInfo item in dir.GetFiles("*.*", SearchOption.AllDirectories).
                       Where(x => x.Name.EndsWith("pdv")))

                        {
                            string dataArquivo = item.Name.Substring(62, 6);
                            string horaArquivo = item.Name.Substring(71, 6);

                            DateTime dataHoraArquivo = DateTime.ParseExact(dataArquivo + " " + horaArquivo, "yyMMdd HHmmss", CultureInfo.InvariantCulture);
                            Bd.SaveArquivo(item.Name, dataHoraArquivo);
                       
                        }

        }

        public List<Arquivo> GetAll()       // Vai ao banco e pega todos os últimos registros de cada PDV

        {
            BancoDeDados bd = new BancoDeDados();
            bd.OpenConnection();
            DataTable dtGetAll = bd.GetAllArquivo();


            //TODO - Pode ser um DTO
            List<Arquivo> lstArquivo = dtGetAll.AsEnumerable().
            Select(x => new Arquivo
            {
                Id = x.Field<int>("Id"),
                Nome = x.Field<string>("Nome"),     // InfoFile.Name()
                Data = x.Field<DateTime>("Data"),   // InfoFile.Date()
                
            }).ToList();
            
            return lstArquivo;
        }
    }
}

