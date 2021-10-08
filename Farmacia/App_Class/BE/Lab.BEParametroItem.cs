using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Laboratorio
{
	public class BEParametroItem : BEBase
	{

		private Int32 _IDParametroItem;
		public Int32 IDParametroItem
		{
			get { return _IDParametroItem; }
			set { _IDParametroItem = value; }
		}
		private Int32 _IDParametro;
		public Int32 IDParametro
		{
			get { return _IDParametro; }
			set { _IDParametro = value; }
		}
		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		private String _Tipo;
		public String Tipo
		{
			get { return _Tipo; }
			set { _Tipo = value; }
		}
		private String _TipoNombre;
		public String TipoNombre
		{
			get { return _TipoNombre; }
			set { _TipoNombre = value; }
		}
		private String _Unidad;
		public String Unidad
		{
			get { return _Unidad; }
			set { _Unidad = value; }
		}
		private String _ValorReferencial;
		public String ValorReferencial
		{
			get { return _ValorReferencial; }
			set { _ValorReferencial = value; }
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