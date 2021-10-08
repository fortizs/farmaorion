using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEUbigeo : BEBase
    {
        private String _IDUbigeo; 
        public String IDUbigeo
		{
            get { return _IDUbigeo; }
            set { _IDUbigeo = value; }
        }

        private String _IDPais;
        public String IDPais
		{
            get { return _IDPais; }
            set { _IDPais = value; }
        }

        private String _Pais;
        public String Pais
		{
            get { return _Pais; }
            set { _Pais = value; }
        }

        private String _IDDepartamento;
        public String IDDepartamento
		{
            get { return _IDDepartamento; }
            set { _IDDepartamento = value; }
        }

		private String _Departamento;
		public String Departamento
		{
			get { return _Departamento; }
			set { _Departamento = value; }
		}

		private String _IDProvincia;
		public String IDProvincia
		{
			get { return _IDProvincia; }
			set { _IDProvincia = value; }
		}

		private String _Provincia;
		public String Provincia
		{
			get { return _Provincia; }
			set { _Provincia = value; }
		}

		private String _IDDistrito;
		public String IDDistrito
		{
			get { return _IDDistrito; }
			set { _IDDistrito = value; }
		}

		private String _Distrito;
		public String Distrito
		{
			get { return _Distrito; }
			set { _Distrito = value; }
		}

		private String _NombreCompleto;
		public String NombreCompleto
		{
			get { return _NombreCompleto; }
			set { _NombreCompleto = value; }
		}

	}
}