using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Proceso
{
	public class BETramaLog : BEBase
	{ 
		private int _IDTramaLog = 0;

		public int IDTramaLog
		{
			get { return _IDTramaLog; }
			set { _IDTramaLog = value; }
		}

		private Int32 _IDEstructuraProceso = 0;

		public Int32 IDEstructuraProceso
		{
			get { return _IDEstructuraProceso; }
			set { _IDEstructuraProceso = value; }
		}
		 
		private Int32 _IDEstructuraDetalle;
		public Int32 IDEstructuraDetalle
		{
			get { return _IDEstructuraDetalle; }
			set { _IDEstructuraDetalle = value; }
		}
		 
		private string _RutaArchivo = string.Empty;

		public string RutaArchivo
		{
			get { return _RutaArchivo; }
			set { _RutaArchivo = value; }
		}

		private String _IDTipoEntidad = string.Empty;

		public String IDTipoEntidad
		{
			get { return _IDTipoEntidad; }
			set { _IDTipoEntidad = value; }
		}

		private String _IDEntidad = string.Empty;

		public String IDEntidad
		{
			get { return _IDEntidad; }
			set { _IDEntidad = value; }
		}
		private string _NombreArchivo = string.Empty;

		public string NombreArchivo
		{
			get { return _NombreArchivo; }
			set { _NombreArchivo = value; }
		}
		private string _IDTipoEjecucion = string.Empty;
		public string IDTipoEjecucion
		{
			get { return _IDTipoEjecucion; }
			set { _IDTipoEjecucion = value; }
		}

		private int _CantidadI = 0;
		public int CantidadI
		{
			get { return _CantidadI; }
			set { _CantidadI = value; }
		}
		private int _CantidadR = 0;

		public int CantidadR
		{
			get { return _CantidadR; }
			set { _CantidadR = value; }
		}
		private bool _Estado = false;

		public bool Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}
		private int _IDUsuario = 0;

		public int IDUsuario
		{
			get { return _IDUsuario; }
			set { _IDUsuario = value; }
		}

		private DateTime _Fecha;
		public DateTime Fecha
		{
			get { return _Fecha; }
			set { _Fecha = value; }
		}

		private int _IDProducto = 0;
		public int IDProducto
		{
			get { return _IDProducto; }
			set { _IDProducto = value; }
		}

		private DateTime _FechaDesde;
		public DateTime FechaDesde
		{
			get { return _FechaDesde; }
			set { _FechaDesde = value; }
		}

		private DateTime _FechaHasta;
		public DateTime FechaHasta
		{
			get { return _FechaHasta; }
			set { _FechaHasta = value; }
		}

		private string _Tamano = string.Empty;
		public string Tamano
		{
			get { return _Tamano; }
			set { _Tamano = value; }
		}

		private string _TipoArchivo = string.Empty;
		public string TipoArchivo
		{
			get { return _TipoArchivo; }
			set { _TipoArchivo = value; }
		}

		private string _IDEstadoEvento = String.Empty;
		public string IDEstadoEvento
		{
			get { return _IDEstadoEvento; }
			set { _IDEstadoEvento = value; }
		}

		private string _EstadoEvento = String.Empty;
		public string EstadoEvento
		{
			get { return _EstadoEvento; }
			set { _EstadoEvento = value; }
		}

		private int _IDEstadoConciliacion = 0;
		public int IDEstadoConciliacion
		{
			get { return _IDEstadoConciliacion; }
			set { _IDEstadoConciliacion = value; }
		}

		private bool _ArchivoLog = false;
		public bool ArchivoLog
		{
			get { return _ArchivoLog; }
			set { _ArchivoLog = value; }
		}



		private bool _EliminarCarga = false;
		public bool EliminarCarga
		{
			get { return _EliminarCarga; }
			set { _EliminarCarga = value; }
		}

		private string _RutaArchivoLog = string.Empty;

		public string RutaArchivoLog
		{
			get { return _RutaArchivoLog; }
			set { _RutaArchivoLog = value; }
		}
		private string _NombreArchivoLog = string.Empty;

		public string NombreArchivoLog
		{
			get { return _NombreArchivoLog; }
			set { _NombreArchivoLog = value; }
		}


	}
}
