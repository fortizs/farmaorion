using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BERetornoTran: BEBase
    {
        private String _ErrorMensaje = String.Empty;

        private BEBase _pBase; 

        public String ErrorMensaje
        {
            get { return _ErrorMensaje; }
            set { _ErrorMensaje = value; }
        }
        private String _Retorno = "-1";

        public String Retorno
        {
            get { return _Retorno; }
            set { _Retorno = value; }
        }

        private String _Retorno2 = "0";

        public String Retorno2
        {
            get { return _Retorno2; }
            set { _Retorno2 = value; }
        }

		private String _Retorno3 = "0";

		public String Retorno3
		{
			get { return _Retorno3; }
			set { _Retorno3 = value; }
		}

		public BEBase pBase
        {
            get { return _pBase;  }
            set { _pBase = value; }
        }

    }
}
