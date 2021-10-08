using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEProveedor : BEBase
    {
        private Int32 _IDProveedor;
        public int IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }
         
        private Int32 _IDTipoDocumento;
		public Int32 IDTipoDocumento
        {
			get { return _IDTipoDocumento; }
			set { _IDTipoDocumento = value; }
		}


        private String _NumeroDocumento = String.Empty;
        public String NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }


		private String _RazonSocial = String.Empty;
		public String RazonSocial
		{
			get { return _RazonSocial; }
			set { _RazonSocial = value; }
		}

		private String _NombreComercial = String.Empty;
		public String NombreComercial
		{
			get { return _NombreComercial; }
			set { _NombreComercial = value; }
		}


		private String _IDUbigeo ;
		public String IDUbigeo
		{
			get { return _IDUbigeo; }
			set { _IDUbigeo = value; }
		}


		private String _Direccion = String.Empty;
		public String Direccion
		{
			get { return _Direccion; }
			set { _Direccion = value; }
		}

		private String _Urbanizacion = String.Empty;
		public String Urbanizacion
		{
			get { return _Urbanizacion; }
			set { _Urbanizacion = value; }
		}

		private String _Correo = String.Empty;
		public String Correo
		{
			get { return _Correo; }
			set { _Correo = value; }
		}



		//el nombre de iddocumentoidentificacion
		private String _TipoDocumento = String.Empty;
		public String TipoDocumento
		{
			get { return _TipoDocumento; }
			set { _TipoDocumento = value; }
		}
		//el nombre de idubigeo
		private String _Distrito = String.Empty;
		public String Distrito
		{
			get { return _Distrito; }
			set { _Distrito = value; }
		}

        private String _Ubigeo = String.Empty;
        public String Ubigeo
        {
            get { return _Ubigeo; }
            set { _Ubigeo = value; }
        }

        private String _Celular = String.Empty;
        public String Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }

        private String _NroCategoria = String.Empty;
        public String NroCategoria
        {
            get { return _NroCategoria; }
            set { _NroCategoria = value; }
        }

        private Int32 _Index = 0;
        public Int32 Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        

    }

}