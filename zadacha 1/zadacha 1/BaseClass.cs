using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class BaseClass  //: Ivagetable
    {
        protected double getCalories(double proteins, double fats, double carbohydrates)
        {
            return 4 * proteins + 9 * fats + 4 * carbohydrates;
        }
        protected double getProteins(double proteins, double massa)
        {
            return proteins * massa / 100;
        }
        protected double getFats(double fats, double massa)
        {
            return fats * massa / 100;
        }
        protected double getCarbohydrates(double carbohydrates, double massa)
        {
            return carbohydrates * massa / 100;
        }
        protected double getEnergeticValue(double massa, double proteins, double fats, double carbohydrates)
        {
            return massa * getCalories(proteins, fats, carbohydrates) / 100;
        }  
    }
}
