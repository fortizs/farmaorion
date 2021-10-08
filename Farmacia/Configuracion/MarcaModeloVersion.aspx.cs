using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Vehicular;
using Farmacia.App_Class.BL.Vehicular;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
	public partial class MarcaModeloVersion : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
			}
		}

		private void CargaInicial()
		{
			CargarDDL(ddlOrigen, new BLMarcaModeloVersion().MarcaOrigenListar(), "Origen", "Origen", true, Constantes.TODOS);
			MarcaListar();
			ModeloListar();
			ModeloVersionListar();

			MarcaListarMantenimiento();
			ModeloListarMantenimiento();
			ModeloVersionListarMantenimiento();
		}

		private void MarcaListar()
		{
			CargarDDL(ddlMarca, new BLMarcaModeloVersion().MarcaListar(ddlOrigen.SelectedValue, 0), "IDMarca", "Marca", true, Constantes.TODOS);
		}

		private void ModeloListar()
		{
			CargarDDL(ddlModelo, new BLMarcaModeloVersion().ModeloListar(ddlOrigen.SelectedValue, Int32.Parse(ddlMarca.SelectedValue), 0), "IDModelo", "Modelo", true, Constantes.TODOS);
		}

		private void ModeloVersionListar()
		{
			CargarDDL(ddlSubModelo, new BLMarcaModeloVersion().ModeloVersionListar(ddlOrigen.SelectedValue, Int32.Parse(ddlMarca.SelectedValue), Int32.Parse(ddlModelo.SelectedValue), 0), "IDModeloVersion", "ModeloVersion", true, Constantes.TODOS);
		}

		protected void ddlOrigen_SelectedIndexChanged(object sender, EventArgs e)
		{
			MarcaListar();
			ModeloListar();
			ModeloVersionListar();
		}

		protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
		{
			ModeloListar();
			ModeloVersionListar();
		}

		protected void ddlModelo_SelectedIndexChanged(object sender, EventArgs e)
		{
			ModeloVersionListar();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			MarcaListarMantenimiento();
			ModeloListarMantenimiento();
			ModeloVersionListarMantenimiento();
		}
		#endregion

		#region Marca
		private void MarcaListarMantenimiento()
		{
			BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
			oBE.Estado = true;

			BLMarcaModeloVersion oBL = new BLMarcaModeloVersion();
			IList Lista = oBL.MarcaListar(ddlOrigen.SelectedValue, Int32.Parse(ddlMarca.SelectedValue));
			Lista.Insert(0, oBE);
			gvListaMarca.DataSource = Lista;
			gvListaMarca.DataBind();
			gvListaMarca.EditIndex = -1;
		}

		protected void gvListaMarca_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvListaMarca.PageIndex = e.NewPageIndex;
			gvListaMarca.EditIndex = -1;
			MarcaListarMantenimiento();
		}

		protected void gvListaMarca_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvListaMarca.EditIndex = e.NewEditIndex;
			MarcaListarMantenimiento();

			DropDownList pddlOrigen = (DropDownList)gvListaMarca.Rows[e.NewEditIndex].FindControl("ddlOrigen");
			Label plblOrigen = (Label)gvListaMarca.Rows[e.NewEditIndex].FindControl("lblOrigen");

			CargarDDL(pddlOrigen, new BLMarcaModeloVersion().MarcaOrigenListar(), "Origen", "Origen", true, Constantes.SELECCIONAR); 
			pddlOrigen.SelectedValue = (String.IsNullOrWhiteSpace(plblOrigen.Text) ? "0" : plblOrigen.Text);
		}

		protected void gvListaMarca_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			gvListaMarca.EditIndex = -1;
			MarcaListarMantenimiento();
		}

		protected void gvListaMarca_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				Int32 IDMarca = Int32.Parse(gvListaMarca.DataKeys[e.RowIndex].Value.ToString());
				DropDownList pddlOrigen = (DropDownList)gvListaMarca.Rows[e.RowIndex].FindControl("ddlOrigen");
				TextBox ptxtMarca = (TextBox)gvListaMarca.Rows[e.RowIndex].FindControl("txtMarca");
				ptxtMarca.Text = (String.IsNullOrWhiteSpace(ptxtMarca.Text.Trim()) ? "" : ptxtMarca.Text.Trim());
				CheckBox pchkEstado = (CheckBox)gvListaMarca.Rows[e.RowIndex].FindControl("chkEstado");

				StringBuilder validacion = new StringBuilder();
				if (ptxtMarca.Text.Length == 0) validacion.Append("<div>Ingrese la Marca.</div>");
				if (pddlOrigen.SelectedValue == "0") validacion.Append("<div>Seleccione el Origen.</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
				BLMarcaModeloVersion oBL = new BLMarcaModeloVersion();
				oBE.IDMarca = IDMarca;
				oBE.Marca = ptxtMarca.Text;
				oBE.Origen = pddlOrigen.SelectedValue;
				oBE.Estado = pchkEstado.Checked;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.MarcaGuardar(oBE);
				if (oBERetorno.Retorno != "-1")
				{
					if (oBERetorno.Retorno == "0")
					{
						msgbox(TipoMsgBox.information, "La Marca fue registrada anteriormente.");
					}
					else
					{
						gvListaMarca.EditIndex = -1;
						gvListaModelo.EditIndex = -1;
						gvListaModeloVersion.EditIndex = -1;
						MarcaListar();
						ModeloListar();
						ModeloVersionListar();
						MarcaListarMantenimiento();
						ModeloListarMantenimiento();
						ModeloVersionListarMantenimiento();

						msgbox(TipoMsgBox.confirmation, "Marca registrada con éxito.");
					}
				}
				else
				{
					RegistrarLogSistema("gvListaMarca_RowUpdating()", oBERetorno.ErrorMensaje, true);
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvListaMarca_RowUpdating()", ex.Message, true);
			}
		}

		protected void gvListaMarca_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				msgbox(TipoMsgBox.information, "Función no habilitada.");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvListaMarca_RowDeleting()", ex.Message, true);
			}
		}
		#endregion

		#region Modelo
		private void ModeloListarMantenimiento()
		{
			BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
			oBE.Estado = true;

			BLMarcaModeloVersion oBL = new BLMarcaModeloVersion();
			IList Lista = oBL.ModeloListar(ddlOrigen.SelectedValue, Int32.Parse(ddlMarca.SelectedValue), Int32.Parse(ddlModelo.SelectedValue));
			Lista.Insert(0, oBE);
			gvListaModelo.DataSource = Lista;
			gvListaModelo.DataBind();
			gvListaModelo.EditIndex = -1;
		}

		protected void gvListaModelo_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvListaModelo.PageIndex = e.NewPageIndex;
			gvListaModelo.EditIndex = -1;
			ModeloListarMantenimiento();
		}

		protected void gvListaModelo_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvListaModelo.EditIndex = e.NewEditIndex;
			ModeloListarMantenimiento();

			DropDownList pddlMarca = (DropDownList)gvListaModelo.Rows[e.NewEditIndex].FindControl("ddlMarca");
			Label plblIDMarca = (Label)gvListaModelo.Rows[e.NewEditIndex].FindControl("lblIDMarca");
			DropDownList pddlTipoVehiculo = (DropDownList)gvListaModelo.Rows[e.NewEditIndex].FindControl("ddlTipoVehiculo");
			Label plblIDTipoVehiculo = (Label)gvListaModelo.Rows[e.NewEditIndex].FindControl("lblIDTipoVehiculo");

			CargarDDL(pddlMarca, new BLMarcaModeloVersion().MarcaListar("0", 0), "IDMarca", "Marca", true);
			pddlMarca.SelectedValue = (String.IsNullOrWhiteSpace(plblIDMarca.Text) ? "0" : plblIDMarca.Text);

			CargarDDL(pddlTipoVehiculo, new BLTipoVehiculo().Listar(), "IDTipoVehiculo", "Nombre", true);
			pddlTipoVehiculo.SelectedValue = (String.IsNullOrWhiteSpace(plblIDTipoVehiculo.Text) ? "0" : plblIDTipoVehiculo.Text);
		}

		protected void gvListaModelo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			gvListaModelo.EditIndex = -1;
			ModeloListarMantenimiento();
		}

		protected void gvListaModelo_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				Int32 IDModelo = Int32.Parse(gvListaModelo.DataKeys[e.RowIndex].Value.ToString());
				DropDownList pddlMarca = (DropDownList)gvListaModelo.Rows[e.RowIndex].FindControl("ddlMarca");
				TextBox ptxtModelo = (TextBox)gvListaModelo.Rows[e.RowIndex].FindControl("txtModelo");
				ptxtModelo.Text = (String.IsNullOrWhiteSpace(ptxtModelo.Text.Trim()) ? "" : ptxtModelo.Text.Trim());
				DropDownList pddlTipoVehiculo = (DropDownList)gvListaModelo.Rows[e.RowIndex].FindControl("ddlTipoVehiculo");
				TextBox ptxtNumeroAsientos = (TextBox)gvListaModelo.Rows[e.RowIndex].FindControl("txtNumeroAsientos");
				ptxtNumeroAsientos.Text = (String.IsNullOrWhiteSpace(ptxtNumeroAsientos.Text.Trim()) ? "" : ptxtNumeroAsientos.Text.Trim());
				CheckBox pchkEstado = (CheckBox)gvListaModelo.Rows[e.RowIndex].FindControl("chkEstado");

				StringBuilder validacion = new StringBuilder();
				if (pddlMarca.SelectedValue == "0") validacion.Append("<div>Seleccione el Marca.</div>");
				if (ptxtModelo.Text.Length == 0) validacion.Append("<div>Ingrese la Modelo.</div>");
				if (pddlTipoVehiculo.SelectedValue == "0") validacion.Append("<div>Seleccione el Tipo.</div>");
				if (!esInt32(ptxtNumeroAsientos.Text)) validacion.Append("<div>Ingrese el Nro. Asientos.</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
				BLMarcaModeloVersion oBL = new BLMarcaModeloVersion();
				oBE.IDModelo = IDModelo;
				oBE.IDMarca = Int32.Parse(pddlMarca.SelectedValue);
				oBE.IDTipoVehiculo = Int32.Parse(pddlTipoVehiculo.SelectedValue);
				oBE.Modelo = ptxtModelo.Text;
				oBE.NumeroAsientos = Int32.Parse(ptxtNumeroAsientos.Text);
				oBE.Estado = pchkEstado.Checked;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.ModeloGuardar(oBE);
				if (oBERetorno.Retorno != "-1")
				{
					if (oBERetorno.Retorno == "0")
					{
						msgbox(TipoMsgBox.information, "El Modelo fue registrado anteriormente.");
					}
					else
					{
						gvListaModelo.EditIndex = -1;
						gvListaModeloVersion.EditIndex = -1;
						ModeloListar();
						ModeloVersionListar();
						ModeloListarMantenimiento();
						ModeloVersionListarMantenimiento();
						msgbox(TipoMsgBox.confirmation, "Modelo registrado con éxito.");
					}
				}
				else
				{
					RegistrarLogSistema("gvListaModelo_RowUpdating()", oBERetorno.ErrorMensaje, true);
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvListaModelo_RowUpdating()", ex.Message, true);
			}
		}

		protected void gvListaModelo_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				msgbox(TipoMsgBox.information, "Función no habilitada.");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvListaModelo_RowDeleting()", ex.Message, true);
			}
		}
		#endregion

		#region ModeloVersion
		private void ModeloVersionListarMantenimiento()
		{
			BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
			oBE.Estado = true;

			BLMarcaModeloVersion oBL = new BLMarcaModeloVersion();
			IList Lista = oBL.ModeloVersionListar(ddlOrigen.SelectedValue, Int32.Parse(ddlMarca.SelectedValue), Int32.Parse(ddlModelo.SelectedValue), Int32.Parse(ddlSubModelo.SelectedValue));
			Lista.Insert(0, oBE);
			gvListaModeloVersion.DataSource = Lista;
			gvListaModeloVersion.DataBind();
			gvListaModeloVersion.EditIndex = -1;
		}

		protected void gvListaModeloVersion_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvListaModeloVersion.PageIndex = e.NewPageIndex;
			gvListaModeloVersion.EditIndex = -1;
			ModeloVersionListarMantenimiento();
		}

		protected void gvListaModeloVersion_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvListaModeloVersion.EditIndex = e.NewEditIndex;
			ModeloVersionListarMantenimiento();

			DropDownList pddlMarca = (DropDownList)gvListaModeloVersion.Rows[e.NewEditIndex].FindControl("ddlMarca");
			Label plblIDMarca = (Label)gvListaModeloVersion.Rows[e.NewEditIndex].FindControl("lblIDMarca");
			DropDownList pddlModelo = (DropDownList)gvListaModeloVersion.Rows[e.NewEditIndex].FindControl("ddlModelo");
			Label plblIDModelo = (Label)gvListaModeloVersion.Rows[e.NewEditIndex].FindControl("lblIDModelo");

			CargarDDL(pddlMarca, new BLMarcaModeloVersion().MarcaListar("0", 0), "IDMarca", "Marca", true);
			pddlMarca.SelectedValue = (String.IsNullOrWhiteSpace(plblIDMarca.Text) ? "0" : plblIDMarca.Text);

			CargarDDL(pddlModelo, new BLMarcaModeloVersion().ModeloListar("0", Int32.Parse(pddlMarca.SelectedValue), 0), "IDModelo", "Modelo", true);
			pddlModelo.SelectedValue = (String.IsNullOrWhiteSpace(plblIDModelo.Text) ? "0" : plblIDModelo.Text);
		}

		protected void gvListaModeloVersion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			gvListaModeloVersion.EditIndex = -1;
			ModeloVersionListarMantenimiento();
		}

		protected void gvListaModeloVersion_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				Int32 IDModeloVersion = Int32.Parse(gvListaModeloVersion.DataKeys[e.RowIndex].Value.ToString());
				DropDownList pddlMarca = (DropDownList)gvListaModeloVersion.Rows[e.RowIndex].FindControl("ddlMarca");
				DropDownList pddlModelo = (DropDownList)gvListaModeloVersion.Rows[e.RowIndex].FindControl("ddlModelo");
				TextBox ptxtModeloVersion = (TextBox)gvListaModeloVersion.Rows[e.RowIndex].FindControl("txtModeloVersion");
				CheckBox pchkEstado = (CheckBox)gvListaModeloVersion.Rows[e.RowIndex].FindControl("chkEstado");

				StringBuilder validacion = new StringBuilder();
				if (pddlMarca.SelectedValue == "0") validacion.Append("<div>Seleccione la Marca.</div>");
				if (pddlModelo.SelectedValue == "0") validacion.Append("<div>Seleccione el Modelo.</div>");
				if (ptxtModeloVersion.Text.Length == 0) validacion.Append("<div>Ingrese el Sub Modelo.</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
				BLMarcaModeloVersion oBL = new BLMarcaModeloVersion();
				oBE.IDModeloVersion = IDModeloVersion;
				oBE.IDModelo = Int32.Parse(pddlModelo.SelectedValue);
				oBE.ModeloVersion = ptxtModeloVersion.Text;
				oBE.Estado = pchkEstado.Checked;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.ModeloVersionGuardar(oBE);
				if (oBERetorno.Retorno != "-1")
				{
					if (oBERetorno.Retorno == "0")
					{
						msgbox(TipoMsgBox.information, "El Sub Modelo fue registrado anteriormente.");
					}
					else
					{
						gvListaModeloVersion.EditIndex = -1;
						ModeloVersionListar();
						ModeloVersionListarMantenimiento();
						msgbox(TipoMsgBox.confirmation, "Sub Modelo registrado con éxito.");
					}
				}
				else
				{
					RegistrarLogSistema("gvListaModeloVersion_RowUpdating()", oBERetorno.ErrorMensaje, true);
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvListaModeloVersion_RowUpdating()", ex.Message, true);
			}
		}

		protected void gvListaModeloVersion_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				msgbox(TipoMsgBox.information, "Función no habilitada.");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvListaModeloVersion_RowDeleting()", ex.Message, true);
			}
		}

		protected void ddlListaModeloVersionMarca_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList pddlMarca = (DropDownList)sender;
			GridViewRow row = (GridViewRow)pddlMarca.NamingContainer;
			if (row != null)
			{
				DropDownList pddlModelo = (DropDownList)row.FindControl("ddlModelo");
				if (pddlMarca.SelectedValue != "0")
				{
					CargarDDL(pddlModelo, new BLMarcaModeloVersion().ModeloListar("0", Int32.Parse(pddlMarca.SelectedValue), 0), "IDModelo", "Modelo", true);
				}
				else
				{
					CargarDDL(pddlModelo, new BLMarcaModeloVersion().ModeloListar("-1", -1, -1), "IDModelo", "Modelo", true);
				}
			}
		}
		#endregion
	}
}