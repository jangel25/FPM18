using System;
using System.Collections.Generic;
using AppCocacolaNayMobiV2.Models.Inventarios;
using System.Text;
using System.Threading.Tasks;

namespace AppCocacolaNayMobiV2.Interfaces.Inventarios
{
	public interface IFicSrvCatAlmacen
	{
        Task<IList<zt_cat_almacenes>> FicMetGetListCatAlmacenes();
        Task FicMetInsertNewCatAlmacen(zt_cat_almacenes FicPaZt_cat_almacen_Item);
        Task FicMetRemoveCatAlmacen(zt_cat_almacenes FicPaZt_cat_almacen_Remove);
	}
}