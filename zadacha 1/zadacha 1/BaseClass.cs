using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class BaseClass  : Ivagetable, IComparable
     {
        protected double proteins, fats, carbohydrates, massa;
        protected string vegetableType, vegetableName, vegetableIso;

        public BaseClass(string vegetableType, string vegetableName, string vegetableIso, 
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

        public virtual double getCalories() 
        {
            return 4 * proteins + 9 * fats + 4 * carbohydrates;
        }
        public double getProteins()
        {
            return proteins * massa / 100;
        }
        public double getFats()
        {
            return fats * massa / 100;
        }
        public double getCarbohydrates()
        {
            return carbohydrates * massa / 100;
        }
        public double getEnergeticValue()
        {
            return massa * getCalories() / 100;
        } 
        public string getVegetableType()
        {
            return vegetableType;
        }
        public string getName()
        {
            return vegetableName;
        }
        public string getIso()
        {
            return vegetableIso;
        }

        public int CompareTo(object obj)
        {
            if (obj is BaseClass)
            {
                BaseClass other = obj as BaseClass;
                return (this.getProteins()).CompareTo(other.getProteins());
            }
            else
            {
                throw new ArgumentException("Object to compare to is not a BaseClass object.");
            }
        }
    }
}
