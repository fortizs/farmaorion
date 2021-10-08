using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Cobranza
{
	public partial class Cobranza : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargarDDL(ddlBIDCliente, new BLCliente().ClienteListar("", IDEmpresa()), "IDCliente", "RazonSocial", true, Constantes.TODOS);
				CargarDDL(ddlBIDEstadoCobranza, new BLEstado().EstadoListar("COB"), "Codigo", "Nombre", true, Constantes.TODOS);
				CargarDDL(ddlIDMedioPago, new BLMedioPago().MedioPagoListar(""), "IDMedioPago", "Nombre", true, Constantes.SELECCIONAR);
				txtFechaInicio.Text = DateTime.Today.ToShortDateString();
				txtFechaFin.Text = DateTime.Today.ToShortDateString();
				VentaListar();
			}
		}
		#endregion

		#region VentasListar

		private void VentaListar()
		{
			BEVenta oBE = new BEVenta();
			oBE.Filtro = txtFiltro.Text.Trim();
			oBE.IDSucursal = IDSucursal();
			oBE.IDCliente = Int32.Parse(ddlBIDCliente.SelectedValue);
			oBE.IDTipoComprobante = 0;
			oBE.IDEstadoCobranza = Int32.Parse(ddlBIDEstadoCobranza.SelectedValue);
			oBE.FechaInicio = txtFechaInicio.Text;
			oBE.FechaFin = txtFechaFin.Text;
			oBE.IDUsuario = IDUsuario();
			oBE.Accion = "VENTA";
			gvLista.DataSource = new BLVenta().VentasListar(oBE);
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			VentaListar();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			hdfIDVenta.Value = e.CommandArgument.ToString();

			if (e.CommandName == "Cobranza")
			{
				BEVenta oBE = new BLVenta().VentaSeleccionar(Int32.Parse(hdfIDVenta.Value));
				txtCliente.Text = oBE.Cliente;
				txtSerieNumero.Text = oBE.SerieNumero;
				txtFechaVenta.Text = oBE.FechaVenta.ToShortDateString();
				CobranzaListar();
				registrarScript("AbrirModal('ModalCobranzaListar');");
			}

		}


		protected void gvCobranzaListar_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			hdfIDCobranza.Value = e.CommandArgument.ToString();

			if (e.CommandName == "Anular")
			{
				try
				{
					BERetornoTran oBERetorno = new BERetornoTran();
					oBERetorno = new BLCobranza().CobranzaEliminar(Int32.Parse(hdfIDCobranza.Value), IDUsuario());

					if (oBERetorno.Retorno == "1")
					{
						CobranzaListar();
						VentaListar();
						msgbox(TipoMsgBox.confirmation, "Facturacion", "La Operación se ha realizado con éxito");

					}
					else
					{
						if (oBERetorno.Retorno != "-1")
						{
							msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
						}
						else {
							RegistrarLogSistema("gvCobranzaListar_RowCommand()", oBERetorno.ErrorMensaje, true);
						}
					}
				}
				catch (Exception ex)
				{
					RegistrarLogSistema("gvCobranzaListar_RowCommand()", ex.Message, true);
				}
			}

		}



		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			VentaListar();
		}

		private void CobranzaListar()
		{
			gvCobranzaListar.DataSource = new BLCobranza().CobranzaListar(Int32.Parse(hdfIDVenta.Value));
			gvCobranzaListar.DataBind();
			upCobranzaRegistro.Update();
		}
		#endregion

		protected void LimpiarCobranza()
		{
			hdfIDCobranza.Value = "0";
			txtFechaCobranza.Text = String.Empty;
			txtTotalPago.Text = "0.00";
			txtObservacion.Text = String.Empty;
			upFormulario.Update();
		}

		protected void btnNuevaCobranza_Click(object sender, EventArgs e)
		{
			LimpiarCobranza();
			registrarScript("AbrirModal('ModalCobranza');");
		}

		protected void lnkGuardarCobranza_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder pValidaciones = new StringBuilder();
				if (hdfIDVenta.Value == "0") pValidaciones.Append("<div>Seleccione una Venta a Anular</div>");
				//if (txtMotivoAnulacion.Text == "") pValidaciones.Append("<div>Ingrese Motivo Anulación</div>");

				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}

				BECobranza oBE = new BECobranza();
				oBE.IDCobranza = Int32.Parse(hdfIDCobranza.Value);
				oBE.IDVenta = Int32.Parse(hdfIDVenta.Value);
				oBE.IDMedioPago = Int32.Parse(ddlIDMedioPago.SelectedValue);
				oBE.IDBanco = 0;
				oBE.MontoCobrado = Decimal.Parse(txtTotalPago.Text);
				oBE.CuentaBancaria = "";
				oBE.Observacion = txtObservacion.Text.Trim();
				oBE.IDUsuario = IDUsuario();

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLCobranza().CobranzaGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					CobranzaListar();
					VentaListar();
					registrarScript("CerrarModal('ModalCobranza')");
					msgbox(TipoMsgBox.confirmation, "Facturacion", "La Operación se ha realizado con éxito");

				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("lnkGuardarCobranza_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("lnkGuardarCobranza_Click()", ex.Message, true);
			}
		}
	}
}