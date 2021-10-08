using System;

namespace Farmacia.App_Class.BE.General
{
    public class BEColaborador : BEBase
    {

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
		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}
		private Int32 _IDCargo;
		public Int32 IDCargo
		{
			get { return _IDCargo; }
			set { _IDCargo = value; }
		}
		private String _Sexo;
		public String Sexo
		{
			get { return _Sexo; }
			set { _Sexo = value; }
		}
		private String _Dni;
		public String Dni
		{
			get { return _Dni; }
			set { _Dni = value; }
		}
		private String _Telefono;
		public String Telefono
		{
			get { return _Telefono; }
			set { _Telefono = value; }
		}

		private Int32 _IDEstadoCivil;
		public Int32 IDEstadoCivil
		{
			get { return _IDEstadoCivil; }
			set { _IDEstadoCivil = value; }
		}

		private DateTime _FechaNacimiento;
		public DateTime FechaNacimiento
		{
			get { return _FechaNacimiento; }
			set { _FechaNacimiento = value; }
		}
		 
		private String _Celular;
		public String Celular
		{
			get { return _Celular; }
			set { _Celular = value; }
		}
		private String _Email;
		public String Email
		{
			get { return _Email; }
			set { _Email = value; }
		}

		private String _Clave;
		public String Clave
		{
			get { return _Clave; }
			set { _Clave = value; }
		}

		
		private String _IDUbigeo;
		public String IDUbigeo
		{
			get { return _IDUbigeo; }
			set { _IDUbigeo = value; }
		}
		private String _Ubigeo;
		public String Ubigeo
		{
			get { return _Ubigeo; }
			set { _Ubigeo = value; }
		}
		private String _Direccion;
		public String Direccion
		{
			get { return _Direccion; }
			set { _Direccion = value; }
		}
		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}

		private String _RutaImagenFoto;
		public String RutaImagenFoto
		{
			get { return _RutaImagenFoto; }
			set { _RutaImagenFoto = value; }
		}
		private String _NombreImagenFoto;
		public String NombreImagenFoto
		{
			get { return _NombreImagenFoto; }
			set { _NombreImagenFoto = value; }
		}

		private String _RutaNombreImagenFotoCompleto;
		public String RutaNombreImagenFotoCompleto
		{
			get { return _RutaNombreImagenFotoCompleto; }
			set { _RutaNombreImagenFotoCompleto = value; }
		}
		
	}
}
