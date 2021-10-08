using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Almacen
{
	public partial class Inventario : PageBase
	{
		#region Inicio

		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
                if (EsAdmin())
                {
                    CargarDDL(ddlBIDAlmacen, new BLAlmacen().AlmacenListar(0), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                    CargarDDL(ddlRegIDAlmacen, new BLAlmacen().AlmacenListar(0), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                }
                else {
                    CargarDDL(ddlBIDAlmacen, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                    CargarDDL(ddlRegIDAlmacen, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                }
				
				SeleccionarPrimerItem(ddlBIDAlmacen);
				SeleccionarPrimerItem(ddlRegIDAlmacen);
				InventarioFisicoListar();
			}
		}

		#endregion

		#region Lista

		private void InventarioFisicoListar()
		{
			BLInventarioFisico oBL = new BLInventarioFisico();
			gvInventarioFisicoListar.DataSource = oBL.InventarioFisicoListar(Int32.Parse(ddlBIDAlmacen.SelectedValue), txtBFiltro.Text.Trim());
			gvInventarioFisicoListar.DataBind();
			upLista.Update();
		}

		protected void gvInventarioFisicoListar_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
		{
			gvInventarioFisicoListar.PageIndex = e.NewPageIndex;
			gvInventarioFisicoListar.SelectedIndex = -1;
			InventarioFisicoListar();
		}

		protected void gvInventarioFisicoListar_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			hdfIDInventarioFisico.Value = e.CommandArgument.ToString();

			if (e.CommandName == "Editar")
			{
				InventarioFisicoSeleccionar();
				InventarioFisicoDetalleListar();
				upRegistro.Update();
				registrarScript("ActivarTabxId('tab2');");
			}

			if (e.CommandName == "Procesar")
			{
				BEInventarioFisico oBE = new BEInventarioFisico();
                BEInventarioFisico oBESeleccionar = new BEInventarioFisico();
                BLInventarioFisico oBL = new BLInventarioFisico();

				oBE.IDInventarioFisico = Int32.Parse(hdfIDInventarioFisico.Value);
                oBESeleccionar = oBL.InventarioFisicoSeleccionar(oBE.IDInventarioFisico);

				oBE.IDSucursal = oBESeleccionar.IDSucursal;
                oBE.IDEmpresa = IDEmpresa();
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.InventarioFisicoProcesar(oBE);

				if (oBERetorno.Retorno != "-1")
				{
					InventarioFisicoListar();
					upLista.Update();
					msgbox(TipoMsgBox.confirmation, "Sistema", "La operación se realizó con éxito");
				}
				else
				{
					RegistrarLogSistema("gvInventarioFisicoListar_RowCommand()", oBERetorno.ErrorMensaje, true);
				}
			}



		}

		protected void InventarioFisicoSeleccionar()
		{
			BEInventarioFisico oBE = new BLInventarioFisico().InventarioFisicoSeleccionar(Int32.Parse(hdfIDInventarioFisico.Value));
			txtRegNumeroInventario.Text = oBE.NumeroInventarioFormato;
			ddlRegIDAlmacen.SelectedValue = oBE.IDAlmacen.ToString();
			txtRegFechaInventario.Text = oBE.FechaInventario.ToShortDateString();
			txtRegObservacion.Text = oBE.Observacion.ToString();
			hdfProcesado.Value = oBE.Procesado.ToString();
			if (oBE.Procesado) {
				pnDetalleListar.Enabled = false;
            }
            else
            {
				pnDetalleListar.Enabled = true;

            }
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			InventarioFisicoListar();
		}

		#endregion

		#region Registro

		protected void lnkGuardarInventarioFisico_Click(object sender, EventArgs e)
		{
			StringBuilder pValidaciones = new StringBuilder();
			if (ddlRegIDAlmacen.SelectedValue == "0" || ddlRegIDAlmacen.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Almacen</div>");
			if (txtRegObservacion.Text.Trim().Length == 0) pValidaciones.Append("<div>Ingrese algun motivo.</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			BEInventarioFisico oBE = new BEInventarioFisico();
			BLInventarioFisico oBL = new BLInventarioFisico();

			oBE.IDInventarioFisico = Int32.Parse(hdfIDInventarioFisico.Value);
			oBE.IDAlmacen = Int32.Parse(ddlRegIDAlmacen.SelectedValue);
			oBE.Observacion = txtRegObservacion.Text;
			oBE.IDUsuario = IDUsuario();
			BERetornoTran oBERetorno = new BERetornoTran();

			if (oBE.IDInventarioFisico == 0)
			{
				oBERetorno = oBL.InventarioFisicoGuardar(oBE);
				hdfIDInventarioFisico.Value = oBERetorno.Retorno2;

			}
			else {

				StringBuilder pValidaciones2 = new StringBuilder();

				foreach (GridViewRow row in gvInventarioFisicoDetalleListar.Rows)
				{					
					Decimal IngresoConteo = Convert.ToDecimal(((TextBox)row.Cells[6].FindControl("txtIngresoConteo")).Text);
					if (IngresoConteo < 0) pValidaciones2.Append("<div>Cantidad ingresada no puede ser negativo</div>");

					if (pValidaciones2.Length > 0)
					{
						msgbox(TipoMsgBox.warning, pValidaciones2.ToString());
						return;
					}
				}



				oBERetorno = oBL.InventarioFisicoActualizar(oBE);
				if (oBERetorno.Retorno == "1") {

					BLInventarioFisicoDetalle oBLD = new BLInventarioFisicoDetalle();
					BERetornoTran RetornoDetalle = new BERetornoTran();

					foreach (GridViewRow row in gvInventarioFisicoDetalleListar.Rows)
					{						
						Int32 IDInventarioFisicoDetalle = Int32.Parse(((Label)row.Cells[6].FindControl("lblIDInventarioFisicoDetalle")).Text);
						Decimal IngresoConteo = Convert.ToDecimal(((TextBox)row.Cells[6].FindControl("txtIngresoConteo")).Text);

						BEInventarioFisicoDetalle oBEInventarioDetalle = new BEInventarioFisicoDetalle();
						

						oBEInventarioDetalle.IDInventarioFisicoDetalle = IDInventarioFisicoDetalle;
						oBEInventarioDetalle.IngresoConteo = IngresoConteo;

						RetornoDetalle = oBLD.InventarioFisicoDetalleActualizar(oBEInventarioDetalle);
						if (RetornoDetalle.Retorno != "1") {
							if (RetornoDetalle.Retorno == "-1")
							{
								msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
							}
							else {
								RegistrarLogSistema("InventarioFisicoDetalleActualizar()", oBERetorno.ErrorMensaje, true);
							}
								
						}

					}
				}

				


			}

			if (oBERetorno.Retorno == "1")
			{
				InventarioFisicoListar();
				InventarioFisicoDetalleListar();
				
				upRegistro.Update();
				msgbox(TipoMsgBox.confirmation, "Sistema", "La operación se realizó con éxito");
			}
			else
			{
				if (oBERetorno.Retorno == "2")
				{
					msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
				}
				else {
					RegistrarLogSistema("lnkGuardarInventarioFisico_Click()", oBERetorno.ErrorMensaje, true);
				}

			}
		}

		protected void lnkNuevoInventarioFisico_Click(object sender, EventArgs e)
		{
			LimpiarInventarioFisico();
		}

		protected void LimpiarInventarioFisico()
		{
			hdfIDInventarioFisico.Value = "0";
			hdfIDInventarioFisicoDetalle.Value = "0";
			txtRegNumeroInventario.Text = String.Empty;
			SeleccionarPrimerItem(ddlRegIDAlmacen);
			txtRegFechaInventario.Text = DateTime.Today.ToShortDateString();
			txtRegObservacion.Text = String.Empty;
			pnDetalleListar.Enabled = true;
			InventarioFisicoDetalleListar();

			upRegistro.Update();
		}

		#endregion

		#region Detalle

		private void InventarioFisicoDetalleListar()
		{
			BLInventarioFisicoDetalle oBL = new BLInventarioFisicoDetalle();
			gvInventarioFisicoDetalleListar.DataSource = oBL.InventarioFisicoDetalleListar(Int32.Parse(hdfIDInventarioFisico.Value));
			gvInventarioFisicoDetalleListar.DataBind();
		}

		protected void gvInventarioFisicoDetalleListar_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
		{
			gvInventarioFisicoDetalleListar.PageIndex = e.NewPageIndex;
			gvInventarioFisicoDetalleListar.SelectedIndex = -1;
			InventarioFisicoDetalleListar();
		}

		protected void gvInventarioFisicoDetalleListar_SelectedIndexChanged(object sender, EventArgs e)
		{
			hdfIDInventarioFisicoDetalle.Value = gvInventarioFisicoDetalleListar.SelectedDataKey["IDInventarioFisicoDetalle"].ToString();

			BEInventarioFisicoDetalle oBE = new BEInventarioFisicoDetalle();
			BLInventarioFisicoDetalle oBL = new BLInventarioFisicoDetalle();

			GridViewRow row = gvInventarioFisicoDetalleListar.SelectedRow;
			String txtIngresoConteo = ((TextBox)row.Cells[6].FindControl("txtIngresoConteo")).Text;

			oBE.IDInventarioFisicoDetalle = Int32.Parse(hdfIDInventarioFisicoDetalle.Value);
			oBE.IngresoConteo = Decimal.Parse(txtIngresoConteo);
			oBE.IDUsuario = IDUsuario();
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.InventarioFisicoDetalleActualizar(oBE);
			if (oBERetorno.Retorno != "-1")
			{
				msgbox(TipoMsgBox.confirmation, "Sistema", "La operación se realizó con éxito");
			}
			else
			{
				RegistrarLogSistema("gvInventarioFisicoListar_RowCommand()", oBERetorno.ErrorMensaje, true);
			}
		}

        #endregion

        protected void lnkImprimirConteo_Click(object sender, EventArgs e)
        {

        }

        protected void lnkGuardarTodo_Click(object sender, EventArgs e)
        {

        }

        protected void lnkImprimirDiferencia_Click(object sender, EventArgs e)
        {

        }
    }
}