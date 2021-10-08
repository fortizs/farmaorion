using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEUsuarioRol : BEBase
    {
        private int _IDUsuarioAdmin;
        private int _IDRol;
        private int _IDEmpresa;
        private int _IDPerfil;
        private string _Nombre;
        private string _Perfil;
        private string _Rol;
        
        private string _IDRoles;
        private int _Index;

        public int IDUsuarioAdmin
        {
            get { return _IDUsuarioAdmin; }
            set { _IDUsuarioAdmin = value; }
        }
        public int IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
        }
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
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
       
        public string Perfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }
        public string Rol
        {
            get { return _Rol; }
            set { _Rol = value; }
        }
        public string IDRoles
        {
            get { return _IDRoles; }
            set { _IDRoles = value; }
        }
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }
    }
}
