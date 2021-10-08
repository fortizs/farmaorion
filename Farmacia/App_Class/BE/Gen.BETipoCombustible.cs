using System;

namespace Farmacia.App_Class.BE.General
{
	public class BETipoCombustible : BEBase
	{

		private Int32 _IDTipoCombustible;
		public Int32 IDTipoCombustible
		{
			get { return _IDTipoCombustible; }
			set { _IDTipoCombustible = value; }
		}
		 
		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		 
	}
}