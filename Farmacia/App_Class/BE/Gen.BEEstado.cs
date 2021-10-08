using System;

namespace Farmacia.App_Class.BE.General
{
	public class BEEstado : BEBase
	{
		private Int32 _IDEstado;
		public Int32 IDEstado
		{
			get { return _IDEstado; }
			set { _IDEstado = value; }
		}

		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}

		private String _Codigo;
		public String Codigo
		{
			get { return _Codigo; }
			set { _Codigo = value; }
		}
	}
}
