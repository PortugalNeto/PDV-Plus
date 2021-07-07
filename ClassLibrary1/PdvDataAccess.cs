using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class PdvDataAccess
    {
        private bool connectState = false;
        private MySqlConnection mConn;

        String sConnectionMysqlString;

        public PdvDataAccess()
        {
            string bdServer = "localhost";
            string bdServerDatabase = "sistema_pdv";
            string bdUser = "root";
            string bdPwd = "";

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

                String sql = "select * from pdv";
                DataTable dtPDV = new DataTable();
                MySqlDataAdapter daPDV = new MySqlDataAdapter();

                daPDV = new MySqlDataAdapter(sql, mConn);

                daPDV.Fill(dtPDV);

                CloseConnection();

                return dtPDV;

            }
            catch (Exception e)
            {
                //ToolsBLL.gerarLog(e, "Problema ao autenticar. ConnectionBD.cs");
                return null;
            }
        }

        public DataTable GetByEstacao(string estacao)
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

                
                String sql = "select * from dt_pdv "
                                + "where Estacao = @estacao";

                DataTable dtPDV = new DataTable();
                MySqlDataAdapter daPDV = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@estacao", estacao);

                daPDV = new MySqlDataAdapter(sql, mConn);

                daPDV.Fill(dtPDV);

                CloseConnection();

                return dtPDV;

            }
            catch (Exception e)
            {
                //ToolsBLL.gerarLog(e, "Problema ao autenticar. ConnectionBD.cs");
                return null;
            }
        }

        public bool Save(string Estacao, string Pdv, string Codigo)
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

                String sql = "insert into dt_pdv (Estacao, PDV_nome, Codigo)" +
                             "values (@estacao, @pdv, @codigo)";

                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@estacao", Estacao);
                cmd.Parameters.AddWithValue("@pdv", Pdv);
                cmd.Parameters.AddWithValue("@codigo", Codigo);

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

        public bool Update(int Id, string Estacao, string Pdv, string Codigo)
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

                String sql = "UPDATE dt_pdv " +
		                        "SET Estacao = @estacao, " +
			                        "PDV_nome = @pdv, " +
			                            "Codigo = @codigo " +
				                            "where Id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@estacao", Estacao);
                cmd.Parameters.AddWithValue("@pdv", Pdv);
                cmd.Parameters.AddWithValue("@codigo", Codigo);
                cmd.Parameters.AddWithValue("@id", Id);

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

        public bool Delete(int id)
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

                String sql = "DELETE from dt_pdv " +
                                "WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@id", id);

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

    }
}