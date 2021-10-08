using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BERolMenu : BEBase
    {
        private int _IDRol;
        private int _IDMenu;
        private int _IDModulo;
        private int _IDMenuPadre;
        private string _Nombre;
        private string _Operaciones;
        private bool _ConfigOperacion;
        
        public int IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
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
        public string Operaciones
        {
            get { return _Operaciones; }
            set { _Operaciones = value; }
        }
        public bool ConfigOperacion
        {
            get { return _ConfigOperacion; }
            set { _ConfigOperacion = value; }
        } 
             
    }
}
