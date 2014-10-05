using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class CruciferousVegetables:  BaseClass
        {
            private string vegetableType = "CruciferousVegetables (капустные)";
            public string getVegetableType()
            {
                return vegetableType;
            }


        }
        class cabbage : CruciferousVegetables
        {
            private string vegetableName = "cabbage (капуста белокочанная)";
            private string vegetableIso = "3211576271";
            public double massa = 0;

            private const double proteins = 1.8;
            private const double fats = 0.1;
            private const double carbohydrates = 4.7;
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


        class sauerkraut : CruciferousVegetables
        {
            private string vegetableName = "sauerkraut (капуста квашенная)";
            private string vegetableIso = "32789021061";
            public double massa = 0;

            private const double proteins = 1.8;
            private const double fats = 0;
            private const double carbohydrates = 2.2;
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

        class sauerkrautWithSugar : CruciferousVegetables
        {
            private string vegetableName = "sauerkraut With Sugar (капуста квашенная с сахаром)";
            private string vegetableIso = "327389851";
            public double massa = 0;

            private const double proteins = 1.6;
            private const double fats = 0;
            private const double carbohydrates = 9.4;
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

        class сhineseСabbage : CruciferousVegetables
        {
            private string vegetableName = "Chinese cabbage (пекинская капуста)";
            private string vegetableIso = "2456342951";
            public double massa = 0;

            private const double proteins = 1.2;
            private const double fats = 0;
            private const double carbohydrates = 2.2;
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
