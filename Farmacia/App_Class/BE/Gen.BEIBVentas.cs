using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
	public class BEIBVentas : BEBase
	{

		private Int32 _IDCliente;
		public Int32 IDCliente
		{
			get { return _IDCliente; }
			set { _IDCliente = value; }
		}

        private Int32 _Anio;
        public Int32 Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        private Int32 _IDMes;
        public Int32 IDMes
        {
            get { return _IDMes; }
            set { _IDMes = value; }
        }

        private Int32 _IDTipoMeta;
        public Int32 IDTipoMeta
        {
            get { return _IDTipoMeta; }
            set { _IDTipoMeta = value; }
        }

        private Int32 _IDDia;
        public Int32 IDDia
        {
            get { return _IDDia; }
            set { _IDDia = value; }
        }

        private Int32 _IDColaborador;
        public Int32 IDColaborador
        {
            get { return _IDColaborador; }
            set { _IDColaborador = value; }
        }

        private String _Colaborador;
        public String Colaborador
        {
            get { return _Colaborador; }
            set { _Colaborador = value; }
        }

        private String _Foto;
        public String Foto
        {
            get { return _Foto; }
            set { _Foto = value; }
        }

        private String _Cliente;
		public String Cliente
		{
			get { return _Cliente; }
			set { _Cliente = value; }
		}

		private Int32 _CantidadServicio;
		public Int32 CantidadServicio
		{
			get { return _CantidadServicio; }
			set { _CantidadServicio = value; }
		}

		private Decimal _MontoServicio;
		public Decimal MontoServicio
		{
			get { return _MontoServicio; }
			set { _MontoServicio = value; }
		}

        private Decimal _MontoMeta;
        public Decimal MontoMeta
        {
            get { return _MontoMeta; }
            set { _MontoMeta = value; }
        }

        private Decimal _TotalCompra;
		public Decimal TotalCompra
		{
			get { return _TotalCompra; }
			set { _TotalCompra = value; }
		}

		private Decimal _TotalVenta;
		public Decimal TotalVenta
		{
			get { return _TotalVenta; }
			set { _TotalVenta = value; }
		}


		private String _Servicio;
		public String Servicio
		{
			get { return _Servicio; }
			set { _Servicio = value; }
		}

		private String _Sucursal;
		public String Sucursal
		{
			get { return _Sucursal; }
			set { _Sucursal = value; }
		}

		private String _TipoServicio;
		public String TipoServicio
		{
			get { return _TipoServicio; }
			set { _TipoServicio = value; }
		}

		private String _FechaInicio;
		public String FechaInicio
		{
			get { return _FechaInicio; }
			set { _FechaInicio = value; }
		}

		private String _FechaFin;
		public String FechaFin
		{
			get { return _FechaFin; }
			set { _FechaFin = value; }
		}


        private String _NombreProducto;
        public String NombreProducto
        {
            get { return _NombreProducto; }
            set { _NombreProducto = value; }
        }

        private String _NombreCategoria;
        public String NombreCategoria
        {
            get { return _NombreCategoria; }
            set { _NombreCategoria = value; }
        }

        private String _NombreUnidadMedida;
        public String NombreUnidadMedida
        {
            get { return _NombreUnidadMedida; }
            set { _NombreUnidadMedida = value; }
        }

        private Decimal _Cantidad;
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

		private Int32 _IDFormaPago;
		public Int32 IDFormaPago
		{
			get { return _IDFormaPago; }
			set { _IDFormaPago = value; }
		}

		private String _FormaPago;
		public String FormaPago
		{
			get { return _FormaPago; }
			set { _FormaPago = value; }
		}

		private Decimal _Total;
		public Decimal Total
		{
			get { return _Total; }
			set { _Total = value; }
		}

		private Decimal _Utilidad;
		public Decimal Utilidad
		{
			get { return _Utilidad; }
			set { _Utilidad = value; }
		}
		
	}
}
