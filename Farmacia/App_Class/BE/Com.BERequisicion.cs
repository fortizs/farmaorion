using System; 

namespace Farmacia.App_Class.BE.Compras
{
    public class BERequisicion : BEBase 
    {

        private Int32 _IDRequisicion;
        public Int32 IDRequisicion
        {
            get { return _IDRequisicion; }
            set { _IDRequisicion = value; }
        }

        private String _TipoRequisicion;
        public String TipoRequisicion
        {
            get { return _TipoRequisicion; }
            set { _TipoRequisicion = value; }
        }

        private String _NumeroRequisicionFormateado;
        public String NumeroRequisicionFormateado
        {
            get { return _NumeroRequisicionFormateado; }
            set { _NumeroRequisicionFormateado = value; }
        }

        private String _TipoRequisicionNombre;
        public String TipoRequisicionNombre
        {
            get { return _TipoRequisicionNombre; }
            set { _TipoRequisicionNombre = value; }
        }

        private Int32 _IDSolicitante;
        public Int32 IDSolicitante
        {
            get { return _IDSolicitante; }
            set { _IDSolicitante = value; }
        }

        private String _Solicitante;
        public String Solicitante
        {
            get { return _Solicitante; }
            set { _Solicitante = value; }
        }

        private Int32 _IDArea;
        public Int32 IDArea
        {
            get { return _IDArea; }
            set { _IDArea = value; }
        }

        private String _Area;
        public String Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        private DateTime _FechaRequisicion;
        public DateTime FechaRequisicion
        {
            get { return _FechaRequisicion; }
            set { _FechaRequisicion = value; }
        }

        private String _Glosa;
        public String Glosa
        {
            get { return _Glosa; }
            set { _Glosa = value; }
        }

        private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private String _EstadoNombre;
        public String EstadoNombre
        {
            get { return _EstadoNombre; }
            set { _EstadoNombre = value; }
        }

    }
}
