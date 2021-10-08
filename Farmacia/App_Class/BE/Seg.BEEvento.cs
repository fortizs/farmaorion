using System;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEEvento : BEBase
    {
        private int _IDEvento;
       
        private int _IDTipoEvento;
        private string _TipoEvento;
        private DateTime _FechaRegistro;
        private string _Detalle = String.Empty;
        private string _Host = String.Empty;
        private string _HostDetalles = String.Empty;
        private string _Navegador = String.Empty;
        private string _NavegadorDetalles = String.Empty;

        public int IDEvento
        {
            get { return _IDEvento; }
            set { _IDEvento = value; }
        }
       
        public int IDTipoEvento
        {
            get { return _IDTipoEvento; }
            set { _IDTipoEvento = value; }
        }
        public string TipoEvento
        {
            get { return _TipoEvento; }
            set { _TipoEvento = value; }
        }
        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public string Detalle
        {
            get { return _Detalle; }
            set { _Detalle = value; }
        }
        public string Host
        {
            get { return _Host; }
            set { _Host = value; }
        }
        public string HostDetalles
        {
            get { return _HostDetalles; }
            set { _HostDetalles = value; }
        }
        public string Navegador
        {
            get { return _Navegador; }
            set { _Navegador = value; }
        }
        public string NavegadorDetalles
        {
            get { return _NavegadorDetalles; }
            set { _NavegadorDetalles = value; }
        }
    }

}