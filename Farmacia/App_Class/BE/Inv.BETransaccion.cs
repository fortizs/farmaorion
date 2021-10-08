using System;

namespace Farmacia.App_Class.BE.Inventario
{
    public class BETransaccion : BEBase
    {

        private Int32 _IDTransaccion;
        public Int32 IDTransaccion
        {
            get { return _IDTransaccion; }
            set { _IDTransaccion = value; }
        }

        private String _Codigo;
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        private String _TipoMovimiento;
        public String TipoMovimiento
        {
            get { return _TipoMovimiento; }
            set { _TipoMovimiento = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private String _TipoMovimientoNombre;
        public String TipoMovimientoNombre
        {
            get { return _TipoMovimientoNombre; }
            set { _TipoMovimientoNombre = value; }
        }

    }
}
