using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
	public class BEPedidoDetalleLote : BEBase
	{

		private Int32 _IDPedidoDetalleLote;
		public Int32 IDPedidoDetalleLote
		{
			get { return _IDPedidoDetalleLote; }
			set { _IDPedidoDetalleLote = value; }
		}

		private Int32 _IDPedidoDetalle;
		public Int32 IDPedidoDetalle
		{
			get { return _IDPedidoDetalle; }
			set { _IDPedidoDetalle = value; }
		}

		private Int32 _IDPedido;
		public Int32 IDPedido
		{
			get { return _IDPedido; }
			set { _IDPedido = value; }
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


		private Int32 _IDPedidoDetalleLoteTemp;
		public Int32 IDPedidoDetalleLoteTemp
		{
			get { return _IDPedidoDetalleLoteTemp; }
			set { _IDPedidoDetalleLoteTemp = value; }
		}
		private Int32 _IDPedidoDetalleTemp;
		public Int32 IDPedidoDetalleTemp
		{
			get { return _IDPedidoDetalleTemp; }
			set { _IDPedidoDetalleTemp = value; }
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