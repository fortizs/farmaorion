
using Farmacia.App_Class;
using Farmacia.App_Class.BE.Caja;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.Caja;
using Farmacia.App_Class.BL.General;
using Muebleria.App_Class.BL.Caja;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.CajaBanco
{
	public partial class ResumenCaja : PageBase
	{
		#region Load

		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				Inicio();
				CajaResumenListar();
			}
		}

		protected void Inicio()
		{
			CargarDDL(ddlIDCajaMecanica, new BLCaja().CajaMecanicaListar("", IDSucursal()), "IDCajaMecanica", "Nombre", true);
			txtFechaInicio.Text = DateTime.Today.ToShortDateString();
			txtFechaFin.Text = DateTime.Today.ToShortDateString();

			CargarDDL(ddlBIDFormaPago, new BLFormaPago().FormaPagoListar(), "IDFormaPago", "Nombre", true, Constantes.TODOS);

			txtBMFechaInicio.Text = DateTime.Today.ToShortDateString();
			txtBMFechaFin.Text = DateTime.Today.ToShortDateString();
		}
		#endregion

		#region Listar 
		private void CajaResumenListar()
		{
			BLCaja oBLc = new BLCaja();
			gvLista.DataSource = oBLc.CajaResumenListar(IDSucursal(), txtFechaInicio.Text, txtFechaFin.Text);
			gvLista.DataBind();
			upLista.Update();
		}
		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			CajaResumenListar();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			hdfCJIDCaja.Value = e.CommandArgument.ToString();
			BERetornoTran oBERetorno = new BERetornoTran();

			switch (e.CommandName)
			{
				case "EliminarCaja":
					oBERetorno = new BLCaja().CajaEliminar(Int32.Parse(hdfCJIDCaja.Value), IDSucursal());
					if (oBERetorno.Retorno == "1")
					{
						CajaResumenListar();
						msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
					}
					else
					{
						if (oBERetorno.Retorno != "-1")
						{
							msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
						}
						else {
							RegistrarLogSistema("gvLista_RowCommand()", oBERetorno.ErrorMensaje, true);
						}
					}
					break;
				case "CerrarCaja":
					String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ';' });
					hdfCJIDCaja.Value = cmdArgumentos[0].ToString();
					txtCaja.Text = cmdArgumentos[1].ToString();
					txtCajero.Text = cmdArgumentos[2].ToString();
					upLista.Update();
					IList Lista = new BLCaja().CorteCajaListar(Int32.Parse(hdfCJIDCaja.Value));
					if (Lista.Count == 0)
					{
						CorteCajaPreListar();
						gvCorteCajaPreListar.Visible = true;
						gvCorteCajaListar.Visible = false;
						btnGuardarCorteCaja.Visible = true;
					}
					else {
						CorteCajaListar();
						gvCorteCajaPreListar.Visible = false;
						gvCorteCajaListar.Visible = true;
						btnGuardarCorteCaja.Visible = false;
					}
					registrarScript("AbrirModal('ModalCorteCaja');");
					break;

				case "VerMovimiento":
					hdfCJIDCaja.Value = e.CommandArgument.ToString();
					MovimientoCajaListar();
					registrarScript("AbrirModal('ModalMovimientoCaja');");
					break;




			}
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			CajaResumenListar();
		}
		#endregion

		protected void btnNuevoAperturarCaja_Click(object sender, EventArgs e)
		{
			LimpiarAperturarCaja();
			registrarScript("AbrirModal('ModalAperturarCaja');");
		}

		protected void LimpiarAperturarCaja()
		{
			hdfIDCaja.Value = "0";
			ddlIDCajaMecanica.SelectedIndex = -1;
			txtFechaApertura.Text = DateTime.Today.ToShortDateString();
			txtMontoApertura.Text = "0.00";
			upAperturarCaja.Update();
		}

		protected void btnGuardarAperturarCaja_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (ddlIDCajaMecanica.SelectedValue == "0") validacion.Append("<div>Seleccione Caja</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BECaja oBE = new BECaja();
				oBE.IDSucursal = IDSucursal();
				oBE.IDCajaMecanica = Int32.Parse(ddlIDCajaMecanica.SelectedValue);
				oBE.IDUsuario = IDUsuario();
				oBE.MontoApertura = Decimal.Parse(txtMontoApertura.Text.Trim());
				oBE.FechaApertura = DateTime.Parse(txtFechaApertura.Text.Trim());

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLCaja().CajaApertura(oBE);

				if (oBERetorno.Retorno == "1")
				{
					CajaResumenListar();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('ModalAperturarCaja');");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarAperturarCaja_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarAperturarCaja_Click()", ex.ToString(), true);
			}
		}

		#region CorteCaja

		private void CorteCajaPreListar()
		{
			BLCaja oBLc = new BLCaja();
			gvCorteCajaPreListar.DataSource = oBLc.CorteCajaPreListar(Int32.Parse(hdfCJIDCaja.Value));
			gvCorteCajaPreListar.DataBind();
			PreCorteCajaTotal();
			upCorteCajaRegistrar.Update();
		}

		private void CorteCajaListar()
		{
			BLCaja oBLc = new BLCaja();
			gvCorteCajaListar.DataSource = oBLc.CorteCajaListar(Int32.Parse(hdfCJIDCaja.Value));
			gvCorteCajaListar.DataBind();
			CorteCajaTotal();
			upCorteCajaRegistrar.Update();
		}

		#endregion

		protected void txtContado_TextChanged(object sender, EventArgs e)
		{
			GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
			TextBox txtContado = (TextBox)row.FindControl("txtContado");
			TextBox txtCalculado = (TextBox)row.FindControl("txtCalculado");
			TextBox txtDiferencia = (TextBox)row.FindControl("txtDiferencia");
			TextBox txtRetiro = (TextBox)row.FindControl("txtRetiro");

			txtDiferencia.Text = (Decimal.Parse(txtContado.Text) - Decimal.Parse(txtCalculado.Text)).ToString("N");
			txtRetiro.Text = txtContado.Text;
			PreCorteCajaTotal();
		}

		protected void txtCalculado_TextChanged(object sender, EventArgs e)
		{
			GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
			TextBox txtContado = (TextBox)row.FindControl("txtContado");
			TextBox txtCalculado = (TextBox)row.FindControl("txtCalculado");
			TextBox txtDiferencia = (TextBox)row.FindControl("txtDiferencia");
			TextBox txtRetiro = (TextBox)row.FindControl("txtRetiro");

			txtDiferencia.Text = (Decimal.Parse(txtContado.Text) - Decimal.Parse(txtCalculado.Text)).ToString("N");
			txtRetiro.Text = Decimal.Parse(txtContado.Text).ToString("N");
			PreCorteCajaTotal();
		}

		protected void txtDiferencia_TextChanged(object sender, EventArgs e)
		{
			GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
			TextBox txtContado = (TextBox)row.FindControl("txtContado");
			TextBox txtCalculado = (TextBox)row.FindControl("txtCalculado");
			TextBox txtDiferencia = (TextBox)row.FindControl("txtDiferencia");
			TextBox txtRetiro = (TextBox)row.FindControl("txtRetiro");

			txtDiferencia.Text = (Decimal.Parse(txtContado.Text) - Decimal.Parse(txtCalculado.Text)).ToString("N");
			txtRetiro.Text = Decimal.Parse(txtContado.Text).ToString("N");
			PreCorteCajaTotal();
		}

		protected void btnGuardarCorteCaja_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder pValidaciones = new StringBuilder();
				BERetornoTran oBERetorno = new BERetornoTran();
				StringBuilder pRetornoError = new StringBuilder();


				//Insertando
				foreach (GridViewRow grvRow in gvCorteCajaPreListar.Rows)
				{
					TextBox txtContado = (TextBox)grvRow.FindControl("txtContado");
					TextBox txtCalculado = (TextBox)grvRow.FindControl("txtCalculado");
					TextBox txtDiferencia = (TextBox)grvRow.FindControl("txtDiferencia");
					TextBox txtRetiro = (TextBox)grvRow.FindControl("txtRetiro");
					HiddenField hdfIDMedioPago = (HiddenField)grvRow.FindControl("hdfIDMedioPago");

					BECaja oBE = new BECaja();
					oBE.IDCorteCaja = 0;
					oBE.IDCaja = Int32.Parse(hdfCJIDCaja.Value);
					oBE.IDMedioPago = Int32.Parse(hdfIDMedioPago.Value);
					oBE.Contado = Decimal.Parse(txtContado.Text.Trim());
					oBE.Calculado = Decimal.Parse(txtCalculado.Text.Trim());
					oBE.Diferencia = Decimal.Parse(txtDiferencia.Text.Trim());
					oBE.Retiro = Decimal.Parse(txtRetiro.Text.Trim());
					oBE.IDUsuario = IDUsuario();
					oBERetorno = new BLCaja().CorteCajaGuardar(oBE);
					if (oBERetorno.Retorno != "1")
					{
						pRetornoError.Append("<div>" + oBERetorno.ErrorMensaje + "</div>");
						break;
					}
				}

				if (pRetornoError.Length > 0)
				{
					msgbox(TipoMsgBox.warning, "Sistema", "<div style='text-align: left;'><div><b>Se han generado estas observaciones:</b></div>" + pRetornoError.ToString() + "</div>");
				}
				else
				{
					//Cerrando Caja
					BECaja oBE = new BECaja();
					oBE.IDCaja = Int32.Parse(hdfCJIDCaja.Value);
					oBE.IDSucursal = IDSucursal();
					oBE.IDUsuario = IDUsuario();
					oBE.Fecha = DateTime.Today;
					oBERetorno = new BLCaja().CajaCierre(oBE);
					//---------------------------------------
					CorteCajaListar();
					gvCorteCajaListar.Visible = true;
					gvCorteCajaPreListar.Visible = false;
					btnGuardarCorteCaja.Visible = false;
					upCorteCajaRegistrar.Update();
					CajaResumenListar();
					CorteCajaTotal();
					msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
				}
			}
			catch (Exception ex)
			{
				btnGuardarCorteCaja.Visible = true;
				RegistrarLogSistema("btnGuardarCorteCaja_Click()", ex.Message, true);
			}
		}

		protected void PreCorteCajaTotal()
		{
			Decimal lblPCJContado = 0;
			Decimal lblPCJCalculado = 0;
			Decimal lblPCJDiferencia = 0;
			Decimal lblPCJRetiro = 0;

			foreach (GridViewRow grvRow in gvCorteCajaPreListar.Rows)
			{
				TextBox txtContado = (TextBox)grvRow.FindControl("txtContado");
				TextBox txtCalculado = (TextBox)grvRow.FindControl("txtCalculado");
				TextBox txtDiferencia = (TextBox)grvRow.FindControl("txtDiferencia");
				TextBox txtRetiro = (TextBox)grvRow.FindControl("txtRetiro");

				lblPCJContado += Decimal.Parse(txtContado.Text.Trim());
				lblPCJCalculado += Decimal.Parse(txtCalculado.Text.Trim());
				lblPCJDiferencia += Decimal.Parse(txtDiferencia.Text.Trim());
				lblPCJRetiro += Decimal.Parse(txtRetiro.Text.Trim());

			}

			var lblTPCJContado = (Label)gvCorteCajaPreListar.FooterRow.FindControl("lblTPCJContado");
			lblTPCJContado.Text = lblPCJContado.ToString("N");

			var lblTPCJCalculado = (Label)gvCorteCajaPreListar.FooterRow.FindControl("lblTPCJCalculado");
			lblTPCJCalculado.Text = lblPCJCalculado.ToString("N");

			var lblTPCJDiferencia = (Label)gvCorteCajaPreListar.FooterRow.FindControl("lblTPCJDiferencia");
			lblTPCJDiferencia.Text = lblPCJDiferencia.ToString("N");

			var lblTPCJRetiro = (Label)gvCorteCajaPreListar.FooterRow.FindControl("lblTPCJRetiro");
			lblTPCJRetiro.Text = lblPCJRetiro.ToString("N");

		}

		protected void CorteCajaTotal()
		{
			Decimal lblTCJContado = 0;
			Decimal lblTCJCalculado = 0;
			Decimal lblTCJDiferencia = 0;
			Decimal lblTCJRetiro = 0;

			foreach (GridViewRow grvRow in gvCorteCajaListar.Rows)
			{
				Label lblCJContado = (Label)grvRow.FindControl("lblCJContado");
				Label lblCJCalculado = (Label)grvRow.FindControl("lblCJCalculado");
				Label lblCJDiferencia = (Label)grvRow.FindControl("lblCJDiferencia");
				Label lblCJRetiro = (Label)grvRow.FindControl("lblCJRetiro");

				lblTCJContado += Decimal.Parse(lblCJContado.Text.Trim());
				lblTCJCalculado += Decimal.Parse(lblCJCalculado.Text.Trim());
				lblTCJDiferencia += Decimal.Parse(lblCJDiferencia.Text.Trim());
				lblTCJRetiro += Decimal.Parse(lblCJRetiro.Text.Trim());

			}

			var lblTTCJContado = (Label)gvCorteCajaListar.FooterRow.FindControl("lblTTCJContado");
			lblTTCJContado.Text = lblTCJContado.ToString("N");

			var lblTTCJCalculado = (Label)gvCorteCajaListar.FooterRow.FindControl("lblTTCJCalculado");
			lblTTCJCalculado.Text = lblTCJCalculado.ToString("N");

			var lblTTCJDiferencia = (Label)gvCorteCajaListar.FooterRow.FindControl("lblTTCJDiferencia");
			lblTTCJDiferencia.Text = lblTCJDiferencia.ToString("N");

			var lblTTCJRetiro = (Label)gvCorteCajaListar.FooterRow.FindControl("lblTTCJRetiro");
			lblTTCJRetiro.Text = lblTCJRetiro.ToString("N");

		}

		#region MovimientoCaja

		protected void btnBuscarMovimiento_Click(object sender, EventArgs e)
		{
			MovimientoCajaListar();
		}

		private void MovimientoCajaListar()
		{
			BEMovimientoCaja oBE = new BLMovimientoCaja().MovimientoCajaTotalListar(Int32.Parse(hdfCJIDCaja.Value), txtFiltro.Text.Trim(), ddlBTipoMovimiento.SelectedValue, Int32.Parse(ddlBIDFormaPago.SelectedValue), txtBMFechaInicio.Text, txtBMFechaFin.Text, IDSucursal(), IDUsuario());
			txtTotalIngresos.Text = oBE.TotalIngreso.ToString("N");
			txtTotalEgresos.Text = oBE.TotalSalida.ToString("N");
			txtSaldo.Text = oBE.TotalSaldo.ToString("N");

			BLMovimientoCaja oBLc = new BLMovimientoCaja();
			gvMovimientoCajaListar.DataSource = oBLc.MovimientoCajaListar(Int32.Parse(hdfCJIDCaja.Value), txtFiltro.Text.Trim(), ddlBTipoMovimiento.SelectedValue, Int32.Parse(ddlBIDFormaPago.SelectedValue), txtBMFechaInicio.Text, txtBMFechaFin.Text, IDSucursal(), IDUsuario());
			gvMovimientoCajaListar.DataBind();
			upMovimientoCajaListar.Update();
		}

		protected void gvMovimientoCajaListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvMovimientoCajaListar.PageIndex = e.NewPageIndex;
			gvMovimientoCajaListar.SelectedIndex = -1;
			MovimientoCajaListar();
		}

		#endregion


	}
}