using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using System;

namespace Farmacia
{
	public partial class Default : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{ 
			ValidarEstadoSesion();
			ValidarPoliticasSeguridad();
			if (!IsPostBack)
			{
				//ConfigPage(); 
			}
		}
	}
}