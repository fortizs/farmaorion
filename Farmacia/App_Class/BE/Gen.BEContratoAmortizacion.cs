using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEContratoAmortizacion : BEBase
    {
        private Int32 _IDVenta;
        public Int32 IDVenta
        {
            get { return _IDVenta; }
            set { _IDVenta = value; }
        }
         
        private Int32 _IDContratoAmortizacion;
        public Int32 IDContratoAmortizacion
        {
            get { return _IDContratoAmortizacion; }
            set { _IDContratoAmortizacion = value; }
        }

        private Int32 _IDContrato;
        public Int32 IDContrato
        {
            get { return _IDContrato; }
            set { _IDContrato = value; }
        }

        private DateTime _FechaAmortizacion;
        public DateTime FechaAmortizacion
        {
            get { return _FechaAmortizacion; }
            set { _FechaAmortizacion = value; }
        }

        private Decimal _MontoAmortizado;
        public Decimal MontoAmortizado
        {
            get { return _MontoAmortizado; }
            set { _MontoAmortizado = value; }
        }

        private String _Observacion;
        public String Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }

        private DateTime _FechaVenta;
        public DateTime FechaVenta
        {
            get { return _FechaVenta; }
            set { _FechaVenta = value; }
        }

        private Decimal _TotalVenta;
        public Decimal TotalVenta
        {
            get { return _TotalVenta; }
            set { _TotalVenta = value; }
        }

        private String _Colaborador;
        public String Colaborador
        {
            get { return _Colaborador; }
            set { _Colaborador = value; }
        }

        private DateTime _FechaReg;
        public DateTime FechaReg
        {
            get { return _FechaReg; }
            set { _FechaReg = value; }
        }

        private String _FormaPago;
        public String FormaPago
        {
            get { return _FormaPago; }
            set { _FormaPago = value; }
        }

        private Boolean _GeneroComprobante;
        public Boolean GeneroComprobante
        {
            get { return _GeneroComprobante; }
            set { _GeneroComprobante = value; }
        }
        


    }
}
