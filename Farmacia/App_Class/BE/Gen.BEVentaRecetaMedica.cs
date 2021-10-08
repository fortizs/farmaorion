using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
	public class BEVentaRecetaMedica : BEBase
	{
		private Int32 _IDVentaRecetaMedica;
		public Int32 IDVentaRecetaMedica
		{
			get { return _IDVentaRecetaMedica; }
			set { _IDVentaRecetaMedica = value; }
		}

		private Int32 _IDVenta;
		public Int32 IDVenta
		{
			get { return _IDVenta; }
			set { _IDVenta = value; }
		}

		private String _FolioReceta;
		public String FolioReceta
		{
			get { return _FolioReceta; }
			set { _FolioReceta = value; }
		}

		private Boolean _RecetaRetenida;
		public Boolean RecetaRetenida
		{
			get { return _RecetaRetenida; }
			set { _RecetaRetenida = value; }
		}

		private String _NumeroDocumento;
		public String NumeroDocumento
		{
			get { return _NumeroDocumento; }
			set { _NumeroDocumento = value; }
		}

		private String _NombresCompleto;
		public String NombresCompleto
		{
			get { return _NombresCompleto; }
			set { _NombresCompleto = value; }
		}

		private String _Direccion;
		public String Direccion
		{
			get { return _Direccion; }
			set { _Direccion = value; }
		}

		private String _CMP;
		public String CMP
		{
			get { return _CMP; }
			set { _CMP = value; }
		}

		private Int32 _IDUsuario;
		public Int32 IDUsuario
		{
			get { return _IDUsuario; }
			set { _IDUsuario = value; }
		}
		 
	}
}