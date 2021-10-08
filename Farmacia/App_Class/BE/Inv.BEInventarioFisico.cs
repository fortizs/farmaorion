using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Inventario
{
	public class BEInventarioFisico : BEBase
	{

		private Int32 _IDInventarioFisico;
		public Int32 IDInventarioFisico
		{
			get { return _IDInventarioFisico; }
			set { _IDInventarioFisico = value; }
		}

		private Int32 _IDAlmacen;
		public Int32 IDAlmacen
		{
			get { return _IDAlmacen; }
			set { _IDAlmacen = value; }
		}

		private String _NumeroInventarioFormato;
		public String NumeroInventarioFormato
		{
			get { return _NumeroInventarioFormato; }
			set { _NumeroInventarioFormato = value; }
		}

		private Int32 _NumeroInventario;
		public Int32 NumeroInventario
		{
			get { return _NumeroInventario; }
			set { _NumeroInventario = value; }
		}

		private String _Observacion;
		public String Observacion
		{
			get { return _Observacion; }
			set { _Observacion = value; }
		}

		private DateTime _FechaInventario;
		public DateTime FechaInventario
		{
			get { return _FechaInventario; }
			set { _FechaInventario = value; }
		}

		private Boolean _Procesado;
		public Boolean Procesado
		{
			get { return _Procesado; }
			set { _Procesado = value; }
		}

		private String _Almacen;
		public String Almacen
		{
			get { return _Almacen; }
			set { _Almacen = value; }
		}

	}
}
