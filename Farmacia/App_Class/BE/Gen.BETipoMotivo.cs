using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
	public class BETipoMotivo : BEBase
	{

		private Int32 _IDTipoMotivo;
		public Int32 IDTipoMotivo
		{
			get { return _IDTipoMotivo; }
			set { _IDTipoMotivo = value; }
		}
		private Int32 _IDTipoComprobante;
		public Int32 IDTipoComprobante
		{
			get { return _IDTipoComprobante; }
			set { _IDTipoComprobante = value; }
		}
		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}
		private String _CodigoSunat;
		public String CodigoSunat
		{
			get { return _CodigoSunat; }
			set { _CodigoSunat = value; }
		}

	}
}