using System; 

namespace Farmacia.App_Class.BE.Cobranza
{
    public class BECuentaCobrar : BEBase
    {

        private Int32 _IDCuentaCobrar;
        public Int32 IDCuentaCobrar
        {
            get { return _IDCuentaCobrar; }
            set { _IDCuentaCobrar = value; }
        }

        private Int32 _IDCliente;
        public Int32 IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private Int32 _IDTipoComprobante;
        public Int32 IDTipoComprobante
        {
            get { return _IDTipoComprobante; }
            set { _IDTipoComprobante = value; }
        }

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private Int32 _IDGenerico;
        public Int32 IDGenerico
        {
            get { return _IDGenerico; }
            set { _IDGenerico = value; }
        }

        private String _Serie;
        public String Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        private String _Numero;
        public String Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        private DateTime _FechaEmision;
        public DateTime FechaEmision
        {
            get { return _FechaEmision; }
            set { _FechaEmision = value; }
        }

        private DateTime _FechaVencimiento;
        public DateTime FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }
        }

        private Decimal _TipoCambio;
        public Decimal TipoCambio
        {
            get { return _TipoCambio; }
            set { _TipoCambio = value; }
        }

        private Decimal _ImporteTotal;
        public Decimal ImporteTotal
        {
            get { return _ImporteTotal; }
            set { _ImporteTotal = value; }
        }

        private Decimal _PorcentajePercepcion;
        public Decimal PorcentajePercepcion
        {
            get { return _PorcentajePercepcion; }
            set { _PorcentajePercepcion = value; }
        }

        private Decimal _ImporteAfectoPercepcion;
        public Decimal ImporteAfectoPercepcion
        {
            get { return _ImporteAfectoPercepcion; }
            set { _ImporteAfectoPercepcion = value; }
        }

        private Decimal _SaldoDocumento;
        public Decimal SaldoDocumento
        {
            get { return _SaldoDocumento; }
            set { _SaldoDocumento = value; }
        }

        private Decimal _ImporteCalculadoPercepcion;
        public Decimal ImporteCalculadoPercepcion
        {
            get { return _ImporteCalculadoPercepcion; }
            set { _ImporteCalculadoPercepcion = value; }
        }

        private Boolean _ExonerarPercepcion;
        public Boolean ExonerarPercepcion
        {
            get { return _ExonerarPercepcion; }
            set { _ExonerarPercepcion = value; }
        }

        private Boolean _EsGiroNegocio;
        public Boolean EsGiroNegocio
        {
            get { return _EsGiroNegocio; }
            set { _EsGiroNegocio = value; }
        }

        private String _DebeHaber;
        public String DebeHaber
        {
            get { return _DebeHaber; }
            set { _DebeHaber = value; }
        }

        private String _CuentaContable;
        public String CuentaContable
        {
            get { return _CuentaContable; }
            set { _CuentaContable = value; }
        }

        private String _Glosa;
        public String Glosa
        {
            get { return _Glosa; }
            set { _Glosa = value; }
        }

        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        private String _NumeroDocumento;
        public String NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }

        private String _Cliente;
        public String Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private String _TipoComprobante;
        public String TipoComprobante
        {
            get { return _TipoComprobante; }
            set { _TipoComprobante = value; }
        }

        private String _Moneda;
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        private String _MonedaSimbolo;
        public String MonedaSimbolo
        {
            get { return _MonedaSimbolo; }
            set { _MonedaSimbolo = value; }
        }

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }
        

    }
}
