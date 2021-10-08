using System;

namespace Farmacia.App_Class.BE.General
{
	public class BEPrecioProveedor : BEBase
	{ 
		private Int32 _IDPrecioProveedor;
		public Int32 IDPrecioProveedor
		{
			get { return _IDPrecioProveedor; }
			set { _IDPrecioProveedor = value; }
		}
		private Int32 _IDProducto;
		public Int32 IDProducto
		{
			get { return _IDProducto; }
			set { _IDProducto = value; }
		}
		private Int32 _IDProveedor;
		public Int32 IDProveedor
		{
			get { return _IDProveedor; }
			set { _IDProveedor = value; }
		}
		private DateTime _FechaUltimoPrecio;
		public DateTime FechaUltimoPrecio
		{
			get { return _FechaUltimoPrecio; }
			set { _FechaUltimoPrecio = value; }
		}
		private Decimal _UltimoPrecioCompra;
		public Decimal UltimoPrecioCompra
		{
			get { return _UltimoPrecioCompra; }
			set { _UltimoPrecioCompra = value; }
		}

		private String _Proveedor;
		public String Proveedor
		{
			get { return _Proveedor; }
			set { _Proveedor = value; }
		}
		 
	}
}