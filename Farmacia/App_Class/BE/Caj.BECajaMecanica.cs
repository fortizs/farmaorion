using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Caja
{
	public class BECajaMecanica : BEBase 
	{

		private Int32 _IDCajaMecanica;
		public Int32 IDCajaMecanica
		{
			get { return _IDCajaMecanica; }
			set { _IDCajaMecanica = value; }
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
		private String _Responsable;
		public String Responsable
		{
			get { return _Responsable; }
			set { _Responsable = value; }
		}
		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}
		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}
		private String _Sucursal;
		public String Sucursal
		{
			get { return _Sucursal; }
			set { _Sucursal = value; }
		}

	}
}