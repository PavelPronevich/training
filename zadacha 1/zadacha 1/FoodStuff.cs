using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    
    class FoodStuff : IFoodStuff, IComparable
     {
             
        
        internal double caloriesAll, proteinsAll, fatsAll, carbohydratesAll;


        public double Proteins { get; private set; }
        public double Fats { get; private set; }
        public double Carbohydrates { get; private set; }
        public double Massa { get; private set; }
        public double Calories { get; private set; }
        public string Name { get; private set; }
        public string ISO { get; private set; }
        public VegetableType Type {get; private set;}



        public FoodStuff(VegetableType vegetableType, string vegetableName, string vegetableIso, 
            double proteins, double fats, double carbohydrates, double massa)
        {
            this.Type = vegetableType;
            this.Name = vegetableName;
            this.ISO = vegetableIso;
            this.Proteins = proteins;
            this.Fats = fats;
            this.Carbohydrates = carbohydrates;
            this.Massa = massa;
            this.proteinsAll=proteins*massa/100; 
            this.fatsAll=fats*massa/100;
            this.carbohydratesAll = carbohydrates * massa/100;
            this.Calories = 4 * proteins + 9 * fats + 4 * carbohydrates;
            this.caloriesAll = Calories * Massa / 100;
        }
 
       

                
             
       
        public int CompareTo(object obj)
        {
            if (obj is FoodStuff)
            {
                FoodStuff other = obj as FoodStuff;
                return (this.Calories).CompareTo(other.Calories);
            }
            else
            {
                throw new ArgumentException("Object to compare to is not a FoodStuff object.");
            }
        }
    }
}
