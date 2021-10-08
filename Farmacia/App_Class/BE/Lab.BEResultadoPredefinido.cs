using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Laboratorio
{
	public class BEResultadoPredefinido : BEBase
	{

		private Int32 _IDResultadoPredefinido;
		public Int32 IDResultadoPredefinido
		{
			get { return _IDResultadoPredefinido; }
			set { _IDResultadoPredefinido = value; }
		}
		private Int32 _IDGenerico;
		public Int32 IDGenerico
		{
			get { return _IDGenerico; }
			set { _IDGenerico = value; }
		}
		private String _Generico;
		public String Generico
		{
			get { return _Generico; }
			set { _Generico = value; }
		}
		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		private Int32 _Posicion;
		public Int32 Posicion
		{
			get { return _Posicion; }
			set { _Posicion = value; }
		}
		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}
		private DateTime _FechaCreacion;
		public DateTime FechaCreacion
		{
			get { return _FechaCreacion; }
			set { _FechaCreacion = value; }
		}

	}
}