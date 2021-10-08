using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Caja
{
	public class BEMovimientoCaja : BEBase
	{

		private Int32 _IDMovimientoCaja;
		public Int32 IDMovimientoCaja
		{
			get { return _IDMovimientoCaja; }
			set { _IDMovimientoCaja = value; }
		}
		private String _CajaMecanica;
		public String CajaMecanica
		{
			get { return _CajaMecanica; }
			set { _CajaMecanica = value; }
		}
		private String _Cajero;
		public String Cajero
		{
			get { return _Cajero; }
			set { _Cajero = value; }
		}

		
		private String _NombreTipoMovimiento;
		public String NombreTipoMovimiento
		{
			get { return _NombreTipoMovimiento; }
			set { _NombreTipoMovimiento = value; }
		}
		private DateTime _FechaMovimiento;
		public DateTime FechaMovimiento
		{
			get { return _FechaMovimiento; }
			set { _FechaMovimiento = value; }
		}
		private String _TipoComprobante;
		public String TipoComprobante
		{
			get { return _TipoComprobante; }
			set { _TipoComprobante = value; }
		}
		private String _SiglaTipoComprobante;
		public String SiglaTipoComprobante
		{
			get { return _SiglaTipoComprobante; }
			set { _SiglaTipoComprobante = value; }
		}
		private String _SerieNumero;
		public String SerieNumero
		{
			get { return _SerieNumero; }
			set { _SerieNumero = value; }
		}
		private Decimal _Monto;
		public Decimal Monto
		{
			get { return _Monto; }
			set { _Monto = value; }
		}
		private String _Observacion;
		public String Observacion
		{
			get { return _Observacion; }
			set { _Observacion = value; }
		}
		private String _UsuarioCreacion;
		public String UsuarioCreacion
		{
			get { return _UsuarioCreacion; }
			set { _UsuarioCreacion = value; }
		}
		private String _Sucursal;
		public String Sucursal
		{
			get { return _Sucursal; }
			set { _Sucursal = value; }
		}
		 
		private Int32 _IDCaja;
		public Int32 IDCaja
		{
			get { return _IDCaja; }
			set { _IDCaja = value; }
		}
		 
		private Int32 _IDOperacion;
		public Int32 IDOperacion
		{
			get { return _IDOperacion; }
			set { _IDOperacion = value; }
		}
		private String _TipoMovimiento;
		public String TipoMovimiento
		{
			get { return _TipoMovimiento; }
			set { _TipoMovimiento = value; }
		}
		 
		private Int32 _IDTipoComprobante;
		public Int32 IDTipoComprobante
		{
			get { return _IDTipoComprobante; }
			set { _IDTipoComprobante = value; }
		}

		private Int32 _IDFormaPago;
		public Int32 IDFormaPago
		{
			get { return _IDFormaPago; }
			set { _IDFormaPago = value; }
		}
		 
		private String _Serie;
		public String Serie
		{
			get { return _Serie; }
			set { _Serie = value; }
		}
		private String _Numero;
		public String Numero
		{
			get { return _Numero; }
			set { _Numero = value; }
		}
		private String _IDMoneda;
		public String IDMoneda
		{
			get { return _IDMoneda; }
			set { _IDMoneda = value; }
		}

		private String _Operacion;
		public String Operacion
		{
			get { return _Operacion; }
			set { _Operacion = value; }
		}

		private String _MedioPago;
		public String MedioPago
		{
			get { return _MedioPago; }
			set { _MedioPago = value; }
		}

		private String _Moneda;
		public String Moneda
		{
			get { return _Moneda; }
			set { _Moneda = value; }
		}

		private Decimal _TotalIngreso;
		public Decimal TotalIngreso
		{
			get { return _TotalIngreso; }
			set { _TotalIngreso = value; }
		}

		private Decimal _TotalSalida;
		public Decimal TotalSalida
		{
			get { return _TotalSalida; }
			set { _TotalSalida = value; }
		}

		private Decimal _TotalSaldo;
		public Decimal TotalSaldo
		{
			get { return _TotalSaldo; }
			set { _TotalSaldo = value; }
		}
		 

	}
}