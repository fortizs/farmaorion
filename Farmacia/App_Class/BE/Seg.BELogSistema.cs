using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BELogSistema: BEBase
    {
        private Int32 _IDLogSistema = 0;
        private Int32 _IDSistema = 0;
        private Int32 _IDModulo = 0;
        private string _Modulo = "";
        private DateTime _Fecha;
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        private string _TipoFiltro = "";
        private string _Compilado = "";
        private string _Host = "";
        private string _Opcion = "";
        private string _Evento = "";
        private string _MensajeError = "";
        private string _Detalle = "";
		private String _Usuario = "";

		

		public Int32 IDModulo
        {
            get { return _IDModulo; }
            set { _IDModulo = value; }
        }

		public String Usuario
		{
			get { return _Usuario; }
			set { _Usuario = value; }
		}

		
		public Int32 IDLogSistema
        {
            get { return _IDLogSistema; }
            set { _IDLogSistema = value; }
        }

        public Int32 IDSistema
        {
            get { return _IDSistema; }
            set { _IDSistema = value; }
        }

        public string Modulo
        {
            get { return _Modulo; }
            set { _Modulo = value; }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        public DateTime FechaDesde
        {
            get { return _FechaDesde; }
            set { _FechaDesde = value; }
        }

        public DateTime FechaHasta
        {
            get { return _FechaHasta; }
            set { _FechaHasta = value; }
        }

        public string TipoFiltro
        {
            get { return _TipoFiltro; }
            set { _TipoFiltro = value; }
        }

        public string Compilado
        {
            get { return _Compilado; }
            set { _Compilado = value; }
        }
        
        
        public string Host
        {
            get { return _Host; }
            set { _Host = value; }
        }        

        public string Opcion
        {
            get { return _Opcion; }
            set { _Opcion = value; }
        }

        public string Evento
        {
            get { return _Evento; }
            set { _Evento = value; }
        }

        public string MensajeError
        {
            get { return _MensajeError; }
            set { _MensajeError = value; }
        }

        public string Detalle
        {
            get { return _Detalle; }
            set { _Detalle = value; }
        }

    }
}
