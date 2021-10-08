using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Laboratorio
{
	public class BEOrden : BEBase
	{
		private Int32 _IDOrden;
		public Int32 IDOrden
		{
			get { return _IDOrden; }
			set { _IDOrden = value; }
		}
		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}
		private Int32 _IDCliente;
		public Int32 IDCliente
		{
			get { return _IDCliente; }
			set { _IDCliente = value; }
		}
		private Int32 _IDClienteConvenio;
		public Int32 IDClienteConvenio
		{
			get { return _IDClienteConvenio; }
			set { _IDClienteConvenio = value; }
		}
		private String _IDMoneda;
		public String IDMoneda
		{
			get { return _IDMoneda; }
			set { _IDMoneda = value; }
		}
		private Int32 _Anio;
		public Int32 Anio
		{
			get { return _Anio; }
			set { _Anio = value; }
		}
		private Int32 _NumeroOrden;
		public Int32 NumeroOrden
		{
			get { return _NumeroOrden; }
			set { _NumeroOrden = value; }
		}
		private DateTime _FechaOrden;
		public DateTime FechaOrden
		{
			get { return _FechaOrden; }
			set { _FechaOrden = value; }
		}
		private Decimal _TotalOperacionGravada;
		public Decimal TotalOperacionGravada
		{
			get { return _TotalOperacionGravada; }
			set { _TotalOperacionGravada = value; }
		}
		private Decimal _TotalIgv;
		public Decimal TotalIgv
		{
			get { return _TotalIgv; }
			set { _TotalIgv = value; }
		}
		private Decimal _TotalOrden;
		public Decimal TotalOrden
		{
			get { return _TotalOrden; }
			set { _TotalOrden = value; }
		}
		private Int32 _IDEstado;
		public Int32 IDEstado
		{
			get { return _IDEstado; }
			set { _IDEstado = value; }
		}
		private String _ClienteNumeroDocumento;
		public String ClienteNumeroDocumento
		{
			get { return _ClienteNumeroDocumento; }
			set { _ClienteNumeroDocumento = value; }
		}
		private String _ClienteRazonSocial;
		public String ClienteRazonSocial
		{
			get { return _ClienteRazonSocial; }
			set { _ClienteRazonSocial = value; }
		}
		private String _ClienteSexoNombre;
		public String ClienteSexoNombre
		{
			get { return _ClienteSexoNombre; }
			set { _ClienteSexoNombre = value; }
		}
		private String _ClienteCelular1;
		public String ClienteCelular1
		{
			get { return _ClienteCelular1; }
			set { _ClienteCelular1 = value; }
		}
		private String _ClienteCelular2;
		public String ClienteCelular2
		{
			get { return _ClienteCelular2; }
			set { _ClienteCelular2 = value; }
		}
		private String _ClienteCorreo;
		public String ClienteCorreo
		{
			get { return _ClienteCorreo; }
			set { _ClienteCorreo = value; }
		}
		private String _ClienteCONumeroDocumento;
		public String ClienteCONumeroDocumento
		{
			get { return _ClienteCONumeroDocumento; }
			set { _ClienteCONumeroDocumento = value; }
		}
		private String _ClienteCORazonSocial;
		public String ClienteCORazonSocial
		{
			get { return _ClienteCORazonSocial; }
			set { _ClienteCORazonSocial = value; }
		}
		private String _ClienteCOSexoNombre;
		public String ClienteCOSexoNombre
		{
			get { return _ClienteCOSexoNombre; }
			set { _ClienteCOSexoNombre = value; }
		}
		private String _ClienteCOCelular1;
		public String ClienteCOCelular1
		{
			get { return _ClienteCOCelular1; }
			set { _ClienteCOCelular1 = value; }
		}
		private String _ClienteCOCelular2;
		public String ClienteCOCelular2
		{
			get { return _ClienteCOCelular2; }
			set { _ClienteCOCelular2 = value; }
		}
		private String _ClienteCOCorreo;
		public String ClienteCOCorreo
		{
			get { return _ClienteCOCorreo; }
			set { _ClienteCOCorreo = value; }
		}
		private String _EstadoNombre;
		public String EstadoNombre
		{
			get { return _EstadoNombre; }
			set { _EstadoNombre = value; }
		}
		private String _OrdenFormato;
		public String OrdenFormato
		{
			get { return _OrdenFormato; }
			set { _OrdenFormato = value; }
		}


		private Int32 _IDOrdenDetalle;
		public Int32 IDOrdenDetalle
		{
			get { return _IDOrdenDetalle; }
			set { _IDOrdenDetalle = value; }
		}

		private String _Examen;
		public String Examen
		{
			get { return _Examen; }
			set { _Examen = value; }
		}
		private String _Resultado;
		public String Resultado
		{
			get { return _Resultado; }
			set { _Resultado = value; }
		}
		private String _Tipo;
		public String Tipo
		{
			get { return _Tipo; }
			set { _Tipo = value; }
		}
		private Int32 _Orden;
		public Int32 Orden
		{
			get { return _Orden; }
			set { _Orden = value; }
		}
		private DateTime _FechaCreacion;
		public DateTime FechaCreacion
		{
			get { return _FechaCreacion; }
			set { _FechaCreacion = value; }
		}
		private String _ResultadoPredefinido;
		public String ResultadoPredefinido
		{
			get { return _ResultadoPredefinido; }
			set { _ResultadoPredefinido = value; }
		}

		private Int32 _IDGenerico;
		public Int32 IDGenerico
		{
			get { return _IDGenerico; }
			set { _IDGenerico = value; }
		}

		private String _Generico;
		public String Generico
		{
			get { return _Generico; }
			set { _Generico = value; }
		}

		private String _ValorReferencial;
		public String ValorReferencial
		{
			get { return _ValorReferencial; }
			set { _ValorReferencial = value; }
		}

		private Int32 _NumeroMuestra;
		public Int32 NumeroMuestra
		{
			get { return _NumeroMuestra; }
			set { _NumeroMuestra = value; }
		}
		private Int32 _ClienteEdad;
		public Int32 ClienteEdad
		{
			get { return _ClienteEdad; }
			set { _ClienteEdad = value; }
		}

		private String _Diagnostico;
		public String Diagnostico
		{
			get { return _Diagnostico; }
			set { _Diagnostico = value; }
		}

		private String _Recomendacion;
		public String Recomendacion
		{
			get { return _Recomendacion; }
			set { _Recomendacion = value; }
		}

	}
}