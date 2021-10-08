using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEMenu : BEBase
    {
        private int _IDMenu=0;
        private int _IDModulo;
        private int _IDMenuPadre;
        private string _Nombre = String.Empty;
        private string _Url = String.Empty;
        private string _Modulo = String.Empty;
        private string _ModuloIcono = String.Empty;
        private string _RutaNavegacion = String.Empty;
        private int _Orden;
        private bool _Visible;
		private string _Icono = String.Empty;

		public string Icono
		{
			get { return _Icono; }
			set { _Icono = value; }
		}

		public int IDMenu
        {
            get { return _IDMenu; }
            set { _IDMenu = value; }
        }
        public int IDModulo
        {
            get { return _IDModulo; }
            set { _IDModulo = value; }
        }
        public int IDMenuPadre
        {
            get { return _IDMenuPadre; }
            set { _IDMenuPadre = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        public string Modulo
        {
            get { return _Modulo; }
            set { _Modulo = value; }
        }
        public string ModuloIcono
        {
            get { return _ModuloIcono; }
            set { _ModuloIcono = value; }
        }
        public string RutaNavegacion
        {
            get { return _RutaNavegacion; }
            set { _RutaNavegacion = value; }
        }
        public int Orden
        {
            get { return _Orden; }
            set { _Orden = value; }
        }
       
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }
    }

}