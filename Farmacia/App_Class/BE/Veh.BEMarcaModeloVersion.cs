using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Vehicular
{
	public class BEMarcaModeloVersion : BEBase
	{
		private Int32 _IDMarca;
		public Int32 IDMarca
		{
			get { return _IDMarca; }
			set { _IDMarca = value; }
		}

		private String _Marca;
		public String Marca
		{
			get { return _Marca; }
			set { _Marca = value; }
		}

		private Int32 _IDModelo;
		public Int32 IDModelo
		{
			get { return _IDModelo; }
			set { _IDModelo = value; }
		}

		private String _Modelo;
		public String Modelo
		{
			get { return _Modelo; }
			set { _Modelo = value; }
		}

		private Int32 _IDModeloVersion;
		public Int32 IDModeloVersion
		{
			get { return _IDModeloVersion; }
			set { _IDModeloVersion = value; }
		}

		private String _ModeloVersion;
		public String ModeloVersion
		{
			get { return _ModeloVersion; }
			set { _ModeloVersion = value; }
		}

		private String _Origen;
		public String Origen
		{
			get { return _Origen; }
			set { _Origen = value; }
		}

		private Int32 _IDTipoVehiculo;
		public Int32 IDTipoVehiculo
		{
			get { return _IDTipoVehiculo; }
			set { _IDTipoVehiculo = value; }
		}

		private String _TipoVehiculo;
		public String TipoVehiculo
		{
			get { return _TipoVehiculo; }
			set { _TipoVehiculo = value; }
		}

		private Int32 _NumeroAsientos;
		public Int32 NumeroAsientos
		{
			get { return _NumeroAsientos; }
			set { _NumeroAsientos = value; }
		}

		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}
	}
}