using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BERolProducto : BEBase
    {
        private int _IDRol;
        private int _IDProducto;
        private int _IDEmpresa;        
        private string _Nombre;
        
        public int IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
        }
        public int IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
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
              
    }
}
