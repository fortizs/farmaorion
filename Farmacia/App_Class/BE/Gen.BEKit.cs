using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE
{
    public class BEKit : BEBase
    {

        private Int32 _IDKit;
        public Int32 IDKit
        {
            get { return _IDKit; }
            set { _IDKit = value; }
        }
        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }
        private String _NombreProducto;
        public String NombreProducto
        {
            get { return _NombreProducto; }
            set { _NombreProducto = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private String _Glosa;
        public String Glosa
        {
            get { return _Glosa; }
            set { _Glosa = value; }
        }





        private Int32 _IDUnidadMedida;
        public Int32 IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }
        private Decimal _Cantidad;
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        private Int32 _IDUsuarioCreacion;
        public Int32 IDUsuarioCreacion
        {
            get { return _IDUsuarioCreacion; }
            set { _IDUsuarioCreacion = value; }
        }
        private DateTime _FechaCreacion;
        public DateTime FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }
        private Int32 _IDUsuarioModificacion;
        public Int32 IDUsuarioModificacion
        {
            get { return _IDUsuarioModificacion; }
            set { _IDUsuarioModificacion = value; }
        }
        private DateTime _FechaModificacion;
        public DateTime FechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }
        }





    }
}




