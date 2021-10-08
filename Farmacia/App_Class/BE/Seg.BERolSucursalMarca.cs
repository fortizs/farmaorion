using System;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BERolOficinaMarca : BEBase
    {
        private int _IDRol;
        private int _IDOficina;
        private int _IDMarca;
        private String _Nombre;
        
        public int IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
        }
        public int IDOficina
        {
            get { return _IDOficina; }
            set { _IDOficina = value; }
        }
        public int IDMarca
        {
            get { return _IDMarca; }
            set { _IDMarca = value; }
        }
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        
    }

}
