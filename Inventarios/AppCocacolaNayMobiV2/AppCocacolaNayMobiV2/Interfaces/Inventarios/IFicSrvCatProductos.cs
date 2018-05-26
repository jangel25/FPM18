using System;
using System.Collections.Generic;
using AppCocacolaNayMobiV2.Models.Inventarios;
using System.Text;
using System.Threading.Tasks;

namespace AppCocacolaNayMobiV2.Interfaces.Productos
{
    public interface  IFicSrvCatProductos
    {

            Task<IList<zt_cat_productos>> FicMetGetListCatProductos();
            Task FicMetInsertNewCatProductos(zt_cat_productos FicPazt_cat_productos_Item);
            Task FicMetRemoveCatProductos(zt_cat_productos FicPaZt_cat_productos_Item);

     }
    
}
