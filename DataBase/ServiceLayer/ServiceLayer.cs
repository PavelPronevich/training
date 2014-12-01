using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public partial class ServiceLayer : ServiceBase
    {
        public ServiceLayer()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        AppLayer.DataCollectManager _dataManager;

        protected override void OnStart(string[] args)
        {
            try
            {
                eventLog1.WriteEntry(string.Format("In OnStart {0}.", DateTime.Now));
                _dataManager = new AppLayer.DataCollectManager();
                _dataManager.Init(@"e:\1\");
                _dataManager.Start();
            }
            catch (Exception e)
            {
                eventLog1.WriteEntry(string.Format("Exception {0}",e.Message));
            }
        }

        protected override void OnStop()
        {
            _dataManager.Dispose();
            eventLog1.WriteEntry(string.Format("In onStop {0}", DateTime.Now));
        }
    }
}
