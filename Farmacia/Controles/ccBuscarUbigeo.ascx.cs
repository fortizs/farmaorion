
using Farmacia.App_Class.BL.General;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Controles
{
	public partial class ccBuscarUbigeo : System.Web.UI.UserControl
	{
		protected void btnBuscarUbigeo_Click(object sender, EventArgs e)
		{
			cBuscarUbigeo();
		}

		private void cBuscarUbigeo()
		{
			BLUbigeo oBL = new BLUbigeo();
			gvBuscarUbigeo.DataSource = oBL.UbigeoListarBuscar(txtBuscarUbigeo.Text.Trim());
			gvBuscarUbigeo.DataBind();
		}

		protected void gvBuscarUbigeo_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvBuscarUbigeo.PageIndex = e.NewPageIndex;
			cBuscarUbigeo();
		}

		protected void gvBuscarUbigeo_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				LinkButton btn = (LinkButton)e.Row.FindControl("lbSeleccionar");
				String IDUbigeo = DataBinder.Eval(e.Row.DataItem, "IDUbigeo").ToString();
				String Nombre = DataBinder.Eval(e.Row.DataItem, "NombreCompleto").ToString();
				btn.Attributes.Add("onclick", "SeleccionarUbigeo('" + IDUbigeo + "','" + Nombre + "'); return false;");
			}
		}

		public Int32 IDProducto
		{
			get
			{
				object text = ViewState["IDProducto"];
				return Int32.Parse(text != null ? text.ToString() : "0");
			}
			set
			{
				ViewState["IDProducto"] = value.ToString();
			}
		}
	}
}