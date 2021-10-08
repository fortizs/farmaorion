using System;

namespace Farmacia.App_Class.BE.Compras
{
    public class BECompraEstado : BEBase
    {
        private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
    }
}
