using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class Tomato: BaseClass   
    {
        private string vegetableType = "Tomato (томатные овощи)";
        public string getVegetableType() 
        {
            return vegetableType;
        }
       
             
    }
    class tomatoes : Tomato
    {
        private string vegetableName = "tomatoes (помидоры)";
        private string vegetableIso = "3325534771";
        public double massa=0;
        
        private const double proteins = 1.1;
        private const double fats = 0.2;
        private const double carbohydrates = 3.8;
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
            return getProteins(proteins,massa);
        }
        
        public double getFats()
        {
            return getFats(fats, massa);
        }
        public double getCarbohydrates()
        {
            return getCarbohydrates(carbohydrates,massa);
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


    class bellPeppers : Tomato
    {
        private string vegetableName = "bell peppers (болгарский перец)";
        private string vegetableIso = "2048882201";
        public double massa = 0;

        private const double proteins = 1.3;
        private const double fats = 0.0;
        private const double carbohydrates = 5.3;
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
