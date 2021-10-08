using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEUnidadMedida : BEBase
    {
        private Int32 _IDUnidadMedida; 
        public Int32 IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }

        private String _Codigo;
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private String _NombreCorto;
        public String NombreCorto
        {
            get { return _NombreCorto; }
            set { _NombreCorto = value; }
        }

		private String _CodigoSunat;
		public String CodigoSunat
		{
			get { return _CodigoSunat; }
			set { _CodigoSunat = value; }
		}

		

	}
}