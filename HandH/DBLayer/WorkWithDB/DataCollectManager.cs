using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
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
            Console.WriteLine("On Start");
            CatalogWatcher = new System.IO.FileSystemWatcher(Catalog, "*.csv");
            CatalogWatcher.Deleted += new FileSystemEventHandler(OnDeleted);
            CatalogWatcher.Renamed += new RenamedEventHandler(OnRenamed);
            CatalogWatcher.Created += new FileSystemEventHandler(OnCreated);
            CatalogWatcher.EnableRaisingEvents = true;
            IEnumerable<string> files = Directory.GetFiles(Catalog, "*.csv");
            Parallel.ForEach(files, item => BDChange.AddOrdersToDBFromFile(item));
        }
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            BDChange.AddOrdersToDBFromFile(e.FullPath);
        }
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            BDChange.RemoveDataRfomDB(e.FullPath);
        }
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            BDChange.RemoveDataRfomDB(e.OldFullPath);
            BDChange.AddOrdersToDBFromFile(e.FullPath);
        }

        public void Stop()
        {
            CatalogWatcher.Deleted -= new FileSystemEventHandler(OnDeleted);
            CatalogWatcher.Renamed -= new RenamedEventHandler(OnRenamed);
            CatalogWatcher.Created -= new FileSystemEventHandler(OnCreated);
            CatalogWatcher.EnableRaisingEvents = false;
        }

        public void Dispose()
        {
            Stop();
            GC.SuppressFinalize(this);    
        }
       
    }
}
