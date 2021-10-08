using System;

namespace Farmacia.App_Class.BE.Seguridad
{
	public class BEParametro : BEBase
	{
		private String _IDParametro;
		public String IDParametro
		{
			get { return _IDParametro; }
			set { _IDParametro = value; }
		}

		private String _Descripcion;
		public String Descripcion
		{
			get { return _Descripcion; }
			set { _Descripcion = value; }
		}

		private String _ValorDefecto;
		public String ValorDefecto
		{
			get { return _ValorDefecto; }
			set { _ValorDefecto = value; }
		}
		 
	}
}