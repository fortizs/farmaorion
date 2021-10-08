using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEPerfil : BEBase
    {
        private int _IDPerfil;
        private string _Nombre = String.Empty;
            

        public int IDPerfil
        {
            get { return _IDPerfil; }
            set { _IDPerfil = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
               
    }

}
