using System;

namespace Farmacia.App_Class.BE.General
{
    public class BEEmpresa : BEBase
    {

        private Int32 _IDEmpresa;
        public Int32 IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
        }

        private Int32 _IDTipoDocumento;
        public Int32 IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }

        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        private Int32 _TipoDocumentoSunat;
        public Int32 TipoDocumentoSunat
        {
            get { return _TipoDocumentoSunat; }
            set { _TipoDocumentoSunat = value; }
        }

        private Int32 _IDTema;
        public Int32 IDTema
        {
            get { return _IDTema; }
            set { _IDTema = value; }
        }

        private String _Ruc;
        public String Ruc
        {
            get { return _Ruc; }
            set { _Ruc = value; }
        }

        private String _RazonSocial;
        public String RazonSocial
        {
            get { return _RazonSocial; }
            set { _RazonSocial = value; }
        }

        private String _NombreComercial;
        public String NombreComercial
        {
            get { return _NombreComercial; }
            set { _NombreComercial = value; }
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

        private String _Urbanizacion;
        public String Urbanizacion
        {
            get { return _Urbanizacion; }
            set { _Urbanizacion = value; }
        }

        private String _Pais;
        public String Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }

        private String _Departamento;
        public String Departamento
        {
            get { return _Departamento; }
            set { _Departamento = value; }
        }

        private String _Provincia;
        public String Provincia
        {
            get { return _Provincia; }
            set { _Provincia = value; }
        }

        private String _Distrito;
        public String Distrito
        {
            get { return _Distrito; }
            set { _Distrito = value; }
        }

        private Boolean _EsPrincipal;
        public Boolean EsPrincipal
        {
            get { return _EsPrincipal; }
            set { _EsPrincipal = value; }
        }
         
        private Int32 _IDEmailSMTP;
        public Int32 IDEmailSMTP
        {
            get { return _IDEmailSMTP; }
            set { _IDEmailSMTP = value; }
        }

        private Int32 _IDSunat;
        public Int32 IDSunat
        {
            get { return _IDSunat; }
            set { _IDSunat = value; }
        }
         
        private String _Certificado;
        public String Certificado
        {
            get { return _Certificado; }
            set { _Certificado = value; }
        }

        private String _ClaveCertificado;
        public String ClaveCertificado
        {
            get { return _ClaveCertificado; }
            set { _ClaveCertificado = value; }
        }

        private String _UsuarioSol;
        public String UsuarioSol
        {
            get { return _UsuarioSol; }
            set { _UsuarioSol = value; }
        }

        private String _ClaveSol;
        public String ClaveSol
        {
            get { return _ClaveSol; }
            set { _ClaveSol = value; }
        }

        private String _Correo;
        public String Correo
        {
            get { return _Correo; }
            set { _Correo = value; }
        }

        private String _ClaveCorreo;
        public String ClaveCorreo
        {
            get { return _ClaveCorreo; }
            set { _ClaveCorreo = value; }
        }

        private String _CuentaDetraccion;
        public String CuentaDetraccion
        {
            get { return _CuentaDetraccion; }
            set { _CuentaDetraccion = value; }
        }

        private String _CuentaBancaria;
        public String CuentaBancaria
        {
            get { return _CuentaBancaria; }
            set { _CuentaBancaria = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

       

        /**********************/

        private Int32 _IDArchivoEmpresa;
        public Int32 IDArchivoEmpresa
        {
            get { return _IDArchivoEmpresa; }
            set { _IDArchivoEmpresa = value; }
        }

        private String _TipoArchivo;
        public String TipoArchivo
        {
            get { return _TipoArchivo; }
            set { _TipoArchivo = value; }
        }

        private String _RutaArchivo;
        public String RutaArchivo
        {
            get { return _RutaArchivo; }
            set { _RutaArchivo = value; }
        }

        private String _NombreArchivo;
        public String NombreArchivo
        {
            get { return _NombreArchivo; }
            set { _NombreArchivo = value; }
        }
          
        private String _Host;
        public String Host
        {
            get { return _Host; }
            set { _Host = value; }
        }

        private Int32 _Puerto;
        public Int32 Puerto
        {
            get { return _Puerto; }
            set { _Puerto = value; }
        }

        private Boolean _HabilitarSSL;
        public Boolean HabilitarSSL
        {
            get { return _HabilitarSSL; }
            set { _HabilitarSSL = value; }
        }

        private Boolean _DefaultCredencial;
        public Boolean DefaultCredencial
        {
            get { return _DefaultCredencial; }
            set { _DefaultCredencial = value; }
        }
         
        private String _EndPointUrl;
        public String EndPointUrl
        {
            get { return _EndPointUrl; }
            set { _EndPointUrl = value; }
        }

		private String _SalidaAlmacen;
		public String SalidaAlmacen
		{
			get { return _SalidaAlmacen; }
			set { _SalidaAlmacen = value; }
		}


		private String _IngresoAlmacen;
		public String IngresoAlmacen
		{
			get { return _IngresoAlmacen; }
			set { _IngresoAlmacen = value; }
		}


		private String _ImpresionVenta;
		public String ImpresionVenta
		{
			get { return _ImpresionVenta; }
			set { _ImpresionVenta = value; }
		}


		private String _CodigoEstablecimiento;
		public String CodigoEstablecimiento
		{
			get { return _CodigoEstablecimiento; }
			set { _CodigoEstablecimiento = value; }
		}

		private String _TerminoCondicion;
		public String TerminoCondicion
		{
			get { return _TerminoCondicion; }
			set { _TerminoCondicion = value; }
		}
		

	}
}