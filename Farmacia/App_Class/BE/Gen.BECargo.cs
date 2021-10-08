using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BECargo : BEBase
    {
        private Int32 _IDCargo; 
        public Int32 IDCargo
        {
            get { return _IDCargo; }
            set { _IDCargo = value; }
        }

        

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        
         
    }
}