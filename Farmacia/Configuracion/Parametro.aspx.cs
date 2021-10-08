using System; 
using System.Web.UI; 

namespace Farmacia.Configuracion
{
    public partial class Parametro : PageBase
    {
        #region Load
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage(); 
            }
        }
        #endregion
         
    }
}