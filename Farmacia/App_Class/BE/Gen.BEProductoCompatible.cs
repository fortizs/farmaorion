using System;

namespace Farmacia.App_Class.BE.General
{
	public class BEProductoCompatible : BEBase
	{

		private Int32 _IDProductoCompatible;
		public Int32 IDProductoCompatible
		{
			get { return _IDProductoCompatible; }
			set { _IDProductoCompatible = value; }
		}
		private Int32 _IDProducto;
		public Int32 IDProducto
		{
			get { return _IDProducto; }
			set { _IDProducto = value; }
		}
		private Int32 _IDProductoComp;
		public Int32 IDProductoComp
		{
			get { return _IDProductoComp; }
			set { _IDProductoComp = value; }
		}
		private DateTime _FechaCreacion;
		public DateTime FechaCreacion
		{
			get { return _FechaCreacion; }
			set { _FechaCreacion = value; }
		}
		private String _Producto;
		public String Producto
		{
			get { return _Producto; }
			set { _Producto = value; }
		}
		 
		private String _CodigoBarra;
		public String CodigoBarra
		{
			get { return _CodigoBarra; }
			set { _CodigoBarra = value; }
		}
		private String _CodigoAlterna;
		public String CodigoAlterna
		{
			get { return _CodigoAlterna; }
			set { _CodigoAlterna = value; }
		}
		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		private String _Descripcion;
		public String Descripcion
		{
			get { return _Descripcion; }
			set { _Descripcion = value; }
		}
		private String _PrincipioActivo;
		public String PrincipioActivo
		{
			get { return _PrincipioActivo; }
			set { _PrincipioActivo = value; }
		}
		private String _Localizacion;
		public String Localizacion
		{
			get { return _Localizacion; }
			set { _Localizacion = value; }
		}
		private Decimal _StockMinimo;
		public Decimal StockMinimo
		{
			get { return _StockMinimo; }
			set { _StockMinimo = value; }
		}
		private Decimal _Stock;
		public Decimal Stock
		{
			get { return _Stock; }
			set { _Stock = value; }
		}
		private Decimal _PrecioCosto;
		public Decimal PrecioCosto
		{
			get { return _PrecioCosto; }
			set { _PrecioCosto = value; }
		}
		private Decimal _PrecioVenta;
		public Decimal PrecioVenta
		{
			get { return _PrecioVenta; }
			set { _PrecioVenta = value; }
		}
		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}
		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}
		private Int32 _IDLinea;
		public Int32 IDLinea
		{
			get { return _IDLinea; }
			set { _IDLinea = value; }
		}
		private Int32 _IDMarca;
		public Int32 IDMarca
		{
			get { return _IDMarca; }
			set { _IDMarca = value; }
		}
		private Int32 _IDCategoria;
		public Int32 IDCategoria
		{
			get { return _IDCategoria; }
			set { _IDCategoria = value; }
		}
		private String _UnidadMedidaCompra;
		public String UnidadMedidaCompra
		{
			get { return _UnidadMedidaCompra; }
			set { _UnidadMedidaCompra = value; }
		}
		private String _UnidadMedidaVenta;
		public String UnidadMedidaVenta
		{
			get { return _UnidadMedidaVenta; }
			set { _UnidadMedidaVenta = value; }
		}
		private String _Linea;
		public String Linea
		{
			get { return _Linea; }
			set { _Linea = value; }
		}
		private String _Marca;
		public String Marca
		{
			get { return _Marca; }
			set { _Marca = value; }
		}
		private String _Categoria;
		public String Categoria
		{
			get { return _Categoria; }
			set { _Categoria = value; }
		}
		private String _TipoProducto;
		public String TipoProducto
		{
			get { return _TipoProducto; }
			set { _TipoProducto = value; }
		}
		private Boolean _AlertaStock;
		public Boolean AlertaStock
		{
			get { return _AlertaStock; }
			set { _AlertaStock = value; }
		}

	}
}