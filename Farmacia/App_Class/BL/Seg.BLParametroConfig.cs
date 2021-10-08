
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Seguridad;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLParametroConfig : BLBase 
    { 
        public IList ListarxEmpresa(Int32 pIDEmpresa)
        {            
            SqlCommand cmd = ConexionCmd("seg.ParametroConfigListarxEmpresa");
            BEParametroConfig oBE;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEParametroConfig();
                    oBE.IDParametro = rd.GetString(rd.GetOrdinal("IDParametro"));
                    oBE.Valor = rd.GetString(rd.GetOrdinal("Valor"));
                    lista.Add(oBE);
                    oBE = null;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
            }
            return lista;
			 
        }

		public String Parametro(String pIDParametro, IList pLista)
		{
			string Retorno = "";
			foreach (BEParametroConfig oBE in pLista)
			{
				if (oBE.IDParametro == pIDParametro)
				{
					Retorno = oBE.Valor;
					break;
				}
			}
			return Retorno;
		}
	}
}
