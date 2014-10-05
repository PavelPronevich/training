using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class Leguminousvegetables: BaseClass   
    {
        private string vegetableType = "Leguminous Vegetables (бобовые)";
        public string getVegetableType() 
        {
            return vegetableType;
        }
       
             
    }
    class beans : Leguminousvegetables
    {
        private string vegetableName = "Beans (фасоль отварная)";
        private string vegetableIso = "3234165721";
        public double massa=0;
        
        private const double proteins = 9.6;
        private const double fats = 0.5;
        private const double carbohydrates = 20.2;
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

    
}
