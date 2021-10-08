using System; 

namespace Farmacia.App_Class.BE.Restaurante
{
	public class BEMesa : BEBase
	{

		private Int32 _IDMesa;
		public Int32 IDMesa
		{
			get { return _IDMesa; }
			set { _IDMesa = value; }
		}

		private Int32 _Numero;
		public Int32 Numero
		{
			get { return _Numero; }
			set { _Numero = value; }
		}

		private Int32 _IDPedido;
		public Int32 IDPedido
		{
			get { return _IDPedido; }
			set { _IDPedido = value; }
		}

		private Int32 _IDEstado;
		public Int32 IDEstado
		{
			get { return _IDEstado; }
			set { _IDEstado = value; }
		}

		private String _Estado;
		public String Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}

		private String _EstadoCodigo;
		public String EstadoCodigo
		{
			get { return _EstadoCodigo; }
			set { _EstadoCodigo = value; }
		}

		private String _EstadoColor;
		public String EstadoColor
		{
			get { return _EstadoColor; }
			set { _EstadoColor = value; }
		}

	}
}
