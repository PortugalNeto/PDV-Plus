using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ArquivoDataAccess
    {
        private bool connectState = false;
        private MySqlConnection mConn;

        String sConnectionMysqlString;

        public ArquivoDataAccess()
        {
            string bdServer = "10.144.63.24";
            string bdServerDatabase = "sistema_pdv";
            string bdUser = "strange";
            string bdPwd = "supervia";

            sConnectionMysqlString = "Persist Security Info=False;server=" + bdServer + ";database=" + bdServerDatabase + ";uid=" + bdUser + ";pwd=" + bdPwd;
        }

        public bool OpenConnection()
        {
            try
            {
                mConn = new MySqlConnection(sConnectionMysqlString);
                connectState = true;
                return true;
            }
            catch (Exception e)
            {
                //Criar log
                //Tools.Log("Problema ao abrir conexão com o banco. BdArqImport.cs " + e.Message);
                return false;
            }
        }

        public bool CloseConnection()
        {
            if (mConn != null)
            {
                if (mConn.State == ConnectionState.Open)
                {
                    try
                    {
                        mConn.Close();
                        connectState = false;
                        return true;
                    }
                    catch (Exception e)
                    {
                        //Criar Log
                        //gerarLog(e, "Problema ao desconectar. ConnectionBD.cs");
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Save(string Nome, DateTime Data, int Id_pdv, int Sequencial)
        {
            try
            {
                try
                {
                    if (mConn.State != ConnectionState.Open)
                    {
                        mConn.Open();
                    }
                }
                catch (Exception e)
                {
                    //ToolsBLL.gerarLog(e, "Problema de Conexão ao banco. ConnectionBD.cs");
                    return false;
                }

                String sql = "insert into arquivo (Nome, Data, Sequencial, Id_pdv) " +
                             "values (@nome, @data, @sequencial, @Id_pdv)";

                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@nome", Nome);
                cmd.Parameters.AddWithValue("@data", Data);
                cmd.Parameters.AddWithValue("@sequencial", Sequencial);
                cmd.Parameters.AddWithValue("@Id_pdv", Id_pdv);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //ToolsBLL.gerarLog(e, "Problema no banco ao mudar o Password. ConnectionBD.cs");
                    return false;
                }
            }
            catch (Exception e)
            {
                //ToolsBLL.gerarLog(e, "Problema ao mudar o Password. ConnectionBD.cs");
                return false;
            }
            return true;
        }

        public DataTable GetAll()
        {
            try
            {
                try
                {
                    if (mConn.State != ConnectionState.Open)
                    {
                        mConn.Open();
                    }
                }
                catch (Exception e)
                {
                    //ToolsBLL.gerarLog(e, "Problema de Conexão ao banco. ConnectionBD.cs");
                    return null;
                }

                String sql = @"select * from arquivo a
                               inner join pdv p
                               on a.Id_pdv = p.Id";
                DataTable dtArq = new DataTable();
                MySqlDataAdapter daArq = new MySqlDataAdapter();

                daArq = new MySqlDataAdapter(sql, mConn);

                daArq.Fill(dtArq);

                CloseConnection();

                return dtArq;

            }
            catch (Exception e)
            {
                //ToolsBLL.gerarLog(e, "Problema ao autenticar. ConnectionBD.cs");
                return null;
            }
        }

        public DataTable GetByPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                try
                {
                    if (mConn.State != ConnectionState.Open)
                    {
                        mConn.Open();
                    }
                }
                catch (Exception e)
                {
                    //ToolsBLL.gerarLog(e, "Problema de Conexão ao banco. ConnectionBD.cs");
                    return null;
                }

                String sql = @"select * from arquivo a
                               inner join pdv p
                               on a.Id_pdv = p.Id 
                               where a.data >= @dataInicio and a.data <= @dataFim";
                DataTable dtArq = new DataTable();
                MySqlDataAdapter daArq = new MySqlDataAdapter();

                daArq = new MySqlDataAdapter(sql, mConn);
                daArq.SelectCommand.Parameters.AddWithValue("@dataInicio", dataInicio);
                daArq.SelectCommand.Parameters.AddWithValue("@dataFim", dataFim);

                daArq.Fill(dtArq);

                CloseConnection();

                return dtArq;

            }
            catch (Exception e)
            {
                //ToolsBLL.gerarLog(e, "Problema ao autenticar. ConnectionBD.cs");
                return null;
            }
        }

    }
}