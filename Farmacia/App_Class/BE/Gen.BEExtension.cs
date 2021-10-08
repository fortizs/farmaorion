using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
	public class BEExtension : BEBase
	{
		private Int32 _IDExtension;

		public Int32 IDExtension
		{
			get { return _IDExtension; }
			set { _IDExtension = value; }
		}

		private String _Extension;

		public String Extension
		{
			get { return _Extension; }
			set { _Extension = value; }
		}
	}
}