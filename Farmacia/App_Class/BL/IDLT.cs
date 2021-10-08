
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
    interface IDLT
    {
        BERetornoTran Insertar(BEBase pEntidad);
        BERetornoTran Actualizar(BEBase pEntidad);
        BERetornoTran Eliminar(BEBase pEntidad);
        SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand pcmd, System.String pTipoTransaccion);
    }
}
