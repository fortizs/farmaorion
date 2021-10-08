using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Inventario
{
	public class BEInventarioFisicoDetalle : BEBase
	{

		private Int32 _IDInventarioFisicoDetalle;
		public Int32 IDInventarioFisicoDetalle
		{
			get { return _IDInventarioFisicoDetalle; }
			set { _IDInventarioFisicoDetalle = value; }
		}

		private Int32 _IDInventarioFisico;
		public Int32 IDInventarioFisico
		{
			get { return _IDInventarioFisico; }
			set { _IDInventarioFisico = value; }
		}

		private Int32 _IDProducto;
		public Int32 IDProducto
		{
			get { return _IDProducto; }
			set { _IDProducto = value; }
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

		private String _Categoria;
		public String Categoria
		{
			get { return _Categoria; }
			set { _Categoria = value; }
		}

		private Int32 _IDLote;
		public Int32 IDLote
		{
			get { return _IDLote; }
			set { _IDLote = value; }
		}

		private String _NombreLote;
		public String NombreLote
		{
			get { return _NombreLote; }
			set { _NombreLote = value; }
		}

		private Decimal _StockLote;
		public Decimal StockLote
		{
			get { return _StockLote; }
			set { _StockLote = value; }
		}


		private Decimal _StockActual;
		public Decimal StockActual
		{
			get { return _StockActual; }
			set { _StockActual = value; }
		}

		private Decimal _IngresoConteo;
		public Decimal IngresoConteo
		{
			get { return _IngresoConteo; }
			set { _IngresoConteo = value; }
		}

		private String _UnidadMedida;
		public String UnidadMedida
		{
			get { return _UnidadMedida; }
			set { _UnidadMedida = value; }
		}


	}
}
