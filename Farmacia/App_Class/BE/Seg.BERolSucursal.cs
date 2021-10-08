using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BERolOficina : BEBase
    {
        private int _IDRol;
        private int _IDOficina;
        private int _IDEmpresa;        
        private string _Nombre;
        private string _Territorio;
              
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
        public int IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
        }        
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public string Territorio
        {
            get { return _Territorio; }
            set { _Territorio = value; }
        }
          
    }
}
