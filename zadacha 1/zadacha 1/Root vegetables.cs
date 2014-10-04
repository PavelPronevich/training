using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha_1
{
    class Root_vegetables: Ivagetable
    {
        public string vegetableType="Root vegetables";
        public string vegetableName;
        private string vegetableIso;
        private int colorii;
        public int getColorii()
        {
            return colorii;
        }
        public string getIso()
        {
            return vegetableIso;
        }
        public string getName()
        {
            return vegetableName;
        }

        


    }
}
