using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEUsuario : BEBase
    {
        
        private int _IDEmpresa;
        private int _IDPerfil;
        private string _NumeroDocumento = String.Empty;
        private string _NombreCompleto = String.Empty;
        private string _Usuario = String.Empty;
        private string _Mensaje = String.Empty;
        private string _UsuarioDominio = String.Empty;
        private string _Clave = String.Empty;
        private string _Tipo = String.Empty;
        private string _TipoEstado = String.Empty;
        private string _Email = String.Empty;
        private string _Telefono = String.Empty;
        //Colaborador
        private int _IDColaborador=0;
        private Int32 _IDOficina;
        //private Int32 _IDSucursal;
        private Int32 _IDCanal;
        private Int32 _IDCargo;
        private string _Oficina = String.Empty;
        private String _CodColaborador = String.Empty;
        private bool _EstadoUsuario;
        private bool _EstadoColaborador;
           
        private bool _Acceso;
        private bool _Bloqueado;
        private int _IDUsuarioAuditoria;
        private IList _ListaRol;
        private string _LogoEmpresa = String.Empty;
        
        private string _CodigoCultura = String.Empty;
        private bool _CambiarClave = false;
        private string _Empresa = String.Empty;

        private string _UltimoLogin;
        private string _UltimoBloqueo;

        private String _Nombres;
        private String _ApellidoPaterno;
        private String _ApellidoMaterno;
        public String Nombres
        {
            get { return _Nombres; }
            set { _Nombres = value; }
        }

        public String Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value; }
        }


        public String ApellidoPaterno
        {
            get { return _ApellidoPaterno; }
            set { _ApellidoPaterno = value; }
        }

        public String ApellidoMaterno
        {
            get { return _ApellidoMaterno; }
            set { _ApellidoMaterno = value; }
        }
          
        public string Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }

        //public Int32 IDSucursal
        //{
        //    get { return _IDSucursal; }
        //    set { _IDSucursal = value; }
        //}
		
        public int IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
        }
        public int IDPerfil
        {
            get { return _IDPerfil; }
            set { _IDPerfil = value; }
        }
        public string NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }
        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }
        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public string UsuarioDominio
        {
            get { return _UsuarioDominio; }
            set { _UsuarioDominio = value; }
        }
        public string Clave
        {
            get { return _Clave; }
            set { _Clave = GetMD5(value); }
        }
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public string TipoEstado
        {
            get { return _TipoEstado; }
            set { _TipoEstado = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public int IDColaborador
        {
            get { return _IDColaborador; }
            set { _IDColaborador = value; }
        }
        public Int32 IDOficina
        {
            get { return _IDOficina; }
            set { _IDOficina = value; }
        }
        public string Oficina
        {
            get { return _Oficina; }
            set { _Oficina = value; }
        }
        public Int32 IDCanal
        {
            get { return _IDCanal; }
            set { _IDCanal = value; }
        }        
        public Int32 IDCargo
        {
            get { return _IDCargo; }
            set { _IDCargo = value; }
        }
        public String CodColaborador
        {
            get { return _CodColaborador; }
            set { _CodColaborador = value; }
        }
        public bool EstadoUsuario
        {
            get { return _EstadoUsuario; }
            set { _EstadoUsuario = value; }
        }
        public bool EstadoColaborador
        {
            get { return _EstadoColaborador; }
            set { _EstadoColaborador = value; }
        }
        public bool Acceso
        {
            get { return _Acceso; }
            set { _Acceso = value; }
        }
        public bool Bloqueado
        {
            get { return _Bloqueado; }
            set { _Bloqueado = value; }
        }
        public int IDUsuarioAuditoria
        {
            get { return _IDUsuarioAuditoria; }
            set { _IDUsuarioAuditoria = value; }
        }
        public IList ListaRol
        {
            get { return _ListaRol; }
            set { _ListaRol = value; }
        }

        public String LogoEmpresa
        {
            get { return _LogoEmpresa; }
            set { _LogoEmpresa = value; }        
        }


        

        public String CodigoCultura
        {
            get { return _CodigoCultura; }
            set { _CodigoCultura = value; }
        }

        public Boolean CambiarClave
        {
            get { return _CambiarClave; }
            set { _CambiarClave = value; }
        }

        public string UltimoLogin
        {
            get { return _UltimoLogin; }
            set { _UltimoLogin = value; }
        }

        public string UltimoBloqueo
        {
            get { return _UltimoBloqueo; }
            set { _UltimoBloqueo = value; }
        }

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        private String _Sucursal; 
        public string Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _ColorInterfaz;
        public string ColorInterfaz
        {
            get { return _ColorInterfaz; }
            set { _ColorInterfaz = value; }
        }

        private Boolean _EsPrincipal;
        public Boolean EsPrincipal
        {
            get { return _EsPrincipal; }
            set { _EsPrincipal = value; }
        }
         
        public string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString().ToUpper();
        }

    }
}
