using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class FoodStuff : IFoodStuff, IComparable
     {
        private double proteins, fats, carbohydrates, massa;
        private string vegetableType, vegetableName, vegetableIso;

        public FoodStuff(string vegetableType, string vegetableName, string vegetableIso, 
            double proteins, double fats, double carbohydrates, double massa)
        {
            this.vegetableType = vegetableType;
            this.vegetableName = vegetableName;
            this.vegetableIso = vegetableIso;
            this.proteins = proteins;
            this.fats = fats;
            this.carbohydrates = carbohydrates;
            this.massa = massa;
        }

        public double GetCalories() 
        {
            return 4 * proteins + 9 * fats + 4 * carbohydrates;
        }

        public double GetProteinsNorm()
        {
            return proteins;
        }

        public double GetProteins()
        {
            return proteins * massa / 100;
        }
        public double GetFatsNorm()
        {
            return fats;
        }
        public double GetFats()
        {
            return fats * massa / 100;
        }
        public double GetCarbohydratesNorm()
        {
            return carbohydrates;
        }
        public double GetCarbohydrates()
        {
            return carbohydrates * massa / 100;
        }
        public double GetMassa()
        {
            return massa;
        }
        public double GetEnergeticValue()
        {
            return massa * GetCalories() / 100;
        } 
        public string GetVegetableType()
        {
            return vegetableType;
        }
        public string GetName()
        {
            return vegetableName;
        }
        public string GetIso()
        {
            return vegetableIso;
        }

        public int CompareTo(object obj)
        {
            if (obj is FoodStuff)
            {
                FoodStuff other = obj as FoodStuff;
                return (this.GetProteinsNorm()).CompareTo(other.GetProteinsNorm());
            }
            else
            {
                throw new ArgumentException("Object to compare to is not a BaseClass object.");
            }
        }
    }
}
