using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEKardexDetalle : BEBase
    {

        private String _EntradaSalida;
        public String EntradaSalida
        {
            get { return _EntradaSalida; }
            set { _EntradaSalida = value; }
        }

        private String _CodigoProducto;
        public String CodigoProducto
        {
            get { return _CodigoProducto; }
            set { _CodigoProducto = value; }
        }

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private String _NombreProducto;
        public String NombreProducto
        {
            get { return _NombreProducto; }
            set { _NombreProducto = value; }
        }

        private String _Fecha;
        public String Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private String _SerieNumeroDocumento;
        public String SerieNumeroDocumento
        {
            get { return _SerieNumeroDocumento; }
            set { _SerieNumeroDocumento = value; }
        }

        private String _TipoMovimiento;
        public String TipoMovimiento
        {
            get { return _TipoMovimiento; }
            set { _TipoMovimiento = value; }
        }


        private String _DocumentoReferencia;
        public String DocumentoReferencia
        {
            get { return _DocumentoReferencia; }
            set { _DocumentoReferencia = value; }
        }


        private Decimal _Cantidad; 
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private Decimal _StockActual;
        public Decimal StockActual
        {
            get { return _StockActual; }
            set { _StockActual = value; }
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

        //private Int32 _IDSucursal;
        //public Int32 IDSucursal
        //{
        //    get { return _IDSucursal; }
        //    set { _IDSucursal = value; }
        //}


        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _SucursalOrigen;
        public String SucursalOrigen
        {
            get { return _SucursalOrigen; }
            set { _SucursalOrigen = value; }
        }

        private String _SucursalDestino;
        public String SucursalDestino
        {
            get { return _SucursalDestino; }
            set { _SucursalDestino = value; }
        }

        private String _Usuario;
        public String Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }


        private Int32 _IDKardexAlmacenDetalle;
        public Int32 IDKardexAlmacenDetalle
        {
            get { return _IDKardexAlmacenDetalle; }
            set { _IDKardexAlmacenDetalle = value; }
        }

        private DateTime _FechaMovimiento;
        public DateTime FechaMovimiento
        {
            get { return _FechaMovimiento; }
            set { _FechaMovimiento = value; }
        }
  
        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        private String _ClienteProveedor;
        public String ClienteProveedor
        {
            get { return _ClienteProveedor; }
            set { _ClienteProveedor = value; }
        }

        private Decimal _Entrada;
        public Decimal Entrada
        {
            get { return _Entrada; }
            set { _Entrada = value; }
        }

        private Decimal _Salida;
        public Decimal Salida
        {
            get { return _Salida; }
            set { _Salida = value; }
        }

        private Decimal _Saldo;
        public Decimal Saldo
        {
            get { return _Saldo; }
            set { _Saldo = value; }
        }
         

        private Int32 _IDSucursalDestino;
        public Int32 IDSucursalDestino
        {
            get { return _IDSucursalDestino; }
            set { _IDSucursalDestino = value; }
        }
         
        private Decimal _EntradaCantidad;
        public Decimal EntradaCantidad
        {
            get { return _EntradaCantidad; }
            set { _EntradaCantidad = value; }
        }

        private Decimal _EntradaPrecioCosto;
        public Decimal EntradaPrecioCosto
        {
            get { return _EntradaPrecioCosto; }
            set { _EntradaPrecioCosto = value; }
        }

        private Decimal _EntradaPrecioCostoTotal;
        public Decimal EntradaPrecioCostoTotal
        {
            get { return _EntradaPrecioCostoTotal; }
            set { _EntradaPrecioCostoTotal = value; }
        }

        private Decimal _SalidaCantidad;
        public Decimal SalidaCantidad
        {
            get { return _SalidaCantidad; }
            set { _SalidaCantidad = value; }
        }

        private Decimal _SalidaPrecioUnitario;
        public Decimal SalidaPrecioUnitario
        {
            get { return _SalidaPrecioUnitario; }
            set { _SalidaPrecioUnitario = value; }
        }

        private Decimal _SalidaPrecioUnitarioTotal;
        public Decimal SalidaPrecioUnitarioTotal
        {
            get { return _SalidaPrecioUnitarioTotal; }
            set { _SalidaPrecioUnitarioTotal = value; }
        }
         

        private Decimal _SaldoPrecioUnitario;
        public Decimal SaldoPrecioUnitario
        {
            get { return _SaldoPrecioUnitario; }
            set { _SaldoPrecioUnitario = value; }
        }

        private Decimal _SaldoPrecioUnitarioTotal;
        public Decimal SaldoPrecioUnitarioTotal
        {
            get { return _SaldoPrecioUnitarioTotal; }
            set { _SaldoPrecioUnitarioTotal = value; }
        }

        



    }
}
