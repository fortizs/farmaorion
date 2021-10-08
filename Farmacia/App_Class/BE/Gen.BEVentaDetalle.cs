﻿using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEVentaDetalle : BEBase
    {

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

        
        private String _Codigo;
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }
         
        private String _CodigoProducto;
        public String CodigoProducto
        {
            get { return _CodigoProducto; }
            set { _CodigoProducto = value; }
        }
        private String _ProductoDetalle;
        public String ProductoDetalle
        {
            get { return _ProductoDetalle; }
            set { _ProductoDetalle = value; }
        }

        private String _DescripcionProducto;
        public String DescripcionProducto
        {
            get { return _DescripcionProducto; }
            set { _DescripcionProducto = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private Decimal _Stock;
        public Decimal Stock
        {
            get { return _Stock; }
            set { _Stock = value; }
        }

        private Int32 _Item;
        public Int32 Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private Int32 _IDUnidadMedida;
        public Int32 IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
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

        private Decimal _PrecioUnitarioConIgv;
        public Decimal PrecioUnitarioConIgv
        {
            get { return _PrecioUnitarioConIgv; }
            set { _PrecioUnitarioConIgv = value; }
        }

        private Decimal _SubTotal;
        public Decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

		private Decimal _ImporteTotal;
		public Decimal ImporteTotal
		{
			get { return _ImporteTotal; }
			set { _ImporteTotal = value; }
		}
		

		private Decimal _Total;
        public Decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }


        private String _Producto;
        public String Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
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
		


		private Decimal _Descuento;
        public Decimal Descuento
        {
            get { return _Descuento; }
            set { _Descuento = value; }
        }

        private String _IDTipoPrecio;
        public String IDTipoPrecio
        {
            get { return _IDTipoPrecio; }
            set { _IDTipoPrecio = value; }
        }


        private String _IDTipoImpuesto;
        public String IDTipoImpuesto
        {
            get { return _IDTipoImpuesto; }
            set { _IDTipoImpuesto = value; }
        }

		private String _Proceso;
		public String Proceso
		{
			get { return _Proceso; }
			set { _Proceso = value; }
		}
		 

		private String _TipoImpuesto;
        public String TipoImpuesto
        {
            get { return _TipoImpuesto; }
            set { _TipoImpuesto = value; }
        }
        

        private Decimal _Igv;
        public Decimal Igv
        {
            get { return _Igv; }
            set { _Igv = value; }
        }

        private Decimal _ImporteVenta;
        public Decimal ImporteVenta
        {
            get { return _ImporteVenta; }
            set { _ImporteVenta = value; }
        }

		private Boolean _ControlaLote;
		public Boolean ControlaLote
		{
			get { return _ControlaLote; }
			set { _ControlaLote = value; }
		}

		private Int32 _IDLote;
		public Int32 IDLote
		{
			get { return _IDLote; }
			set { _IDLote = value; }
		}

		private Int32 _CantidadLote;
		public Int32 CantidadLote
		{
			get { return _CantidadLote; }
			set { _CantidadLote = value; }
		}

		private String _NombreProducto;
		public String NombreProducto
		{
			get { return _NombreProducto; }
			set { _NombreProducto = value; }
		}

		private String _NombreCategoria;
		public String NombreCategoria
		{
			get { return _NombreCategoria; }
			set { _NombreCategoria = value; }
		}

		private String _NombreUnidadMedida;
		public String NombreUnidadMedida
		{
			get { return _NombreUnidadMedida; }
			set { _NombreUnidadMedida = value; }
		}


		private Int32 _IDVentaDetalleTemp;
		public Int32 IDVentaDetalleTemp
		{
			get { return _IDVentaDetalleTemp; }
			set { _IDVentaDetalleTemp = value; }
		}
		 
		private String _Token;
		public String Token
		{
			get { return _Token; }
			set { _Token = value; }
		}
		 
		private Int32 _IDUnidadMedidaVenta;
		public Int32 IDUnidadMedidaVenta
		{
			get { return _IDUnidadMedidaVenta; }
			set { _IDUnidadMedidaVenta = value; }
		}
	 
		private Decimal _ValorUnitario;
		public Decimal ValorUnitario
		{
			get { return _ValorUnitario; }
			set { _ValorUnitario = value; }
		}
		 
		private Decimal _PrecioVenta;
		public Decimal PrecioVenta
		{
			get { return _PrecioVenta; }
			set { _PrecioVenta = value; }
		}
		 
		private Boolean _EsVentaEspera;
		public Boolean EsVentaEspera
		{
			get { return _EsVentaEspera; }
			set { _EsVentaEspera = value; }
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
		 
		private String _UnidadMedidaVenta;
		public String UnidadMedidaVenta
		{
			get { return _UnidadMedidaVenta; }
			set { _UnidadMedidaVenta = value; }
		}


		private Decimal _PorcentajeDescuento;
		public Decimal PorcentajeDescuento
		{
			get { return _PorcentajeDescuento; }
			set { _PorcentajeDescuento = value; }
		}

		private Decimal _PrecioVentaSinDescuento;
		public Decimal PrecioVentaSinDescuento
		{
			get { return _PrecioVentaSinDescuento; }
			set { _PrecioVentaSinDescuento = value; }
		}



		
		private Decimal _PrecioConDescuento;
		public Decimal PrecioConDescuento
		{
			get { return _PrecioConDescuento; }
			set { _PrecioConDescuento = value; }
		}

		private Boolean _VentaConReceta;
		public Boolean VentaConReceta
		{
			get { return _VentaConReceta; }
			set { _VentaConReceta = value; }
		}


		/*****************/

		private String _Carroceria;
		public String Carroceria
		{
			get { return _Carroceria; }
			set { _Carroceria = value; }
		}
		private String _Chasis;
		public String Chasis
		{
			get { return _Chasis; }
			set { _Chasis = value; }
		}
		private String _NumeroLote;
		public String NumeroLote
		{
			get { return _NumeroLote; }
			set { _NumeroLote = value; }
		}
		private String _Motor;
		public String Motor
		{
			get { return _Motor; }
			set { _Motor = value; }
		}
		private String _NumeroDUA;
		public String NumeroDUA
		{
			get { return _NumeroDUA; }
			set { _NumeroDUA = value; }
		}
		private DateTime _FechaDUA;
		public DateTime FechaDUA
		{
			get { return _FechaDUA; }
			set { _FechaDUA = value; }
		}
		private Int32 _AnioModelo;
		public Int32 AnioModelo
		{
			get { return _AnioModelo; }
			set { _AnioModelo = value; }
		}
		private String _TipoCombustible;
		public String TipoCombustible
		{
			get { return _TipoCombustible; }
			set { _TipoCombustible = value; }
		}
		private String _Color;
		public String Color
		{
			get { return _Color; }
			set { _Color = value; }
		}
		private String _Modelo;
		public String Modelo
		{
			get { return _Modelo; }
			set { _Modelo = value; }
		}
		private String _MarcaVehiculo;
		public String MarcaVehiculo
		{
			get { return _MarcaVehiculo; }
			set { _MarcaVehiculo = value; }
		}
		private String _ModeloVersion;
		public String ModeloVersion
		{
			get { return _ModeloVersion; }
			set { _ModeloVersion = value; }
		}



	}
}
