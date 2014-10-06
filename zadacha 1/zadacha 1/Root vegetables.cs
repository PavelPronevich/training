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

    class Potatoes : BaseClass
    {
        public Potatoes(double massa)
            : base("Tubers (корнеплоды)", "Potatoes (картофель лтварной)", "326412841", 2.0, 0.4, 16.7, massa)
        {
        }
    }

    class Cabbage : BaseClass
    {
        public Cabbage(double massa)
            : base("Cruciferous vegetables (капустные)", "Cabbage (капуста белокачанная)", "32621735", 1.8, 0.1, 4.7, massa)
        {
        }
    }

    
    class ChineseCabbage : BaseClass
    {
        public ChineseCabbage(double massa)
            : base("Cruciferous vegetables (капустные)", "ChineseCabbage (капуста пекинская)", "3107373", 1.2, 0.2, 2.2, massa)
        {
        }
    }

    
    class Sauerkraut : BaseClass
    {
        public Sauerkraut(double massa)
            : base("Cruciferous vegetables (капустные)", "Sauerkraut (капуста квашенная)", "32290472", 1.8, 0, 2.2, massa)
        {
        }
    }

    class Tomatoes : BaseClass
    {
        public Tomatoes(double massa)
            : base("Tomato vegetables (томатные)", "Tomatoes (помидоры)", "122934523", 1.1, 0.2, 3.8, massa)
        {
        }
    }

    class Onions : BaseClass
    {
        public Onions(double massa)
            : base("Tomato vegetables (томатные)", "Onions (лук репчатый)", "1239617563", 1.4, 0, 9.1, massa)
        {
        }
    }

    class Lettuce : BaseClass
    {
        public Lettuce(double massa)
            : base("Salad vegetables  (салатные)", "Lettuce (салат)", "1239617563", 0.5, 0.2, 2.3, massa)
        {
        }
    }

}
