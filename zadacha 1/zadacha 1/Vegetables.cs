using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{

    class Carrot : FoodStuff
    {
        public Carrot(double massa)
            : base(VegetableType.Roots, "Carrot", "3216771", 1.2, 0.1, 7.2, massa)
        {
        }
    }

    class Radish : FoodStuff
    {
        public Radish(double massa)
            : base(VegetableType.Roots, "Radish", "32217315", 1.2, 0.1, 3.8, massa)
        {
        }
    }

    class Beets : FoodStuff
    {
        public Beets(double massa)
            : base(VegetableType.Roots, "Beets", "33279841", 1.8, 0, 10.8, massa)
        {
        }
    }

    class Potatoes : FoodStuff
    {
        public Potatoes(double massa)
            : base(VegetableType.Tubers, "Potatoes", "326412841", 2.0, 0.4, 16.7, massa)
        {
        }
    }

    class Cabbage : FoodStuff
    {
        public Cabbage(double massa)
            : base(VegetableType.Cruciferous, "Cabbage", "32621735", 1.8, 0.1, 4.7, massa)
        {
        }
    }


    class ChineseCabbage : FoodStuff
    {
        public ChineseCabbage(double massa)
            : base(VegetableType.Cruciferous, "ChineseCabbage", "3107373", 1.2, 0.2, 2.2, massa)
        {
        }
    }


    class Sauerkraut : FoodStuff
    {
        public Sauerkraut(double massa)
            : base(VegetableType.Cruciferous, "Sauerkraut", "32290472", 1.8, 0, 2.2, massa)
        {
        }
    }

    class Tomatoes : FoodStuff
    {
        public Tomatoes(double massa)
            : base(VegetableType.Tomatos, "Tomatoes", "122934523", 1.1, 0.2, 3.8, massa)
        {
        }
    }

    class Onions : FoodStuff
    {
        public Onions(double massa)
            : base(VegetableType.Onions, "Onions", "1239617563", 1.4, 0, 9.1, massa)
        {
        }
    }

    class Lettuce : FoodStuff
    {
        public Lettuce(double massa)
            : base(VegetableType.Salads, "Lettuce", "1239617563", 0.5, 0.2, 2.3, massa)
        {
        }
    }

}
