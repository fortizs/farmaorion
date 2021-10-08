
using Farmacia.App_Class.BE;
using System.Collections;

namespace Farmacia.App_Class.BL
{
    interface IDLNT
    {
        IList Listar();
        IList Listar(BEBase pEntidad);
        IList Listar(System.Int32 pCodigo);
        IList Listar(System.String pCodigo);
        BEBase Seleccionar(System.Int32 pCodigo);
        BEBase Seleccionar(System.String pCodigo);
        BEBase Seleccionar(BEBase pEntidad);
    }
}
