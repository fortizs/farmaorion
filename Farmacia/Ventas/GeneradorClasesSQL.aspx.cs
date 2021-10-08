using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Farmacia
{
	public partial class GeneradorClasesSQLTunki : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ddlProcedimientosAlmacedos.DataBind();
                ParametrosListar();
            }
        }

        protected void ddlProcedimientosAlmacedos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParametrosListar();
        }

        private void ParametrosListar()
        {
            using (SqlConnection myConnection = new SqlConnection())
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;

                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = ddlProcedimientosAlmacedos.SelectedValue;
                myCommand.CommandType = CommandType.StoredProcedure;

                myConnection.Open();
                SqlCommandBuilder.DeriveParameters(myCommand);
                myConnection.Close();

                blInputParametros.Items.Clear();
                blOutputParametros.Items.Clear();

                List<SqlParameter> inputParamList = new List<SqlParameter>();

                StringBuilder pDataInput = new StringBuilder();
                StringBuilder pDataInputOutput = new StringBuilder();
                StringBuilder pDataOutput = new StringBuilder();
                StringBuilder pDataReturnValue = new StringBuilder();

                foreach (SqlParameter param in myCommand.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                    {
                        blInputParametros.Items.Add(param.ParameterName + " - " + param.SqlDbType.ToString());
                        inputParamList.Add(param);
                    }
                    else
                    {
                        blOutputParametros.Items.Add(param.ParameterName + " - " + param.SqlDbType.ToString());
                    }

                    if (param.Direction == ParameterDirection.Input)
                    {
                        if (param.SqlDbType.ToString() == "VarChar" || param.SqlDbType.ToString() == "Char")
                            pDataInput.Append(String.Format("cmd.Parameters.Add(\"@{0}\", SqlDbType.{1}, {2}).Value = BEParam.{0};", param.ParameterName.Replace("@", ""), param.SqlDbType.ToString(), param.Size.ToString()) + Environment.NewLine);
                        else
                            pDataInput.Append(String.Format("cmd.Parameters.Add(\"@{0}\", SqlDbType.{1}).Value = BEParam.{0};", param.ParameterName.Replace("@", ""), param.SqlDbType.ToString()) + Environment.NewLine);
                    }
                    else if (param.Direction == ParameterDirection.InputOutput)
                    {
                        if (param.SqlDbType.ToString() == "VarChar" || param.SqlDbType.ToString() == "Char")
                            pDataInputOutput.Append(String.Format("cmd.Parameters.Add(\"@{0}\", SqlDbType.{1}, {2}).Direction = ParameterDirection.Output;", param.ParameterName.Replace("@", ""), param.SqlDbType.ToString(), param.Size.ToString()) + Environment.NewLine);
                        else
                            pDataInputOutput.Append(String.Format("cmd.Parameters.Add(\"@{0}\", SqlDbType.{1}).Direction = ParameterDirection.Output;", param.ParameterName.Replace("@", ""), param.SqlDbType.ToString()) + Environment.NewLine);
                    }
                    else if (param.Direction == ParameterDirection.Output)
                    {
                        if (param.SqlDbType.ToString() == "VarChar" || param.SqlDbType.ToString() == "Char")
                            pDataOutput.Append(String.Format("cmd.Parameters.Add(\"@{0}\", SqlDbType.{1}, {2}).Direction = ParameterDirection.Output;", param.ParameterName.Replace("@", ""), param.SqlDbType.ToString(), param.Size.ToString()) + Environment.NewLine);
                        else
                            pDataOutput.Append(String.Format("cmd.Parameters.Add(\"@{0}\", SqlDbType.{1}).Direction = ParameterDirection.Output;", param.ParameterName.Replace("@", ""), param.SqlDbType.ToString()) + Environment.NewLine);
                    }
                    else
                    {
                        pDataReturnValue.Append("cmd.Parameters.Add(\"ReturnValue\", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;" + Environment.NewLine);
                    }
                }

                StringBuilder pDataGE = new StringBuilder();

                pDataGE.Append(Environment.NewLine);
                pDataGE.Append(String.Format("SqlCommand cmd = ConexionCmd(\"{0}\");", ddlProcedimientosAlmacedos.SelectedValue) + Environment.NewLine);
                pDataGE.Append(Environment.NewLine);
                pDataGE.Append(pDataInput.ToString());
                pDataGE.Append(pDataInputOutput.ToString());
                pDataGE.Append(pDataOutput.ToString());
                pDataGE.Append(pDataReturnValue.ToString());

                litInput.Text = pDataInput.ToString();
                litInputOutput.Text = pDataInputOutput.ToString();
                litOutput.Text = pDataOutput.ToString();
                litReturnValue.Text = pDataReturnValue.ToString();

                txtResultadoGE.Text = pDataGE.ToString();
                gvParametros.DataSource = inputParamList;
                gvParametros.DataBind();
            }

            pnResultadoSql.Visible = false;
        }

        protected void btnEjecutarSql_Click(object sender, EventArgs e)
        {
            pnResultadoSql.Visible = false;
            try
            {
                txtResultadoBE.Text = "";
                txtResultadoDL.Text = "";
             

                using (SqlConnection myConnection = new SqlConnection())
                {
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;

                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = myConnection;
                    myCommand.CommandText = ddlProcedimientosAlmacedos.SelectedValue;
                    myCommand.CommandType = CommandType.StoredProcedure;

                    foreach (GridViewRow gvRow in gvParametros.Rows)
                    {
                        string paramName = gvParametros.DataKeys[gvRow.RowIndex].Value.ToString();
                        object paramValue = DBNull.Value;

                        TextBox paramValueTextBox = gvRow.FindControl("ParameterValue") as TextBox;
                        if (!String.IsNullOrEmpty(paramValueTextBox.Text))
                            paramValue = paramValueTextBox.Text;

                        myCommand.Parameters.AddWithValue(paramName, paramValue);
                    }

                    myConnection.Open();

                    var dataReader = myCommand.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    if (dataTable != null)
                    {
                        StringBuilder pDataBE = new StringBuilder();
                        StringBuilder pDataDL = new StringBuilder();
                        StringBuilder pDataME = new StringBuilder();
                        StringBuilder pDataBL = new StringBuilder();
                        StringBuilder pReporteJScriptHead = new StringBuilder();
                        StringBuilder pReporteJScript = new StringBuilder();

                        pDataDL.Append(Environment.NewLine);
                        pDataBE.Append(Environment.NewLine);
                        pDataME.Append(Environment.NewLine);
                        pDataBL.Append(Environment.NewLine);

                        foreach (DataColumn dc in dataTable.Columns)
                        {
                            pDataBE.Append(String.Format("private {0} _{1};", dc.DataType.Name.ToString(), dc.Caption) + Environment.NewLine);
                            pDataBE.Append(String.Format("public {0} {1}", dc.DataType.Name.ToString(), dc.Caption) + Environment.NewLine);
                            pDataBE.Append("[" + Environment.NewLine);
                            pDataBE.Append(String.Format("get [ return _{0}; ]", dc.Caption) + Environment.NewLine);
                            pDataBE.Append(String.Format("set [ _{0} = value; ]", dc.Caption) + Environment.NewLine);
                            pDataBE.Append("]" + Environment.NewLine);
                            pDataBE.Replace('[', '{').Replace(']', '}');

                            pDataME.Append(String.Format("oBE.{1} = rd.Get{0}(rd.GetOrdinal(\"{1}\"));", dc.DataType.Name.ToString(), dc.Caption) + Environment.NewLine);
                            pReporteJScriptHead.Append(String.Format("filah += '<th>{0}</th>';", dc.Caption) + Environment.NewLine);
                            pReporteJScript.Append(String.Format("fila += '<td>' + obj[i][\"{0}\"] + '</td>';", dc.Caption) + Environment.NewLine);
                        }

                        if (pDataBE.Length > 0)
                        {
                            txtResultadoBE.Text = pDataBE.ToString();
                        }

                        if (pDataME.Length > 0)
                        {
                            if (ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("seleccion") || ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("obtener") || ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("total"))
                            {
                                pDataDL.Append(String.Format("public BEClase {0}(BEParam pBEParam)", ddlProcedimientosAlmacedos.SelectedValue.Split('.')[1]) + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("BEClase oBE = new BEClase();" + Environment.NewLine);
                                pDataDL.Append(String.Format("SqlCommand cmd = ConexionCmd(\"{0}\");", ddlProcedimientosAlmacedos.SelectedValue) + Environment.NewLine);
                                pDataDL.Append(litInput.Text);
                                pDataDL.Append("try" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("cmd.Connection.Open();" + Environment.NewLine);
                                pDataDL.Append("SqlDataReader rd = cmd.ExecuteReader();" + Environment.NewLine);
                                pDataDL.Append("if (rd.Read()) {" + Environment.NewLine);
                                pDataDL.Append(pDataME.ToString());
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("rd.Close();" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("catch (Exception ex)" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("throw ex;" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("finally" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("if ((cmd.Connection.State == ConnectionState.Open))" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("cmd.Connection.Close();" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("return oBE;" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                            }
                            else if (ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("lista") || ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("consulta"))
                            {
                                pDataDL.Append(String.Format("public IList {0}(BEParam pBEParam)", ddlProcedimientosAlmacedos.SelectedValue.Split('.')[1]) + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append(String.Format("SqlCommand cmd = ConexionCmd(\"{0}\");", ddlProcedimientosAlmacedos.SelectedValue) + Environment.NewLine);
                                pDataDL.Append(litInput.Text);
                                pDataDL.Append("ArrayList lista = new ArrayList();" + Environment.NewLine);
                                pDataDL.Append("try" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("cmd.Connection.Open();" + Environment.NewLine);
                                pDataDL.Append("SqlDataReader rd = cmd.ExecuteReader();" + Environment.NewLine);
                                pDataDL.Append("while (rd.Read())" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("BEClase oBE = new BEClase ();" + Environment.NewLine);
                                pDataDL.Append(pDataME.ToString());
                                pDataDL.Append("lista.Add(oBE);" + Environment.NewLine);
                                pDataDL.Append("oBE = null;" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("rd.Close();" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("catch (Exception ex)" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("throw ex;" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("finally" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("if ((cmd.Connection.State == ConnectionState.Open))" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("cmd.Connection.Close();" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("return lista;" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                            }
                            else if (ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("guarda") || ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("inserta") || ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("actualiza") || ddlProcedimientosAlmacedos.SelectedItem.Text.ToLower().Contains("asigna"))
                            {
                                pDataDL.Append(String.Format("public BERetornoTran {0}(BEParam pBEParam)", ddlProcedimientosAlmacedos.SelectedValue.Split('.')[1]) + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("BERetornoTran BERetorno = new BERetornoTran();" + Environment.NewLine);
                                pDataDL.Append(String.Format("SqlCommand cmd = ConexionCmd(\"{0}\");", ddlProcedimientosAlmacedos.SelectedValue) + Environment.NewLine);
                                pDataDL.Append(litInput.Text);
                                pDataDL.Append(litInputOutput.Text);
                                pDataDL.Append(litReturnValue.Text);
                                pDataDL.Append(litOutput.Text);
                                pDataDL.Append("try" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("cmd.Connection.Open();" + Environment.NewLine);
                                pDataDL.Append("cmd.ExecuteNonQuery();" + Environment.NewLine);
                                pDataDL.Append("BERetorno.Retorno = Convert.ToString(cmd.Parameters[\"ReturnValue\"].Value);" + Environment.NewLine);
                                pDataDL.Append("BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters[\"@ErrorMensaje\"].Value);" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("catch (Exception ex)" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("BERetorno.ErrorMensaje = ex.ToString();" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("finally" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("if (cmd.Connection.State == ConnectionState.Open)" + Environment.NewLine);
                                pDataDL.Append("{" + Environment.NewLine);
                                pDataDL.Append("cmd.Connection.Close();" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                                pDataDL.Append("return BERetorno;" + Environment.NewLine);
                                pDataDL.Append("}" + Environment.NewLine);
                            }

                            txtResultadoDL.Text = pDataDL.ToString();
                        }
						   
                        pnResultadoSql.Visible = true;
                    }
					 
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                msgbox(TipoMsgBox.error, "<b>Ocurrió un error</b>: " + ex.Message);
            }
        }
    }
}