
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Collections;
using System.Web.UI;

namespace Farmacia
{
	public partial class Site : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				BEUsuario oBEUsuario = (BEUsuario)Session["BEUsuario"];
				if (oBEUsuario != null)
				{
					//lEmpresa.Text = oBEUsuario.Empresa;
					//lSucursal.Text = oBEUsuario.Sucursal;
					ltNombreSucursal.Text = oBEUsuario.Sucursal; 
				    lUsuario.Text = oBEUsuario.NombreCompleto;

					BEColaborador oBE = new BLColaborador().SeleccionarColaborador(oBEUsuario.IDColaborador);

					ltfoto.Text = "<img src='" + oBE.RutaNombreImagenFotoCompleto + "' id='imgFoto' style='width: 46px; border-radius: 50px; border: 2px solid #62ff00;background-color: #62ff00;box-shadow: 0 0 20px #6bff1f;' runat='server' class='img-fluid mr-2' alt='avatar'>";
					 CargarMenu(oBEUsuario.IDUsuario, oBEUsuario.IDEmpresa, oBEUsuario.IDPerfil);
					AlertaBajoStock();
                    AlertaProductoxVencer();

                    if (Session["IDModuloSuperior"] == null) //Trabajando en Memoria
					{
						Session["IDModuloSuperior"] = 0;
					}
					//CargarMenuSuperior(oBEUsuario.IDUsuario, Int32.Parse(Session["IDModuloSuperior"].ToString()));
				}
			}
		} 

		private void CargarMenu(Int32 pIDUsuario, Int32 pIDEmpresa, Int32 pIDPerfil)
		{

			int NroSMenu = 0;
			BLModulo oBLModulo = new BLModulo();
			IList ListaModulo = oBLModulo.ModuloListarxUsuario(pIDUsuario);
			BLMenu oBLMenu = new BLMenu();
			IList Lista = oBLMenu.Listar(pIDUsuario, 1);
			String sMenuModulo = "";
			foreach (BEModulo oBEModulo in ListaModulo)
			{
				if (oBEModulo.Acceso)
				{

					sMenuModulo += "<li class='menu'>" + Environment.NewLine;
					sMenuModulo += "<a href='#V" + oBEModulo.IDModulo.ToString() + "Modulo' data-toggle='collapse' aria-expanded='false' id=\"mo" + oBEModulo.IDModulo.ToString() + "\" class='dropdown-toggle'>" + Environment.NewLine;
					sMenuModulo += "<div class=''>" + Environment.NewLine;
					sMenuModulo += "<i class='" + oBEModulo.Icono + "'></i>" + Environment.NewLine;
					//sMenuModulo += "<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-home'>" + Environment.NewLine;
					//sMenuModulo += "<path d='M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z'></path><polyline points='9 22 9 12 15 12 15 22'></polyline></svg>" + Environment.NewLine;
					sMenuModulo += "<span>" + oBEModulo.Nombre + "</span>" + Environment.NewLine;
					sMenuModulo += "</div>" + Environment.NewLine;
					sMenuModulo += "<div>" + Environment.NewLine;
					sMenuModulo += "<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-chevron-right'>" + Environment.NewLine;
					sMenuModulo += "<polyline points='9 18 15 12 9 6'></polyline></svg>" + Environment.NewLine;
					sMenuModulo += "</div>" + Environment.NewLine;
					sMenuModulo += "</a>" + Environment.NewLine;

					sMenuModulo += "    <ul class='collapse submenu list-unstyled' id='V" + oBEModulo.IDModulo.ToString() + "Modulo' data-parent='#accordionExample'>" + Environment.NewLine;
					foreach (BEMenu oBEMenu in Lista)
					{
						if (oBEMenu.IDModulo == oBEModulo.IDModulo && oBEMenu.IDMenuPadre == 0)
						{
							if (oBEMenu.Visible == true)
							{
								NroSMenu = NroSubMenu(oBEMenu.IDMenu, Lista);
								sMenuModulo += "<li>" + Environment.NewLine;

								if (NroSMenu > 0)
								{
									sMenuModulo += "    <a id=\"sm" + oBEMenu.IDMenu.ToString() + "\" onclick='GenerarMenuSuperior(" + oBEModulo.IDModulo + ","+ pIDUsuario + ")' href=\"" + Page.ResolveClientUrl(oBEMenu.Url) + "\">" + oBEMenu.Nombre + "<span class='pull-right-container'></span></a>" + Environment.NewLine;

								}
								else {
									sMenuModulo += "    <a id=\"sm" + oBEMenu.IDMenu.ToString() + "\" onclick='GenerarMenuSuperior(" + oBEModulo.IDModulo + "," + pIDUsuario + ")' href=\"" + Page.ResolveClientUrl(oBEMenu.Url) + "\">" + oBEMenu.Nombre + "</a>" + Environment.NewLine;

								}

								if (NroSMenu > 0)
								{
									sMenuModulo += "    <ul class='collapse submenu list-unstyled' id='dashboard' data-parent='#accordionExample'>" + Environment.NewLine;
									sMenuModulo = this.AgregarSubMenuSis(Lista, sMenuModulo, oBEMenu.IDMenu);
									sMenuModulo += "    </ul>" + Environment.NewLine;
								}
								sMenuModulo += "</li>" + Environment.NewLine;
							}
						}
					}
					sMenuModulo += "    </ul>" + Environment.NewLine;
					sMenuModulo += "</li>" + Environment.NewLine;
				}
			}
			lMenuSis.Text = sMenuModulo;
		}

		private void AlertaBajoStock()
		{
			BLProducto oBL = new BLProducto();
			ltCantidadProductoStockBajo.Text = oBL.CantidadAlertaProductoxSucursal(1).ToString();			
		}

        private void AlertaProductoxVencer()
        {
            BLProducto oBL = new BLProducto();
            ltCantidadProductoxVencer.Text = oBL.AlertaCantidadProductosVencidos(1).ToString();
        }



        private string AgregarSubMenuSis(IList Lista, string sSubMenu, int IDMenu)
		{
			int NroSMenu = 0;
			foreach (BEMenu oBEMenu in Lista)
			{
				if ((oBEMenu.IDMenuPadre == IDMenu))
				{
					if (oBEMenu.Visible == true)
					{
						NroSMenu = NroSubMenu(oBEMenu.IDMenu, Lista);
						if (NroSMenu > 0)
						{
							sSubMenu += "<li><a id=\"sm" + oBEMenu.IDMenu.ToString() + "\" href=\"" + Page.ResolveClientUrl(oBEMenu.Url) + "\">" + oBEMenu.Nombre + "<span class='pull-right-container'></span></a>" + Environment.NewLine;
						}
						else {
							sSubMenu += "<li><a id=\"sm" + oBEMenu.IDMenu.ToString() + "\" href=\"" + Page.ResolveClientUrl(oBEMenu.Url) + "\">" + oBEMenu.Nombre + "</a>" + Environment.NewLine;
						}

						if (NroSMenu > 0)
						{
							sSubMenu += "<ul class='collapse submenu list-unstyled' id='dashboard' data-parent='#accordionExample'>" + Environment.NewLine;
							sSubMenu = AgregarSubMenuSis(Lista, sSubMenu, oBEMenu.IDMenu);
							sSubMenu += "</ul>" + Environment.NewLine;
						}
						sSubMenu += "</li>" + Environment.NewLine;
					}
				}
			}
			return sSubMenu;
		}

		public int NroSubMenu(int pIDMenu, IList pLista)
		{
			int c = 0;
			foreach (BEMenu oBE in pLista)
			{
				if (oBE.IDMenuPadre == pIDMenu)
				{
					c += 1;
				}
			}
			return c;
		}

		protected void btnGenerarMenuSuperior_Click(object sender, EventArgs e)
		{
			//CargarMenuSuperior(Int32.Parse(hfIDUsuario.Value), Int32.Parse(hfIDModuloSuperior.Value));
		}


		protected void CargarMenuSuperior(Int32 pIDUsuario, Int32 pIDModuloSuperior) {

			String sMenuxModulo = "";
			BLMenu oBLMenu = new BLMenu();
			IList Lista = oBLMenu.MenuListarxModuloUsuario(pIDUsuario, pIDModuloSuperior); 
			Session["IDModuloSuperior"] = pIDModuloSuperior;
			foreach (BEMenu oBEMenu in Lista)
			{
				sMenuxModulo += "<li>";
				sMenuxModulo += "<a href=\"" + Page.ResolveClientUrl(oBEMenu.Url) + "\">";
				sMenuxModulo += "<i class='" + oBEMenu.Icono + "'></i>";
				sMenuxModulo += "<p>" + oBEMenu.Nombre + "</p>";
				sMenuxModulo += "</a>";
				sMenuxModulo += "</li>";
			}

			//ltMenuSuperior.Text = sMenuxModulo;
			//UpdatePanel1.Update();
		}
	}
}