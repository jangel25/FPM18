using System.IO;
using AppCocacolaNayMobiV2.Interfaces.SQLite;
using AppCocacolaNayMobiV2.UWP.Services.SQLite;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FicConfigSQLiteUWP))]
namespace AppCocacolaNayMobiV2.UWP.Services.SQLite
{
    class FicConfigSQLiteUWP : IFicConfigSQLiteNETStd
    {
        public string FicGetDatabasePath()
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, AppSettings.ficDatabaseName);
        }
    }
}
