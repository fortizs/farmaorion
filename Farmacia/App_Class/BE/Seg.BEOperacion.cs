using System;

namespace Farmacia.App_Class.BE.Seguridad
{
    public class BEOperacion : BEBase
    {
        private Boolean _ModificarEmision = false; // 1
        public Boolean ModificarEmision
        {
            get { return _ModificarEmision; }
            set { _ModificarEmision = value; }
        }

        private Boolean _AnularEmision = false; // 2
        public Boolean AnularEmision
        {
            get { return _AnularEmision; }
            set { _AnularEmision = value; }
        }

        private Boolean _SubirSustentos = false; // 3
        public Boolean SubirSustentos
        {
            get { return _SubirSustentos; }
            set { _SubirSustentos = value; }
        }

        private Boolean _AplicarCobro = false; // 4
        public Boolean AplicarCobro
        {
            get { return _AplicarCobro; }
            set { _AplicarCobro = value; }
        }

        private Boolean _VehiculoAsegurado = false; // 5
        public Boolean VehiculoAsegurado
        {
            get { return _VehiculoAsegurado; }
            set { _VehiculoAsegurado = value; }
        }

        private Boolean _ImprimirEmision = false; // 6
        public Boolean ImprimirEmision
        {
            get { return _ImprimirEmision; }
            set { _ImprimirEmision = value; }
        }

        //private Boolean _ListarUsuarioRenovacion = false; // 7
        //public Boolean ListarUsuarioRenovacion
        //{
        //    get { return _ListarUsuarioRenovacion; }
        //    set { _ListarUsuarioRenovacion = value; }
        //}

        private Boolean _ExportarReporte = false; // 8
        public Boolean ExportarReporte
        {
            get { return _ExportarReporte; }
            set { _ExportarReporte = value; }
        }

        //private Boolean _AtencionClienteTipificacion = false; // 9
        //public Boolean AtencionClienteTipificacion
        //{
        //    get { return _AtencionClienteTipificacion; }
        //    set { _AtencionClienteTipificacion = value; }
        //}

        //private Boolean _RegistrarAtencion = false; // 10
        //public Boolean RegistrarAtencion
        //{
        //    get { return _RegistrarAtencion; }
        //    set { _RegistrarAtencion = value; }
        //}

        //private Boolean _ResponsableAtencion=false; //
        //public Boolean ResponsableAtencion
        //{
        //    get { return _ResponsableAtencion; }
        //    set { _ResponsableAtencion = value; }
        //}
        private Boolean _VerSustento = false; //11
        public Boolean VerSustento
        {
            get { return _VerSustento; }
            set { _VerSustento = value; }
        }
        private Boolean _BloquearCobro = false; //12
        public Boolean BloquearCobro
        {
            get { return _BloquearCobro; }
            set { _BloquearCobro = value; }
        }

        private Boolean _ModificarRecibo = false; //
        public Boolean ModificarRecibo
        {
            get { return _ModificarRecibo; }
            set { _ModificarRecibo = value; }
        }

        private Boolean _CambiarEstadoEmision = false; //15
        public Boolean CambiarEstadoEmision
        {
            get { return _CambiarEstadoEmision; }
            set { _CambiarEstadoEmision = value; }
        }
    }
}