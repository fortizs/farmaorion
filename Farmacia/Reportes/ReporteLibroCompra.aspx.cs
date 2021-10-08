using Farmacia.App_Class;
using Farmacia.App_Class.BL.Compras;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
    public partial class ReporteLibroCompra : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(Int32.Parse(Session["IDEmpresa"].ToString())), "IDSucursal", "Sucursal", true, Constantes.TODOS);
                CargaPeriodo();
                Listar();
            }
        }

        private void CargaPeriodo()
        {
            Int32 AnioActual = DateTime.Now.Year;
            for (int i = AnioActual; i > 2014; i--)
            {
                ddlPeriodoAnio.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            String[] ListaMes = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames;
            String Mes = "";

            for (int i = 0; i < ListaMes.Length - 1; i++)
            {
                Mes = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ListaMes[i]);
                ddlPeriodoMes.Items.Add(new ListItem(Mes, (i + 1).ToString("0#")));
            }
            ddlPeriodoMes.SelectedValue = DateTime.Now.Month.ToString("D0");
        }

        private void Listar()
        {
            BLCompras oBL = new BLCompras();
            gvLista.DataSource = oBL.ReporteRegistroCompras(Int32.Parse(ddlIDSucursal.SelectedValue),(ddlPeriodoAnio.SelectedValue.Trim() + ddlPeriodoMes.SelectedValue.Trim()));
            gvLista.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            Listar();
        }
    }
}