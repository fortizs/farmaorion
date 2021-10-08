using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE
{
    public class BEWebService : BEBase
    {

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
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

        private Decimal _PrecioVentaSinIgv;
        public Decimal PrecioVentaSinIgv
        {
            get { return _PrecioVentaSinIgv; }
            set { _PrecioVentaSinIgv = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private Int32 _IDUsuarioReg;
        public Int32 IDUsuarioReg
        {
            get { return _IDUsuarioReg; }
            set { _IDUsuarioReg = value; }
        }

        private DateTime _FechaReg;
        public DateTime FechaReg
        {
            get { return _FechaReg; }
            set { _FechaReg = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private String _IDTipoImpuesto;
        public String IDTipoImpuesto
        {
            get { return _IDTipoImpuesto; }
            set { _IDTipoImpuesto = value; }
        }

        private String _IDTipoPrecio;
        public String IDTipoPrecio
        {
            get { return _IDTipoPrecio; }
            set { _IDTipoPrecio = value; }
        }

        private Decimal _PrecioVenta;
        public Decimal PrecioVenta
        {
            get { return _PrecioVenta; }
            set { _PrecioVenta = value; }
        }

        private Decimal _StockActual;
        public Decimal StockActual
        {
            get { return _StockActual; }
            set { _StockActual = value; }
        }

        


    }
}
