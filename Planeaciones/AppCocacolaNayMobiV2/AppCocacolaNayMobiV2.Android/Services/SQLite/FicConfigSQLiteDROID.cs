using System.IO;
using Xamarin.Forms;
using AppCocacolaNayMobiV2.Interfaces.SQLite;
using AppCocacolaNayMobiV2.Droid.Services.SQLite;

[assembly: Dependency(typeof(ficConfigSQLiteDROID))]
namespace AppCocacolaNayMobiV2.Droid.Services.SQLite
{
    public class ficConfigSQLiteDROID : IFicConfigSQLiteNETStd

    {
        public string FicGetDatabasePath()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, AppSettings.ficDatabaseName);
        }
    }
}