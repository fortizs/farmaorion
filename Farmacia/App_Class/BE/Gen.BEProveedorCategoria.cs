using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEProveedorCategoria : BEBase
    {

        private Int32 _IDProveedorCategoria;
        public Int32 IDProveedorCategoria
        {
            get { return _IDProveedorCategoria; }
            set { _IDProveedorCategoria = value; }
        }

        private Int32 _IDProveedor;
        public Int32 IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private Int32 _IDCategoria;
        public Int32 IDCategoria
        {
            get { return _IDCategoria; }
            set { _IDCategoria = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private String _Categoria;
        public String Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; }
        }



    }
}
