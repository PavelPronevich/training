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

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry(string.Format("In OnStart {0}.", DateTime.Now));
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry(string.Format("In onStop {0}", DateTime.Now));
        }
    }
}
