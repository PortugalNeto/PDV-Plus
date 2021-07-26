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

                String sql = @"select * from pdv";
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


                String sql = "select * from pdv where Estacao = @estacao";

                DataTable dtPDV = new DataTable();
                MySqlDataAdapter daPDV = new MySqlDataAdapter(sql, mConn);
                daPDV.SelectCommand.Parameters.AddWithValue("@estacao", estacao);
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
        
        public bool GetByCode(string codigo)
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

                String sql = "select * from pdv where codigo = @codigo";

                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                MySqlDataReader myReader = cmd.ExecuteReader();

                try
                {
                    if (myReader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        public bool Save(string estacao, string numero, string codigo)
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

                String sql = "insert into pdv (Estacao, Numero, Codigo)" +
                             "values (@estacao, @numero, @codigo)";

                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@estacao", estacao);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@codigo", codigo);

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

        public bool Update(int id, string estacao, string numero, string codigo)
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

                String sql = "UPDATE pdv " +
                                "SET Estacao = @estacao, " +
                                    "Numero = @numero, " +
                                        "Codigo = @codigo " +
                                            "where Id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@estacao", estacao);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@codigo", codigo);
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

                String sql = "DELETE from pdv " +
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