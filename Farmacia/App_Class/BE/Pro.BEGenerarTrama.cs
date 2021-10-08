using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Farmacia.App_Class.BE.Proceso
{
	public class BEGenerarTrama : BEBase
	{

		private Int32 _IDProducto = 0;
		public Int32 IDProducto
		{
			get { return _IDProducto; }
			set { _IDProducto = value; }
		}

		private Int32 _IDEstructuraProducto = 0;
		public Int32 IDEstructuraProducto
		{
			get { return _IDEstructuraProducto; }
			set { _IDEstructuraProducto = value; }
		}

		private string _Formato = string.Empty;
		public string Formato
		{
			get { return _Formato; }
			set { _Formato = value; }
		}

		private Int32 _Registros = 0;
		public Int32 Registros
		{
			get { return _Registros; }
			set { _Registros = value; }
		}
		private string _IDMoneda = string.Empty;

		public string IDMoneda
		{
			get { return _IDMoneda; }
			set { _IDMoneda = value; }
		}

		private DateTime _FechaInicio;
		public DateTime FechaInicio
		{
			get { return _FechaInicio; }
			set { _FechaInicio = value; }
		}
		private DateTime _FechaFin;
		public DateTime FechaFin
		{
			get { return _FechaFin; }
			set { _FechaFin = value; }
		}

		private DateTime _FechaRegistro;

		public DateTime FechaRegistro
		{
			get { return _FechaRegistro; }
			set { _FechaRegistro = value; }
		}

		private string _NombreFecha = string.Empty;
		public string NombreFecha
		{
			get { return _NombreFecha; }
			set { _NombreFecha = value; }
		}

		private string _NombreCampoFecha = string.Empty;
		public string NombreCampoFecha
		{
			get { return _NombreCampoFecha; }
			set { _NombreCampoFecha = value; }
		}

		private string _Poliza = string.Empty;
		public string Poliza
		{
			get { return _Poliza; }
			set { _Poliza = value; }
		}

		private string _Adicional1 = string.Empty;
		public string Adicional1
		{
			get { return _Adicional1; }
			set { _Adicional1 = value; }
		}

		private string _Adicional2 = string.Empty;
		public string Adicional2
		{
			get { return _Adicional2; }
			set { _Adicional2 = value; }
		}

		private string _Adicional3 = string.Empty;
		public string Adicional3
		{
			get { return _Adicional3; }
			set { _Adicional3 = value; }
		}

		private string _IDTipoEjecucion = string.Empty;
		public string IDTipoEjecucion
		{
			get { return _IDTipoEjecucion; }
			set { _IDTipoEjecucion = value; }
		}

		private StringBuilder _Trama;
		public StringBuilder Trama
		{
			get { return _Trama; }
			set { _Trama = value; }
		}

		private Int32 _NumeroRegistros;
		public Int32 NumeroRegistros
		{
			get { return _NumeroRegistros; }
			set { _NumeroRegistros = value; }
		}

		private Int32 _NumeroRechazos;
		public Int32 NumeroRechazos
		{
			get { return _NumeroRechazos; }
			set { _NumeroRechazos = value; }
		}

		private Int32 _IDTramaLog;
		public Int32 IDTramaLog
		{
			get { return _IDTramaLog; }
			set { _IDTramaLog = value; }
		}

		private DataTable _Tramadt;

		public DataTable Tramadt
		{
			get { return _Tramadt; }
			set { _Tramadt = value; }
		}

		private DataSet _Tramads;

		public DataSet Tramads
		{
			get { return _Tramads; }
			set { _Tramads = value; }
		}


		public string IDConciliacionCupones { get; set; }
	}
}
