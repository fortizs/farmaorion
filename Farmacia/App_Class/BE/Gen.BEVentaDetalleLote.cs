using System;

namespace Farmacia.App_Class.BE.General
{
	public class BEVentaDetalleLote : BEBase
	{
		
			private Int32 _IDVentaDetalleLote;
		public Int32 IDVentaDetalleLote
		{
			get { return _IDVentaDetalleLote; }
			set { _IDVentaDetalleLote = value; }
		}

		private Int32 _IDVentaDetalle;
		public Int32 IDVentaDetalle
		{
			get { return _IDVentaDetalle; }
			set { _IDVentaDetalle = value; }
		}

		private Int32 _IDVenta;
		public Int32 IDVenta
		{
			get { return _IDVenta; }
			set { _IDVenta = value; }
		}

		private Int32 _IDProducto;
		public Int32 IDProducto
		{
			get { return _IDProducto; }
			set { _IDProducto = value; }
		}

		private Int32 _IDLote;
		public Int32 IDLote
		{
			get { return _IDLote; }
			set { _IDLote = value; }
		}

		private Decimal _CantidadLote;
		public Decimal CantidadLote
		{
			get { return _CantidadLote; }
			set { _CantidadLote = value; }
		}


		private Int32 _IDVentaDetalleLoteTemp;
		public Int32 IDVentaDetalleLoteTemp
		{
			get { return _IDVentaDetalleLoteTemp; }
			set { _IDVentaDetalleLoteTemp = value; }
		}
		private Int32 _IDVentaDetalleTemp;
		public Int32 IDVentaDetalleTemp
		{
			get { return _IDVentaDetalleTemp; }
			set { _IDVentaDetalleTemp = value; }
		}
		 
		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}
		private Decimal _Cantidad;
		public Decimal Cantidad
		{
			get { return _Cantidad; }
			set { _Cantidad = value; }
		}
		private Int32 _IDUsuarioCreacion;
		public Int32 IDUsuarioCreacion
		{
			get { return _IDUsuarioCreacion; }
			set { _IDUsuarioCreacion = value; }
		}
		private DateTime _FechaCreacion;
		public DateTime FechaCreacion
		{
			get { return _FechaCreacion; }
			set { _FechaCreacion = value; }
		}
		private String _Lote;
		public String Lote
		{
			get { return _Lote; }
			set { _Lote = value; }
		}
		private DateTime _FechaVencimiento;
		public DateTime FechaVencimiento
		{
			get { return _FechaVencimiento; }
			set { _FechaVencimiento = value; }
		}
		private DateTime _FechaFabricacion;
		public DateTime FechaFabricacion
		{
			get { return _FechaFabricacion; }
			set { _FechaFabricacion = value; }
		}
		private Decimal _StockActualLote;
		public Decimal StockActualLote
		{
			get { return _StockActualLote; }
			set { _StockActualLote = value; }
		}

		
	}
}