using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    class Port
    {
        private bool _isSwitched=true; // вкл.откл станция за неуплату
        private bool _isBusy=false;    // занет не занет опред станция если идет входящий или пользователь если исходящий звонки
        public int Number{get;private set;}// номер порта (телефона)
        private int _namber = 754318;
        
        //private int pinCode; // секр номер для телефона, чтобы он прослушивал Port
        public Port() 
        {
            this.Number = _namber++;
        }
        public bool Isswitched 
        {
            get 
            { 
                return _isSwitched; 
            }
            private set
            { 
                _isSwitched=value;
            }
            
        }
        
        public static void Block(object sender, BlockPortEventArgs e)
        {
         e.port.Isswitched = e.IsSwitched;
        }
        
        

    }
    
}
