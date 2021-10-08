using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEGuiaRemision : BEBase
    {
        private Int32 _Codigo;
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private Int32 _IDGuiaRemision;
        public Int32 IDGuiaRemision
        {
            get { return _IDGuiaRemision; }
            set { _IDGuiaRemision = value; }
        }

        private Int32 _IDEmpresaTransporte;
        public Int32 IDEmpresaTransporte
        {
            get { return _IDEmpresaTransporte; }
            set { _IDEmpresaTransporte = value; }
        }

        private Int32 _IDConductor;
        public Int32 IDConductor
        {
            get { return _IDConductor; }
            set { _IDConductor = value; }
        }

        private Int32 _IDUnidadTransporte;
        public Int32 IDUnidadTransporte
        {
            get { return _IDUnidadTransporte; }
            set { _IDUnidadTransporte = value; }
        }

        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private Int32 _IDCliente;
        public Int32 IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private Int32 _IDMotivoTraslado;
        public Int32 IDMotivoTraslado
        {
            get { return _IDMotivoTraslado; }
            set { _IDMotivoTraslado = value; }
        }

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private String _PuntoPartida;
        public String PuntoPartida
        {
            get { return _PuntoPartida; }
            set { _PuntoPartida = value; }
        }

        private String _PuntoLlegada;
        public String PuntoLlegada
        {
            get { return _PuntoLlegada; }
            set { _PuntoLlegada = value; }
        }

        private DateTime _FechaInicioTraslado;
        public DateTime FechaInicioTraslado
        {
            get { return _FechaInicioTraslado; }
            set { _FechaInicioTraslado = value; }
        }

        private String _TipoOperacion;
        public String TipoOperacion
        {
            get { return _TipoOperacion; }
            set { _TipoOperacion = value; }
        }

        private String _NumeroOperacion;
        public String NumeroOperacion
        {
            get { return _NumeroOperacion; }
            set { _NumeroOperacion = value; }
        }

        private Int32 _CodigoOperacion;
        public Int32 CodigoOperacion
        {
            get { return _CodigoOperacion; }
            set { _CodigoOperacion = value; }
        }
         
        private String _ComprobantePago;
        public String ComprobantePago
        {
            get { return _ComprobantePago; }
            set { _ComprobantePago = value; }
        }

        private String _EmpresaTransporte;
        public String EmpresaTransporte
        {
            get { return _EmpresaTransporte; }
            set { _EmpresaTransporte = value; }
        }

        private String _EmpresaTransporteNumeroDocumento;
        public String EmpresaTransporteNumeroDocumento
        {
            get { return _EmpresaTransporteNumeroDocumento; }
            set { _EmpresaTransporteNumeroDocumento = value; }
        }

        private String _Conductor;
        public String Conductor
        {
            get { return _Conductor; }
            set { _Conductor = value; }
        }

        private String _NumeroLicenciaConducir;
        public String NumeroLicenciaConducir
        {
            get { return _NumeroLicenciaConducir; }
            set { _NumeroLicenciaConducir = value; }
        }

        private String _UnidadTransporte;
        public String UnidadTransporte
        {
            get { return _UnidadTransporte; }
            set { _UnidadTransporte = value; }
        }

        private String _NumeroPlaca;
        public String NumeroPlaca
        {
            get { return _NumeroPlaca; }
            set { _NumeroPlaca = value; }
        }

        private String _MotivoTraslado;
        public String MotivoTraslado
        {
            get { return _MotivoTraslado; }
            set { _MotivoTraslado = value; }
        }

        private String _Cliente;
        public String Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private String _ClienteNumeroDocumento;
        public String ClienteNumeroDocumento
        {
            get { return _ClienteNumeroDocumento; }
            set { _ClienteNumeroDocumento = value; }
        }



    }
}
