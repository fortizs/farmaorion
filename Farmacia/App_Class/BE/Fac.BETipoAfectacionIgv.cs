using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.Facturacion
{
	public class BETipoAfectacionIgv : BEBase
	{
		private Int32 _TipoAfectacionIgv;
		public Int32 IDTipoAfectacionIgv
		{
			get { return _TipoAfectacionIgv; }
			set { _TipoAfectacionIgv = value; }
		}

		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}

		private String _CodigoSunat;
		public String CodigoSunat
		{
			get { return _CodigoSunat; }
			set { _CodigoSunat = value; }
		}
	}
}