using CreateDataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            CatalogWatcher.Created += new FileSystemEventHandler(OnCreated);

            IEnumerable<string> files = Directory.GetFiles(Catalog, "*.csv");
            //Parallel.ForEach(files, item => OrdersContext.AddOrdersToDBFromFile(item, @"E:\1\DB.log"));
            //Parallel.ForEach(files, item => OrdersContext.AddOrdersToDBFromFile(item));
        }
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            OrdersContext.AddOrdersToDBFromFile(e.FullPath);
        }
        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            OrdersContext.RemoveDataRfomDB(e.FullPath, @"E:\1\DB.log");
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
