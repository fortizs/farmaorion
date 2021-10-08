using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BESucursal : BEBase
    {

        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private Int32 _IDEmpresa;
        public Int32 IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
         
        private String _Telefono;
        public String Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        private String _Celular;
        public String Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }

        private String _Email;
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private String _IDUbigeo;
        public String IDUbigeo
        {
            get { return _IDUbigeo; }
            set { _IDUbigeo = value; }
        }

        private String _Ubigeo;
        public String Ubigeo
        {
            get { return _Ubigeo; }
            set { _Ubigeo = value; }
        }

        private String _Direccion;
        public String Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

		private String _Ruc;
		public String Ruc
		{
			get { return _Ruc; }
			set { _Ruc = value; }
		}

		private String _Empresa;
		public String Empresa
		{
			get { return _Empresa; }
			set { _Empresa = value; }
		}

		private String _NombreComercial;
		public String NombreComercial
		{
			get { return _NombreComercial; }
			set { _NombreComercial = value; }
		}
		 

    }
}



