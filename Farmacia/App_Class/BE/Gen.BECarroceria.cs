using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
	public class BECarroceria : BEBase
	{
		private Int32 _IDCarroceria;
		public Int32 IDCarroceria
		{
			get { return _IDCarroceria; }
			set { _IDCarroceria = value; }
		}

		private String _Codigo;
		public String Codigo
		{
			get { return _Codigo; }
			set { _Codigo = value; }
		}

		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}

	}
}