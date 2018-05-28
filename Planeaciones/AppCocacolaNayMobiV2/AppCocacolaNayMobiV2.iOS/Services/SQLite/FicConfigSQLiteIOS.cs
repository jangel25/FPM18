using System;
using System.Collections.Generic;
using System;
using System.IO;
using AppCocacolaNayMobiV2.iOS.Services.SQLite;
using AppCocacolaNayMobiV2.Interfaces.SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(FicConfigSQLiteIOS))]
namespace AppCocacolaNayMobiV2.iOS.Services.SQLite
{
    class FicConfigSQLiteIOS : IFicConfigSQLiteNETStd
    {
        public string FicGetDatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, AppSettings.ficDatabaseName);
        }
    }
}