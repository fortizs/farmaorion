using System;

namespace Farmacia.App_Class.BE.Inventario
{
    public class BEAlmacen : BEBase
    {

        private Int32 _IDAlmacen;
        public Int32 IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
        }

        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
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

        private String _IDUbigeo;
        public String IDUbigeo
        {
            get { return _IDUbigeo; }
            set { _IDUbigeo = value; }
        }

        private String _Direccion;
        public String Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        private String _Telefono;
        public String Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        private String _Celular;
        public String Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }

        private Int32 _NumeroNotaIngreso;
        public Int32 NumeroNotaIngreso
        {
            get { return _NumeroNotaIngreso; }
            set { _NumeroNotaIngreso = value; }
        }

        private Int32 _NumeroNotaSalida;
        public Int32 NumeroNotaSalida
        {
            get { return _NumeroNotaSalida; }
            set { _NumeroNotaSalida = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _Ubigeo;
        public String Ubigeo
        {
            get { return _Ubigeo; }
            set { _Ubigeo = value; }
        }

    }
}
