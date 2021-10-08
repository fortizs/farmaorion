
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;

namespace Farmacia.Controles.WebService
{
	/// <summary>
	/// Descripción breve de WSConsultas
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
	[System.Web.Script.Services.ScriptService]
	public class WSConsultas : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hola a todos";
		}

		[WebMethod()]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public List<BEWebService> ProductoSucursalListar(Int32 pIDSucursal, String pBuscar)
		{
			BLWebService pBL = new BLWebService();
			return pBL.ProductoSucursalListar(pIDSucursal, pBuscar);
		}
	}
}
