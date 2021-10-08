using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEContrato : BEBase
    {

        private Int32 _IDContrato;
        public Int32 IDContrato
        {
            get { return _IDContrato; }
            set { _IDContrato = value; }
        }

        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private Int32 _IDTipoDocumento;
        public Int32 IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }

        private Int32 _IDCliente;
        public Int32 IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private Int32 _NumeroPedido;
        public Int32 NumeroPedido
        {
            get { return _NumeroPedido; }
            set { _NumeroPedido = value; }
        }

        private Int32 _IDTipoComprobante;
        public Int32 IDTipoComprobante
        {
            get { return _IDTipoComprobante; }
            set { _IDTipoComprobante = value; }
        }
         
        private String _TipoPedido;
        public String TipoPedido
        {
            get { return _TipoPedido; }
            set { _TipoPedido = value; }
        }

        private DateTime _FechaVenta;
        public DateTime FechaVenta
        {
            get { return _FechaVenta; }
            set { _FechaVenta = value; }
        }

        private Decimal _TotalPagado;
        public Decimal TotalPagado
        {
            get { return _TotalPagado; }
            set { _TotalPagado = value; }
        }
        

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private Decimal _MontoPedido;
        public Decimal MontoPedido
        {
            get { return _MontoPedido; }
            set { _MontoPedido = value; }
        }

        private DateTime _FechaEntrega;
        public DateTime FechaEntrega
        {
            get { return _FechaEntrega; }
            set { _FechaEntrega = value; }
        }

        private DateTime _FechaPedido;
        public DateTime FechaPedido
        {
            get { return _FechaPedido; }
            set { _FechaPedido = value; }
        }

        private String _CaracteristicaProducto;
        public String CaracteristicaProducto
        {
            get { return _CaracteristicaProducto; }
            set { _CaracteristicaProducto = value; }
        }

        private String _Observacion;
        public String Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }

        private String _NumeroPedidoFormateado;
        public String NumeroPedidoFormateado
        {
            get { return _NumeroPedidoFormateado; }
            set { _NumeroPedidoFormateado = value; }
        }

        private String _ClienteDni;
        public String ClienteDni
        {
            get { return _ClienteDni; }
            set { _ClienteDni = value; }
        }

        private String _Cliente;
        public String Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private String _ClienteDireccion;
        public String ClienteDireccion
        {
            get { return _ClienteDireccion; }
            set { _ClienteDireccion = value; }
        }

        private String _IDUbigeo;
        public String IDUbigeo
        {
            get { return _IDUbigeo; }
            set { _IDUbigeo = value; }
        }

        private String _TipoOperacion;
        public String TipoOperacion
        {
            get { return _TipoOperacion; }
            set { _TipoOperacion = value; }
        }

        

        /**/

        private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private String _ClienteTipoDocumento;
        public String ClienteTipoDocumento
        {
            get { return _ClienteTipoDocumento; }
            set { _ClienteTipoDocumento = value; }
        }
         
        private String _TipoPedidoNombre;
        public String TipoPedidoNombre
        {
            get { return _TipoPedidoNombre; }
            set { _TipoPedidoNombre = value; }
        }

        private String _EstadoNombre;
        public String EstadoNombre
        {
            get { return _EstadoNombre; }
            set { _EstadoNombre = value; }
        }

        private String _Simbolo;
        public String Simbolo
        {
            get { return _Simbolo; }
            set { _Simbolo = value; }
        }
         
        private Decimal _MontoAmortizado;
        public Decimal MontoAmortizado
        {
            get { return _MontoAmortizado; }
            set { _MontoAmortizado = value; }
        }

        private Decimal _MontoDeuda;
        public Decimal MontoDeuda
        {
            get { return _MontoDeuda; }
            set { _MontoDeuda = value; }
        }

        private String _Accion;
        public String Accion
        {
            get { return _Accion; }
            set { _Accion = value; }
        }

        private Int32 _IDEstadoFase;
        public Int32 IDEstadoFase
        {
            get { return _IDEstadoFase; }
            set { _IDEstadoFase = value; }
        }

        private Int32 _Codigo;
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        private String _NumeroComprobante;
        public String NumeroComprobante
        {
            get { return _NumeroComprobante; }
            set { _NumeroComprobante = value; }
        }

        private DateTime _FechaRegistro;
        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
 
        private String _Celular;
        public String Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }

        private Boolean _GeneroComprobante;
        public Boolean GeneroComprobante
        {
            get { return _GeneroComprobante; }
            set { _GeneroComprobante = value; }
        }

        

    }
}
