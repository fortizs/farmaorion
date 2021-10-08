using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEPerfilMenu : BEBase
    {
        private int _IDPerfil;
        private int _IDMenu;
        private int _IDModulo;
        private int _IDMenuPadre;
        private string _Nombre;
        private string _IDPais;
        public string IDPais
        {
            get { return _IDPais; }
            set { _IDPais = value; }
        }
      
        public int IDPerfil
        {
            get { return _IDPerfil; }
            set { _IDPerfil = value; }
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
             
    }
}
