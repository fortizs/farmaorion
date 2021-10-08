using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class CargarProductosExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string path = "";
            //SLDocument sl = new SLDocument(path);

            string ruta_carpeta = HttpContext.Current.Server.MapPath("~/Temporal");

            if (!Directory.Exists(ruta_carpeta))
            {
                Directory.CreateDirectory(ruta_carpeta);
            }





        }
    }
}