using System;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEParametroConfig : BEBase
    {
        private string _IDParametro = String.Empty;
        private int _IDEmpresa;
        private string _Valor = String.Empty;

        public string IDParametro
        {
            get { return _IDParametro; }
            set { _IDParametro = value; }
        }
        public int IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
        }
        public string Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
    }

}