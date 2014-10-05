using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class Tubers: BaseClass
        {
            private string vegetableType = "Root vegetables (корнеплоды)";
            public string getVegetableType()
            {
                return vegetableType;
            }


        }
    class potatoes : Tubers
        {
            private string vegetableName = "potatoes (картофель отварной)";
            private string vegetableIso = "3216771";
            public double massa = 0;

            private const double proteins = 2.0;
            private const double fats = 0.4;
            private const double carbohydrates = 16.7;
            public string getName()
            {
                return vegetableName;
            }
            public string getIso()
            {
                return vegetableIso;
            }
            public double getProteins()
            {
                return getProteins(proteins, massa);
            }

            public double getFats()
            {
                return getFats(fats, massa);
            }
            public double getCarbohydrates()
            {
                return getCarbohydrates(carbohydrates, massa);
            }
            public double getCalories()
            {
                return getCalories(proteins, fats, carbohydrates);
            }
            public double getEnergeticValue()
            {
                return getEnergeticValue(massa, proteins, fats, carbohydrates);
            }
        }
       
        
}    
    

