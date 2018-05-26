using AppCocacolaNayMobiV2.Helpers;
using AppCocacolaNayMobiV2.Interfaces.Productos;
using AppCocacolaNayMobiV2.Interfaces.SQLite;
using AppCocacolaNayMobiV2.Models.Inventarios;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppCocacolaNayMobiV2.Services.Productos
{
    public class FicSrvCatProductosList: IFicSrvCatProductos
    {
        private static readonly FicAsyncLock ficMutex = new FicAsyncLock();
        private SQLiteAsyncConnection ficSQLiteConnection;

        //FIC: Constructor
        public FicSrvCatProductosList()
        {
            var ficDataBasePath = DependencyService.Get<IFicConfigSQLiteNETStd>().FicGetDatabasePath();
            //var ficDataBasePath = "Data Source" = " + Server.MapPath(~/data/dbSQLite/";
            //SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + Server.MapPath("~/db/test_database2.db") + ";Version=3;New=True;Compress=True;");
            ficSQLiteConnection = new SQLiteAsyncConnection(ficDataBasePath);
            //FIC: Se manda llamar funcion local para verificar
            //si no existen las tablas crearlas de forma local en el dispositivo
            FicLoMetCreateDataBaseAsync();
        }

        //FIC: Metodo para crear las tablas si no existen localmente en el dispositivo
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

        #region zt_cat_productos

        public async Task<IList<zt_cat_productos>> FicMetGetListCatProductos()
        {
            var items = new List<zt_cat_productos>();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                items = await ficSQLiteConnection.Table<zt_cat_productos>().ToListAsync().ConfigureAwait(false);
            }

            return items;
        }

        public async Task FicMetInsertNewCatProductos(zt_cat_productos FicPazt_cat_productos_Item)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var FicExistingInventarioItem = await ficSQLiteConnection.Table<zt_cat_productos>()
                        .Where(x => x.Id == FicPazt_cat_productos_Item.Id)
                        .FirstOrDefaultAsync();

                if (FicExistingInventarioItem == null)
                {
                    await ficSQLiteConnection.InsertAsync(FicPazt_cat_productos_Item).ConfigureAwait(false);
                }
                else
                {
                    FicPazt_cat_productos_Item.Id = FicExistingInventarioItem.Id;
                    await ficSQLiteConnection.UpdateAsync(FicPazt_cat_productos_Item).ConfigureAwait(false);
                }
            }
        }

        public async Task FicMetRemoveCatProductos(zt_cat_productos FicPaZt_cat_productos_Item)
        {
            await ficSQLiteConnection.DeleteAsync(FicPaZt_cat_productos_Item);
        }



        #endregion
    }
}
