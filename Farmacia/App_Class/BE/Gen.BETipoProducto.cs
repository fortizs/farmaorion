using System;

namespace Farmacia.App_Class.BE.General
{
	public class BETipoProducto : BEBase
	{

		private Int32 _IDTipoProducto;
		public Int32 IDTipoProducto
		{
			get { return _IDTipoProducto; }
			set { _IDTipoProducto = value; }
		}
		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}

	}
}