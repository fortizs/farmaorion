using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Inventario
{
	public class BEMovimientoDetalle : BEBase
	{
		private Int32 _IDMovimientoDetalle;
		public Int32 IDMovimientoDetalle
		{
			get { return _IDMovimientoDetalle; }
			set { _IDMovimientoDetalle = value; }
		}

		private Int32 _IDMovimiento;
		public Int32 IDMovimiento
		{
			get { return _IDMovimiento; }
			set { _IDMovimiento = value; }
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

		private String _NombreProducto;
		public String NombreProducto
		{
			get { return _NombreProducto; }
			set { _NombreProducto = value; }
		}

		private Int32 _IDUnidadMedida;
		public Int32 IDUnidadMedida
		{
			get { return _IDUnidadMedida; }
			set { _IDUnidadMedida = value; }
		}

		private String _UnidadMedida;
		public String UnidadMedida
		{
			get { return _UnidadMedida; }
			set { _UnidadMedida = value; }
		}

		private Decimal _Cantidad;
		public Decimal Cantidad
		{
			get { return _Cantidad; }
			set { _Cantidad = value; }
		}

		private Decimal _PrecioUnitario;
		public Decimal PrecioUnitario
		{
			get { return _PrecioUnitario; }
			set { _PrecioUnitario = value; }
		}

		private String _Token;
		public String Token
		{
			get { return _Token; }
			set { _Token = value; }
		}

		private Decimal _PrecioCosto;
		public Decimal PrecioCosto
		{
			get { return _PrecioCosto; }
			set { _PrecioCosto = value; }
		}

		private Decimal _StockActual;
		public Decimal StockActual
		{
			get { return _StockActual; }
			set { _StockActual = value; }
		}

		private Decimal _Saldo;
		public Decimal Saldo
		{
			get { return _Saldo; }
			set { _Saldo = value; }
		}

		private Decimal _SubTotal;
		public Decimal SubTotal
		{
			get { return _SubTotal; }
			set { _SubTotal = value; }
		}

		private Decimal _PrecioPromedio;
		public Decimal PrecioPromedio
		{
			get { return _PrecioPromedio; }
			set { _PrecioPromedio = value; }
		}

		private DateTime _FechaMovimiento;
		public DateTime FechaMovimiento
		{
			get { return _FechaMovimiento; }
			set { _FechaMovimiento = value; }
		}

		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}

		private String _TipoMovimiento;
		public String TipoMovimiento
		{
			get { return _TipoMovimiento; }
			set { _TipoMovimiento = value; }
		}

		private Decimal _ValorUnidad;
		public Decimal ValorUnidad
		{
			get { return _ValorUnidad; }
			set { _ValorUnidad = value; }
		}

		private Decimal _ValorTotal;
		public Decimal ValorTotal
		{
			get { return _ValorTotal; }
			set { _ValorTotal = value; }
		}

		private Decimal _SaldoCantidad;
		public Decimal SaldoCantidad
		{
			get { return _SaldoCantidad; }
			set { _SaldoCantidad = value; }
		}

		private Decimal _SaldoValorUnidad;
		public Decimal SaldoValorUnidad
		{
			get { return _SaldoValorUnidad; }
			set { _SaldoValorUnidad = value; }
		}

		private Decimal _SaldoValorTotal;
		public Decimal SaldoValorTotal
		{
			get { return _SaldoValorTotal; }
			set { _SaldoValorTotal = value; }
		}

		private String _ProductoCodigo;
		public String ProductoCodigo
		{
			get { return _ProductoCodigo; }
			set { _ProductoCodigo = value; }
		}

		private String _Producto;
		public String Producto
		{
			get { return _Producto; }
			set { _Producto = value; }
		}


		private String _DocumentoReferencia;
		public String DocumentoReferencia
		{
			get { return _DocumentoReferencia; }
			set { _DocumentoReferencia = value; }
		}
		private String _Transaccion;
		public String Transaccion
		{
			get { return _Transaccion; }
			set { _Transaccion = value; }
		}
		private Decimal _EntradaCantidad;
		public Decimal EntradaCantidad
		{
			get { return _EntradaCantidad; }
			set { _EntradaCantidad = value; }
		}
		private Decimal _EntradaValorUnidad;
		public Decimal EntradaValorUnidad
		{
			get { return _EntradaValorUnidad; }
			set { _EntradaValorUnidad = value; }
		}
		private Decimal _EntradaValorTotal;
		public Decimal EntradaValorTotal
		{
			get { return _EntradaValorTotal; }
			set { _EntradaValorTotal = value; }
		}
		private Decimal _SalidaCantidad;
		public Decimal SalidaCantidad
		{
			get { return _SalidaCantidad; }
			set { _SalidaCantidad = value; }
		}
		private Decimal _SalidaValorUnidad;
		public Decimal SalidaValorUnidad
		{
			get { return _SalidaValorUnidad; }
			set { _SalidaValorUnidad = value; }
		}
		private Decimal _SalidaValorTotal;
		public Decimal SalidaValorTotal
		{
			get { return _SalidaValorTotal; }
			set { _SalidaValorTotal = value; }
		}

		

	}
}
