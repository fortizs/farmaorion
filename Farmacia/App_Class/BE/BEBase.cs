using System;

namespace Farmacia.App_Class.BE
{
    public class BEBase
    {
        private String _TipoBuscar;
        private String _Buscar;
        private Int32 _IDUsuario;
        private Int32 _IDCategoria;
        private Int32 _IDSucursal;
        private Int32 _IDEmpresa;
        private Int32 _IDIdioma;
        private Boolean _Estado;
        private Boolean _Eliminado;

        public String TipoBuscar
        {
            get { return _TipoBuscar; }
            set { _TipoBuscar = value; }
        }

        public String Buscar
        {
            get { return _Buscar; }
            set { _Buscar = value; }
        }

        public Int32 IDUsuario
        {
            get { return _IDUsuario; }
            set { _IDUsuario = value; }
        }

        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        public Int32 IDCategoria
        {
            get { return _IDCategoria; }
            set { _IDCategoria = value; }
        }

        public Int32 IDIdioma
        {
            get { return _IDIdioma; }
            set { _IDIdioma = value; }
        }

        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public Boolean Eliminado
        {
            get { return _Eliminado; }
            set { _Eliminado = value; }
        }

        public Int32 IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
        }

		private DateTime _FechaCreacion;
		public DateTime FechaCreacion
		{
			get { return _FechaCreacion; }
			set { _FechaCreacion = value; }
		}

		private String _UsuarioModificacion;
		public String UsuarioModificacion
		{
			get { return _UsuarioModificacion; }
			set { _UsuarioModificacion = value; }
		}

		private DateTime _FechaModificacion;
		public DateTime FechaModificacion
		{
			get { return _FechaModificacion; }
			set { _FechaModificacion = value; }
		}


		
	}
}