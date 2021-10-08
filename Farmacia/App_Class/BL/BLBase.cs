using System; 
using System.Configuration;
using System.Data;
using System.Data.SqlClient; 

namespace Farmacia.App_Class.BL
{
    public class BLBase
    {
        public string CadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
        }

        public string CadenaConexionTemporal()
        {
            return ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
        }

        public Int32 DuracionComando()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["cmdTimeOut"].ToString());
        }
        public SqlCommand ConexionCmd(System.String Procedimiento)
        {
            SqlConnection objcn = new SqlConnection(CadenaConexion());
            SqlCommand objcmd = new SqlCommand(Procedimiento , objcn);
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandTimeout = DuracionComando();
            return objcmd;
        }
        public SqlCommand ConexionGenericaCmd(System.String Consulta)
        {
            SqlConnection objcn = new SqlConnection(CadenaConexion());
            SqlCommand objcmd = new SqlCommand(Consulta, objcn);
            objcmd.CommandTimeout = DuracionComando();
            return objcmd;
        }
        public string CadenaConexionE(string Ext)
        {
            string conStr = "";
            switch (Ext)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Xls"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Xlsx"].ConnectionString;
                    break;
            }
            return conStr;
        }
    }
}
