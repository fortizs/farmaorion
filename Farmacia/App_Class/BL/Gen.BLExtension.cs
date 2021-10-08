using Farmacia.App_Class.BE.General;
using System;

namespace Farmacia.App_Class.BL.General
{
	public class BLExtension : BLBase
	{
		public BEExtension Extension(string pCodigo)
		{
			BEExtension BEExtension = new BEExtension();
			BEExtension.Extension = Convert.ToString(CadenaConexionE(pCodigo));
			return BEExtension;

		}
	}
}