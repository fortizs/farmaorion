using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Laboratorio
{
	public class BEParametro : BEBase
	{

		private Int32 _IDParametro;
		public Int32 IDParametro
		{
			get { return _IDParametro; }
			set { _IDParametro = value; }
		}
		private Int32 _IDProducto;
		public Int32 IDProducto
		{
			get { return _IDProducto; }
			set { _IDProducto = value; }
		}
		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		private String _TipoResultado;
		public String TipoResultado
		{
			get { return _TipoResultado; }
			set { _TipoResultado = value; }
		}
		private String _TipoResultadoNombre;
		public String TipoResultadoNombre
		{
			get { return _TipoResultadoNombre; }
			set { _TipoResultadoNombre = value; }
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