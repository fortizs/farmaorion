using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BERol : BEBase
    {
        private int _IDRol;
        private int _IDEmpresa;
        private int _IDPerfil;
        private string _Nombre = String.Empty;
        private string _Empresa = String.Empty;
        private string _Perfil = String.Empty;
         
        public int IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
        }
        //public int IDEmpresa
        //{
        //    get { return _IDEmpresa; }
        //    set { _IDEmpresa = value; }
        //}
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
        public string Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }        
        public string Perfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }
             
    }
}
