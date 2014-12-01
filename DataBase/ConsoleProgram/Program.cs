using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateDataBase;
using AppLayer;



namespace ConsoleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            AppLayer.DataCollectManager _dataManager;
            _dataManager = new AppLayer.DataCollectManager();
            _dataManager.Init(@"e:\1\");
            _dataManager.Start();
            Console.Read();
            _dataManager.Dispose();

        }
    }
}
