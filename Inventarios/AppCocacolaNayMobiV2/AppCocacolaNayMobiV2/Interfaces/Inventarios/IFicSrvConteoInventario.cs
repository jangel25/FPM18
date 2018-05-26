using System;
using System.Collections.Generic;
using AppCocacolaNayMobiV2.Models.Inventarios;
using System.Text;
using System.Threading.Tasks;

namespace AppCocacolaNayMobiV2.Interfaces.Inventarios
{
    public interface IFicSrvConteoInventario
    {
        Task<IList<zt_inventarios>> FicMetGetListInventarios();
        Task FicMetInsertNewInventario(zt_inventarios FicPaZt_inventarios_Item);
        Task FicMetRemoveInventario(zt_inventarios FicPaZt_inventarios_Remove);

        Task<IList<zt_cat_cedis>> FicMetGetListCEDIS();
        Task<zt_cat_cedis> FicMetGetCEDIS(zt_inventarios FicPaZt_inventarios_Item);

        Task<IList<zt_inventarios_det>> FicMetGetListInventariosDet();
        Task<IList<zt_inventarios_det>> FicMetGetListInventariosDet(zt_inventarios FicPaZt_inventarios);
        Task FicMetInsertNewInventarioDet(zt_inventarios_det FicPaZt_inventarios_det_Item);
        Task FicMetRemoveInventarioDet(zt_inventarios_det FicPaZt_inventarios_det_Remove);

    }
}
