using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{

    class Carrot : BaseClass
    {
        public Carrot(double massa)
            : base("Root vegetables (корнеплоды)", "Carrot (морковь)", "3216771", 1.2, 0.1, 7.2, massa)
        {
        }
    }

    class Radish : BaseClass
    {
        public Radish(double massa)
            : base("Root vegetables (корнеплоды)", "Radish (редис)", "32217315", 1.2, 0.1, 3.8, massa)
        {
        }
    }

    class Beets : BaseClass
    {
        public Beets(double massa)
            : base("Root vegetables (корнеплоды)", "Beets (свекла отварная)", "33279841", 1.8, 0, 10.8, massa)
        {
        }
    }
    
}
