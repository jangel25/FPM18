using AppCocacolaNayMobiV2.Helpers;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.SQLite;
using AppCocacolaNayMobiV2.Models.Inventarios;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCocacolaNayMobiV2.Services.Inventarios
{
	public class FicSrvCatAlmacenList : IFicSrvCatAlmacen
    {
        private static readonly FicAsyncLock ficMutex = new FicAsyncLock();
        private SQLiteAsyncConnection ficSQLiteConnection;
        private SQLiteConnection ficSQLiteConnection2;

        public FicSrvCatAlmacenList()
        {
            var ficDataBasePath = DependencyService.Get<IFicConfigSQLiteNETStd>().FicGetDatabasePath();
            ficSQLiteConnection = new SQLiteAsyncConnection(ficDataBasePath);
            FicLoMetCreateDataBaseAsync();
        }

        public List<zt_cat_almacenes> GetAll_zt_cat_almacenes()
        {
            var ficDataBasePath = DependencyService.Get<IFicConfigSQLiteNETStd>().FicGetDatabasePath();
            ficSQLiteConnection2 = new SQLiteConnection(ficDataBasePath);
            var almacenes = ficSQLiteConnection2.Table<zt_cat_almacenes>().ToList();
            return almacenes;
        }

        public async void FicLoMetCreateDataBaseAsync()
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                await ficSQLiteConnection.CreateTableAsync<zt_cat_cedis>(CreateFlags.None).ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<zt_cat_almacenes>(CreateFlags.None).ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<zt_cat_unidad_medidas>(CreateFlags.None).ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<zt_cat_productos>(CreateFlags.None).ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<zt_inventarios>(CreateFlags.None).ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<zt_inventarios_det>(CreateFlags.None).ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<zt_inventarios_conteos>(CreateFlags.None).ConfigureAwait(false);
            }
        }

        #region zt_cat_almacenes
        public async Task<IList<zt_cat_almacenes>> FicMetGetListCatAlmacenes()
        {
            var Items = new List<zt_cat_almacenes>();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                Items = await ficSQLiteConnection.Table<zt_cat_almacenes>().ToListAsync().ConfigureAwait(false);
            }

            return Items;
        }

        public async Task FicMetInsertNewCatAlmacen(zt_cat_almacenes FicPaZt_cat_almacenes_Item)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var FicExistingAlmacenItem = await ficSQLiteConnection.Table<zt_cat_almacenes>()
                    .Where(x => x.Id == FicPaZt_cat_almacenes_Item.Id)
                    .FirstOrDefaultAsync();

                if (FicExistingAlmacenItem == null)
                {
                    await ficSQLiteConnection.InsertAsync(FicPaZt_cat_almacenes_Item).ConfigureAwait(false);
                }
                else
                {
                    FicPaZt_cat_almacenes_Item.Id = FicExistingAlmacenItem.Id;
                    await ficSQLiteConnection.UpdateAsync(FicPaZt_cat_almacenes_Item).ConfigureAwait(false);
                }
            }
        }

        public async Task FicMetRemoveCatAlmacen(zt_cat_almacenes FicPaZt_cat_almacenes_Item)
        {
            await ficSQLiteConnection.DeleteAsync(FicPaZt_cat_almacenes_Item);
        }

        #endregion
    }
}