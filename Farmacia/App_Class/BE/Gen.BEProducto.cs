using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.General
{
    public class BEProducto : BEBase
    {
		private Int32 _IdProductoPrecio;
		public Int32 IdProductoPrecio
		{
			get { return _IdProductoPrecio; }
			set { _IdProductoPrecio = value; }
		}

		private Int32 _IDProducto; 
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }
		 
		private Int32 _IDLinea;
        public Int32 IDLinea
        {
            get { return _IDLinea; }
            set { _IDLinea = value; }
        }

        private Int32 _IDColor;
        public Int32 IDColor
        {
            get { return _IDColor; }
            set { _IDColor = value; }
        }

        private String _Color;
        public String Color
        {
            get { return _Color; }
            set { _Color = value; }
        }


        private Int32 _IDCategoria;
        public Int32 IDCategoria
        {
            get { return _IDCategoria; }
            set { _IDCategoria = value; }
        }

        private String _Categoria;
        public String Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; }
        }

        private String _Linea;
        public String Linea
        {
            get { return _Linea; }
            set { _Linea = value; }
        }


        private Int32 _IDMarca;
        public Int32 IDMarca
        {
            get { return _IDMarca; }
            set { _IDMarca = value; }
        }

        private String _Marca;
        public String Marca
        {
            get { return _Marca; }
            set { _Marca = value; }
        }

        private Int32 _IDProcedencia;
        public Int32 IDProcedencia
        {
            get { return _IDProcedencia; }
            set { _IDProcedencia = value; }
        }

        private String _Procedencia;
        public String Procedencia
        {
            get { return _Procedencia; }
            set { _Procedencia = value; }
        }

        private String _ControlStock;
        public String ControlStock
        {
            get { return _ControlStock; }
            set { _ControlStock = value; }
        }

        

        private Int32 _IDUnidadMedida;
        public Int32 IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
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

        private Decimal _PrecioVentaSinIgv;
        public Decimal PrecioVentaSinIgv
        {
            get { return _PrecioVentaSinIgv; }
            set { _PrecioVentaSinIgv = value; }
        }

        private Decimal _PrecioVenta;
        public Decimal PrecioVenta
        {
            get { return _PrecioVenta; }
            set { _PrecioVenta = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private Int32 _IdStock;
        public Int32 IdStock
        {
            get { return _IdStock; }
            set { _IdStock = value; }
        }
		 
        private Decimal _StockMinimo;
        public Decimal StockMinimo
        {
            get { return _StockMinimo; }
            set { _StockMinimo = value; }
        }

		private Decimal _StockInicial;
		public Decimal StockInicial
		{
			get { return _StockInicial; }
			set { _StockInicial = value; }
		}
		 
		private Decimal _StockActual;
        public Decimal StockActual
        {
            get { return _StockActual; }
            set { _StockActual = value; }
        }
          
        private bool  _ControlaStock;
        public bool  ControlaStock
        {
            get { return _ControlaStock; }
            set { _ControlaStock = value; }
        }

        private Int32 _IDTipoAfectacionIgv;
        public Int32 IDTipoAfectacionIgv
		{
            get { return _IDTipoAfectacionIgv; }
            set { _IDTipoAfectacionIgv = value; }
        }

        private Int32 _IDTipoPrecio;
        public Int32 IDTipoPrecio
        {
            get { return _IDTipoPrecio; }
            set { _IDTipoPrecio = value; }
        }

        /*SE AGREGO 26022018*/
        private String _Codigo;
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        private String _CodigoUnidadMedida;
        public String CodigoUnidadMedida
        {
            get { return _CodigoUnidadMedida; }
            set { _CodigoUnidadMedida = value; }
        }
         
		private Boolean _AlertaStock;
		public Boolean AlertaStock
        {
			get { return _AlertaStock; }
			set { _AlertaStock = value; }
		}

        
        private String _CodigoProducto;
        public String CodigoProducto
        {
            get { return _CodigoProducto; }
            set { _CodigoProducto = value; }
        }

        private String _Producto;
        public String Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private String _UnidadMedidaCorto;
        public String UnidadMedidaCorto
        {
            get { return _UnidadMedidaCorto; }
            set { _UnidadMedidaCorto = value; }
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
	 
		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}

		private Boolean _EsServicio;
		public Boolean EsServicio
		{
			get { return _EsServicio; }
			set { _EsServicio = value; }
		}
		 
		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
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
		 
		private String _TipoProducto;
		public String TipoProducto
		{
			get { return _TipoProducto; }
			set { _TipoProducto = value; }
		}
		 
		private Int32 _IDTipoProducto;
		public Int32 IDTipoProducto
		{
			get { return _IDTipoProducto; }
			set { _IDTipoProducto = value; }
		}
		private Int32 _IDUnidadMedidaCompra;
		public Int32 IDUnidadMedidaCompra
		{
			get { return _IDUnidadMedidaCompra; }
			set { _IDUnidadMedidaCompra = value; }
		}
		private Int32 _IDUnidadMedidaVenta;
		public Int32 IDUnidadMedidaVenta
		{
			get { return _IDUnidadMedidaVenta; }
			set { _IDUnidadMedidaVenta = value; }
		}
		 
		private String _NombreCorto;
		public String NombreCorto
		{
			get { return _NombreCorto; }
			set { _NombreCorto = value; }
		}
		 
		private Int32 _Factor;
		public Int32 Factor
		{
			get { return _Factor; }
			set { _Factor = value; }
		}
		 
		private Boolean _ControlaLote;
		public Boolean ControlaLote
		{
			get { return _ControlaLote; }
			set { _ControlaLote = value; }
		}
		private Boolean _VentaConReceta;
		public Boolean VentaConReceta
		{
			get { return _VentaConReceta; }
			set { _VentaConReceta = value; }
		}
		private Decimal _Peso;
		public Decimal Peso
		{
			get { return _Peso; }
			set { _Peso = value; }
		}
		 
		private Decimal _PrecioCostoTotalSinIgv;
		public Decimal PrecioCostoTotalSinIgv
		{
			get { return _PrecioCostoTotalSinIgv; }
			set { _PrecioCostoTotalSinIgv = value; }
		}
		private Decimal _PrecioCostoUnidadSinIgv;
		public Decimal PrecioCostoUnidadSinIgv
		{
			get { return _PrecioCostoUnidadSinIgv; }
			set { _PrecioCostoUnidadSinIgv = value; }
		}

		private Decimal _PrecioCostoUnidadConIgv;
		public Decimal PrecioCostoUnidadConIgv
		{
			get { return _PrecioCostoUnidadConIgv; }
			set { _PrecioCostoUnidadConIgv = value; }
		}
		 
		private Decimal _MargenUtilidad;
		public Decimal MargenUtilidad
		{
			get { return _MargenUtilidad; }
			set { _MargenUtilidad = value; }
		}

		private Decimal _Valorizado;
		public Decimal Valorizado
		{
			get { return _Valorizado; }
			set { _Valorizado = value; }
		}
		 
		private Int32 _MayoreoUnidad;
		public Int32 MayoreoUnidad
		{
			get { return _MayoreoUnidad; }
			set { _MayoreoUnidad = value; }
		}

		private Decimal _StockCompra;
		public Decimal StockCompra
		{
			get { return _StockCompra; }
			set { _StockCompra = value; }
		}

		private BELote _Lote;
		public BELote Lote
		{
			get { return _Lote; }
			set { _Lote = value; }
		}

		private String _Cod_Prod;
		public String Cod_Prod
		{
			get { return _Cod_Prod; }
			set { _Cod_Prod = value; }
		}

		private String _NombreCompleto;
		public String NombreCompleto
		{
			get { return _NombreCompleto; }
			set { _NombreCompleto = value; }
		}

		private String _Laboratorio;
		public String Laboratorio
		{
			get { return _Laboratorio; }
			set { _Laboratorio = value; }
		}

		

		private String _Nom_Titular;
		public String Nom_Titular
		{
			get { return _Nom_Titular; }
			set { _Nom_Titular = value; }
		}

		private String _Presentac;
		public String Presentac
		{
			get { return _Presentac; }
			set { _Presentac = value; }
		}

		private Int32 _IDProductoTemp;
		public Int32 IDProductoTemp
		{
			get { return _IDProductoTemp; }
			set { _IDProductoTemp = value; }
		}

		private Int32 _Reservado;
		public Int32 Reservado
		{
			get { return _Reservado; }
			set { _Reservado = value; }
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

		private Int32 _IDTipoCombustible;
		public Int32 IDTipoCombustible
		{
			get { return _IDTipoCombustible; }
			set { _IDTipoCombustible = value; }
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

		private Int32 _IDModeloVersion;
		public Int32 IDModeloVersion
		{
			get { return _IDModeloVersion; }
			set { _IDModeloVersion = value; }
		}

		private Int32 _IDCarroceria;
		public Int32 IDCarroceria
		{
			get { return _IDCarroceria; }
			set { _IDCarroceria = value; }
		}


		

		private String _TipoCombustible;
		public String TipoCombustible
		{
			get { return _TipoCombustible; }
			set { _TipoCombustible = value; }
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

		private Int32 _IDMarcaVehiculo;
		public Int32 IDMarcaVehiculo
		{
			get { return _IDMarcaVehiculo; }
			set { _IDMarcaVehiculo = value; }
		}

		private Int32 _IDModelo;
		public Int32 IDModelo
		{
			get { return _IDModelo; }
			set { _IDModelo = value; }
		}
	 
		 
	}
}