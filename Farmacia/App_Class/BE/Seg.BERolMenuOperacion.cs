using System;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BERolMenuOperacion : BEBase
    {
        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private Int32 _IDRol;
        public Int32 IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
        }

        private Int32 _IDMenu;
        public Int32 IDMenu
        {
            get { return _IDMenu; }
            set { _IDMenu = value; }
        }

        private String _Nombre;

        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private Int32 _IDOperacion;
        public Int32 IDOperacion
        {
            get { return _IDOperacion; }
            set { _IDOperacion = value; }
        }

        private Boolean _Acceso;
        public Boolean Acceso
        {
            get { return _Acceso; }
            set { _Acceso = value; }
        }
    }
}