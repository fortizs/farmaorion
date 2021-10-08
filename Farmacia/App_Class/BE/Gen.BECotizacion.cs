using System;

namespace Farmacia.App_Class.BE.General
{
	public class BECotizacion : BEBase
	{ 
		private Int32 _IDCotizacion;
		public Int32 IDCotizacion
		{
			get { return _IDCotizacion; }
			set { _IDCotizacion = value; }
		}
		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}
		private Int32 _IDCliente;
		public Int32 IDCliente
		{
			get { return _IDCliente; }
			set { _IDCliente = value; }
		}
		private String _IDMoneda;
		public String IDMoneda
		{
			get { return _IDMoneda; }
			set { _IDMoneda = value; }
		}
		private Int32 _Anio;
		public Int32 Anio
		{
			get { return _Anio; }
			set { _Anio = value; }
		}
		private Int32 _NumeroCotizacion;
		public Int32 NumeroCotizacion
		{
			get { return _NumeroCotizacion; }
			set { _NumeroCotizacion = value; }
		}
		private DateTime _FechaCotizacion;
		public DateTime FechaCotizacion
		{
			get { return _FechaCotizacion; }
			set { _FechaCotizacion = value; }
		}
		private Decimal _CalculoIGV;
		public Decimal CalculoIGV
		{
			get { return _CalculoIGV; }
			set { _CalculoIGV = value; }
		}
		private Decimal _TotalOperacionGravada;
		public Decimal TotalOperacionGravada
		{
			get { return _TotalOperacionGravada; }
			set { _TotalOperacionGravada = value; }
		}
		private Decimal _TotalIGV;
		public Decimal TotalIGV
		{
			get { return _TotalIGV; }
			set { _TotalIGV = value; }
		}
		private Decimal _TotalDescuentos;
		public Decimal TotalDescuentos
		{
			get { return _TotalDescuentos; }
			set { _TotalDescuentos = value; }
		}
		private Decimal _TotalCotizacion;
		public Decimal TotalCotizacion
		{
			get { return _TotalCotizacion; }
			set { _TotalCotizacion = value; }
		}
		private Decimal _TipoCambio;
		public Decimal TipoCambio
		{
			get { return _TipoCambio; }
			set { _TipoCambio = value; }
		}
		private Int32 _IDEstado;
		public Int32 IDEstado
		{
			get { return _IDEstado; }
			set { _IDEstado = value; }
		}
		private String _Moneda;
		public String Moneda
		{
			get { return _Moneda; }
			set { _Moneda = value; }
		}
		private String _Sucursal;
		public String Sucursal
		{
			get { return _Sucursal; }
			set { _Sucursal = value; }
		}
		private String _ClienteNumeroDocumento;
		public String ClienteNumeroDocumento
		{
			get { return _ClienteNumeroDocumento; }
			set { _ClienteNumeroDocumento = value; }
		}
		private String _Cliente;
		public String Cliente
		{
			get { return _Cliente; }
			set { _Cliente = value; }
		}
		private String _EstadoNombre;
		public String EstadoNombre
		{
			get { return _EstadoNombre; }
			set { _EstadoNombre = value; }
		}
		private String _MotivoAnulacion;
		public String MotivoAnulacion
		{
			get { return _MotivoAnulacion; }
			set { _MotivoAnulacion = value; }
		}
		private String _NumeroCotizacionFormato;
		public String NumeroCotizacionFormato
		{
			get { return _NumeroCotizacionFormato; }
			set { _NumeroCotizacionFormato = value; }
		}
		

		private String _Filtro;
		public String Filtro
		{
			get { return _Filtro; }
			set { _Filtro = value; }
		}

		private String _FechaInicio;
		public String FechaInicio
		{
			get { return _FechaInicio; }
			set { _FechaInicio = value; }
		}

		private String _FechaFin;
		public String FechaFin
		{
			get { return _FechaFin; }
			set { _FechaFin = value; }
		}

		private String _Proceso;
		public String Proceso
		{
			get { return _Proceso; }
			set { _Proceso = value; }
		}
		

	}
}