using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEModulo : BEBase
    {
        private int _IDModulo;
        private string _Nombre = String.Empty;
        private string _Imagen = String.Empty;
        private string _Icono = String.Empty;
        private bool _VisibleSinPermiso;
        private string _Url = String.Empty;
        private string _Descripcion = String.Empty;
        private bool _Acceso;
        private int _Orden;
       
        private int _Espacio;

        public int IDModulo
        {
            get { return _IDModulo; }
            set { _IDModulo = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string Imagen
        {
            get { return _Imagen; }
            set { _Imagen = value; }
        }
        public string Icono
        {
            get { return _Icono; }
            set { _Icono = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public bool VisibleSinPermiso
        {
            get { return _VisibleSinPermiso; }
            set { _VisibleSinPermiso = value; }
        }
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        public bool Acceso
        {
            get { return _Acceso; }
            set { _Acceso = value; }
        }
        public int Orden
        {
            get { return _Orden; }
            set { _Orden = value; }
        }
      
        public int Espacio
        {
            get { return _Espacio; }
            set { _Espacio = value; }
        }

    }

}