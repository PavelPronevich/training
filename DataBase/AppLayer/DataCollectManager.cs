using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer
{
    public class DataCollectManager:IDisposable
    {
        public string Catalog { get; set; }
        private System.IO.FileSystemWatcher CatalogWatcher;

        public void Init (string catalogName)
        {
            if (catalogName!=null)
            {
                this.Catalog = catalogName;
            }
            else
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                Catalog = settings["CatalogName"].Value;
            }
        }
        public void Start()
        {
            CatalogWatcher = new System.IO.FileSystemWatcher(Catalog, "*.csv");
            CatalogWatcher.Deleted += null;
            CatalogWatcher.Renamed += null;
            CatalogWatcher.Created += null;

            IEnumerable<string> files = System.IO.Directory.GetFiles(Catalog, "*.csv");
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
