using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Vehicular
{
	public class BETipoVehiculo : BEBase
	{
		private Int32 _IDTipoVehiculo;
		public Int32 IDTipoVehiculo
		{
			get { return _IDTipoVehiculo; }
			set { _IDTipoVehiculo = value; }
		}

		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
	}
}