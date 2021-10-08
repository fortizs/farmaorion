using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Inventario
{
    public class BEReporteMovimiento : BEBase
    {


        private String _IDMovimiento;
        public String IDMovimiento
        {
            get { return _IDMovimiento; }
            set { _IDMovimiento = value; }
        }
        private String _Entidad;
        public String Entidad
        {
            get { return _Entidad; }
            set { _Entidad = value; }
        }
        private String _Transaccion;
        public String Transaccion
        {
            get { return _Transaccion; }
            set { _Transaccion = value; }
        }
        private DateTime _FechaMovimiento;
        public DateTime FechaMovimiento
        {
            get { return _FechaMovimiento; }
            set { _FechaMovimiento = value; }
        }
        private String _TipoMovimiento;
        public String TipoMovimiento
        {
            get { return _TipoMovimiento; }
            set { _TipoMovimiento = value; }
        }
        private String _AlmacenOrigen;
        public String AlmacenOrigen
        {
            get { return _AlmacenOrigen; }
            set { _AlmacenOrigen = value; }
        }
        private String _AlmacenDestino;
        public String AlmacenDestino
        {
            get { return _AlmacenDestino; }
            set { _AlmacenDestino = value; }
        }
        private String _Producto;
        public String Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }
        private Decimal _Cantidad;
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
       
        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }
        private Decimal _ValorUnidad;
        public Decimal ValorUnidad
        {
            get { return _ValorUnidad; }
            set { _ValorUnidad = value; }
        }
        private Decimal _ValorTotal;
        public Decimal ValorTotal
        {
            get { return _ValorTotal; }
            set { _ValorTotal = value; }
        }

    }
}