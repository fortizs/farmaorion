using System;

namespace Farmacia.App_Class.BE.Proceso
{
	public class BEEstructuraProceso : BEBase
	{
		private Int32 _IDEstructuraProceso;
		public Int32 IDEstructuraProceso
		{
			get { return _IDEstructuraProceso; }
			set { _IDEstructuraProceso = value; }
		}

		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}

		private String _Procedimiento;
		public String Procedimiento
		{
			get { return _Procedimiento; }
			set { _Procedimiento = value; }
		}

		private String _Descripcion;
		public String Descripcion
		{
			get { return _Descripcion; }
			set { _Descripcion = value; }
		}

		private Boolean _Cargar;
		public Boolean Cargar
		{
			get { return _Cargar; }
			set { _Cargar = value; }
		}


		

		private String _Extension;
		public String Extension
		{
			get { return _Extension; }
			set { _Extension = value; }
		}

		private String _NombreHoja;
		public String NombreHoja
		{
			get { return _NombreHoja; }
			set { _NombreHoja = value; }
		}

		

	}
}