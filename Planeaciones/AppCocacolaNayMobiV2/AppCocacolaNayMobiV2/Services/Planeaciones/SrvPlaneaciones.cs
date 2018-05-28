using AppCocacolaNayMobiV2.Helpers;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using AppCocacolaNayMobiV2.Interfaces.SQLite;
using AppCocacolaNayMobiV2.Models.Planeaciones;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCocacolaNayMobiV2.Services.Planeaciones
{
    public class SrvPlaneaciones : ISrvPlaneacion
    {
        private static readonly FicAsyncLock ficMutex = new FicAsyncLock();
        private SQLiteAsyncConnection ficSQLiteConnection;

        public SrvPlaneaciones()
        {
            var ficDataBasePath = DependencyService.Get<IFicConfigSQLiteNETStd>().FicGetDatabasePath();
            ficSQLiteConnection = new SQLiteAsyncConnection(ficDataBasePath);
            FicLoMetCreateDataBaseAsync();
        }//Fin constructor

        public async void FicLoMetCreateDataBaseAsync()
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                await ficSQLiteConnection.CreateTableAsync<Eva_planeacion>().ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<Eva_planeacion_temas>().ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<Eva_planeacion_subtemas>().ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<Eva_planeacion_fuentes>().ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<Eva_cat_fuentes_bibliograficas>().ConfigureAwait(false);
                await ficSQLiteConnection.CreateTableAsync<Eva_cat_apoyos_didacticos>().ConfigureAwait(false);
            }
        }//Fin createDataBaseAsync

        public async Task<IList<Eva_planeacion>> GetAll_eva_planeacion()
        {
            var eva_planeacion = new List<Eva_planeacion>();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                eva_planeacion = await ficSQLiteConnection.Table<Eva_planeacion>().ToListAsync().ConfigureAwait(false);
            }

            return eva_planeacion;
        }//Fin GetAll

        public async Task Insert_eva_planeacion(Eva_planeacion eva_planeacion)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var existingCountItem = await ficSQLiteConnection.Table<Eva_planeacion>()
                        .Where(x => x.IdPlaneacion == eva_planeacion.IdPlaneacion)
                        .FirstOrDefaultAsync();

                if (existingCountItem == null)
                {
                    await ficSQLiteConnection.InsertAsync(eva_planeacion).ConfigureAwait(false);
                }
                else
                {
                    eva_planeacion.IdPlaneacion = existingCountItem.IdPlaneacion;
                    await ficSQLiteConnection.UpdateAsync(eva_planeacion).ConfigureAwait(false);
                }
            }
        }//Fin insert

        public async Task Remove_eva_planeacion(Eva_planeacion eva_planeacion)
        {
            await ficSQLiteConnection.DeleteAsync(eva_planeacion);
        }//Fin remove

        //Esto es para Eva_planeacion_temas
        public async Task<IList<Eva_planeacion_temas>> GetAll_eva_planeacion_temas()
        {
            var eva_planeacion_temas = new List<Eva_planeacion_temas>();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                eva_planeacion_temas = await ficSQLiteConnection.Table<Eva_planeacion_temas>().ToListAsync().ConfigureAwait(false);
            }

            return eva_planeacion_temas;
        }//Fin GetAll

        public async Task Insert_eva_planeacion_temas(Eva_planeacion_temas eva_planeacion_temas)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var existingCountItem = await ficSQLiteConnection.Table<Eva_planeacion_temas>()
                        .Where(x => x.IdTema == eva_planeacion_temas.IdTema)
                        .FirstOrDefaultAsync();

                if (existingCountItem == null)
                {
                    await ficSQLiteConnection.InsertAsync(eva_planeacion_temas).ConfigureAwait(false);
                }
                else
                {
                    eva_planeacion_temas.IdTema = existingCountItem.IdTema;
                    await ficSQLiteConnection.UpdateAsync(eva_planeacion_temas).ConfigureAwait(false);
                }
            }
        }//Fin insert

        public async Task Remove_eva_planeacion_temas(Eva_planeacion_temas eva_planeacion_temas)
        {
            await ficSQLiteConnection.DeleteAsync(eva_planeacion_temas);
        }//Fin remove

        //Esto es para Eva_planeacion_subtemas
        public async Task<IList<Eva_planeacion_subtemas>> GetAll_eva_planeacion_subtemas()
        {
            var eva_planeacion_subtemas = new List<Eva_planeacion_subtemas>();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                eva_planeacion_subtemas = await ficSQLiteConnection.Table<Eva_planeacion_subtemas>().ToListAsync().ConfigureAwait(false);
            }

            return eva_planeacion_subtemas;
        }//Fin GetAll

        public async Task Insert_eva_planeacion_subtemas(Eva_planeacion_subtemas eva_planeacion_subtemas)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var existingCountItem = await ficSQLiteConnection.Table<Eva_planeacion_subtemas>()
                        .Where(x => x.IdSubtema == eva_planeacion_subtemas.IdSubtema)
                        .FirstOrDefaultAsync();

                if (existingCountItem == null)
                {
                    await ficSQLiteConnection.InsertAsync(eva_planeacion_subtemas).ConfigureAwait(false);
                }
                else
                {
                    eva_planeacion_subtemas.IdSubtema = existingCountItem.IdSubtema;
                    await ficSQLiteConnection.UpdateAsync(eva_planeacion_subtemas).ConfigureAwait(false);
                }
            }
        }//Fin insert

        public async Task Remove_eva_planeacion_subtemas(Eva_planeacion_subtemas eva_planeacion_subtemas)
        {
            await ficSQLiteConnection.DeleteAsync(eva_planeacion_subtemas);
        }//Fin remove

        //Esto es para Eva_planeacion_fuentes
        public async Task Insert_eva_planeacion_fuentes(Eva_planeacion_fuentes eva_planeacion_fuentes)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var existingCountItem = await ficSQLiteConnection.Table<Eva_planeacion_fuentes>()
                        .Where(x => x.IdFuente == eva_planeacion_fuentes.IdFuente)
                        .FirstOrDefaultAsync();

                if (existingCountItem == null)
                {
                    await ficSQLiteConnection.InsertAsync(eva_planeacion_fuentes).ConfigureAwait(false);
                }
                else
                {
                    eva_planeacion_fuentes.IdFuente = existingCountItem.IdFuente;
                    await ficSQLiteConnection.UpdateAsync(eva_planeacion_fuentes).ConfigureAwait(false);
                }
            }
        }//Fin insert

        public async Task Remove_eva_planeacion_fuentes(Eva_planeacion_fuentes eva_planeacion_fuentes)
        {
            await ficSQLiteConnection.DeleteAsync(eva_planeacion_fuentes);
        }//Fin remove

        //Esto es para Eva_planeacion_fuentes_bibliograficas
        public async Task<IList<Eva_cat_fuentes_bibliograficas>> GetAll_eva_cat_fuentes_bibliograficas()
        {
            var eva_cat_fuentes_bibliograficas = new List<Eva_cat_fuentes_bibliograficas>();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                eva_cat_fuentes_bibliograficas = await ficSQLiteConnection.Table<Eva_cat_fuentes_bibliograficas>().ToListAsync().ConfigureAwait(false);
            }

            return eva_cat_fuentes_bibliograficas;
        }//Fin GetAll

        public async Task Insert_eva_cat_fuentes_bibliograficas(Eva_cat_fuentes_bibliograficas eva_cat_fuentes_bibliograficas)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var existingCountItem = await ficSQLiteConnection.Table<Eva_cat_fuentes_bibliograficas>()
                        .Where(x => x.IdFuente == eva_cat_fuentes_bibliograficas.IdFuente)
                        .FirstOrDefaultAsync();

                if (existingCountItem == null)
                {
                    await ficSQLiteConnection.InsertAsync(eva_cat_fuentes_bibliograficas).ConfigureAwait(false);
                }
                else
                {
                    eva_cat_fuentes_bibliograficas.IdFuente = existingCountItem.IdFuente;
                    await ficSQLiteConnection.UpdateAsync(eva_cat_fuentes_bibliograficas).ConfigureAwait(false);
                }
            }
        }//Fin insert

        public async Task Remove_eva_cat_fuentes_bibliograficas(Eva_cat_fuentes_bibliograficas eva_cat_fuentes_bibliograficas)
        {
            await ficSQLiteConnection.DeleteAsync(eva_cat_fuentes_bibliograficas);
        }//Fin remove

        //Esto es para Eva_cat_apoyos_didacticos
        public async Task<IList<Eva_cat_apoyos_didacticos>> GetAll_eva_cat_apoyos_didacticos()
        {
            var eva_cat_fuentes_bibliograficas = new List<Eva_cat_apoyos_didacticos>();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                eva_cat_fuentes_bibliograficas = await ficSQLiteConnection.Table<Eva_cat_apoyos_didacticos>().ToListAsync().ConfigureAwait(false);
            }

            return eva_cat_fuentes_bibliograficas;
        }//Fin GetAll

        public async Task Insert_eva_cat_apoyos_didacticos(Eva_cat_apoyos_didacticos eva_cat_apoyos_didacticos)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var existingCountItem = await ficSQLiteConnection.Table<Eva_cat_apoyos_didacticos>()
                        .Where(x => x.IdApoyoDidactico == eva_cat_apoyos_didacticos.IdApoyoDidactico)
                        .FirstOrDefaultAsync();

                if (existingCountItem == null)
                {
                    await ficSQLiteConnection.InsertAsync(eva_cat_apoyos_didacticos).ConfigureAwait(false);
                }
                else
                {
                    eva_cat_apoyos_didacticos.IdApoyoDidactico = existingCountItem.IdApoyoDidactico;
                    await ficSQLiteConnection.UpdateAsync(eva_cat_apoyos_didacticos).ConfigureAwait(false);
                }
            }
        }//Fin insert

        public async Task Remove_eva_cat_apoyos_didacticos(Eva_cat_apoyos_didacticos eva_cat_apoyos_didacticos)
        {
            await ficSQLiteConnection.DeleteAsync(eva_cat_apoyos_didacticos);
        }//Fin remove
    }
}//Fin clase
