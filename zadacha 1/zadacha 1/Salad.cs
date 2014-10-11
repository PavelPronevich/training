﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class Salad<X> : IFoodStuff, IList<X> where X : FoodStuff
    {
        public string Name;

        private List<X> salat= new List<X>();
        public List<X> Salat
        {
            get
            {
                return salat;
            }
        }
        
        public int IndexOf(X item)
        {
            return salat.IndexOf(item);
        }

        public void Insert(int index, X item)
        {
            salat.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            salat.RemoveAt(index);
        }

        public X this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(X item)
        {
            salat.Add(item);
        }

        public void Clear()
        {
            salat.Clear();
        }

        public bool Contains(X item)
        {
            return salat.Contains(item);
        }

        public void CopyTo(X[] array, int arrayIndex)
        {
            salat.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(X item)
        {
            return salat.Remove(item);
        }

        public IEnumerator<X> GetEnumerator()
        {
            return salat.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }



        public double GetMassa()
        {
            double massa = 0;
            foreach (FoodStuff ingr in salat)
            {
                massa += ingr.GetMassa();
            }
            return massa;
        }
        public double GetProteins()
        {
            double proteins = 0;
            foreach (FoodStuff ingr in salat)
            {
                proteins += ingr.GetProteins();
            }
            return proteins;
        }
        public double GetProteinsNorm()
        {
            return GetProteins()/GetMassa()*100;
        }

        public double GetFats()
        {
            double fats = 0;
            foreach (FoodStuff ingr in salat)
            {
                fats += ingr.GetFats();
            }
            return fats;
        }
        public double GetFatsNorm()
        {
            return GetFats() / GetMassa() * 100;
        }

        public double GetCarbohydrates()
        {
            double carbohydrates = 0;
            foreach (FoodStuff ingr in salat)
            {
                carbohydrates += ingr.GetCarbohydrates();
            }
            return carbohydrates;
        }
        public double GetCarbohydratesNorm()
        {
            return GetCarbohydrates() / GetMassa() * 100;
        }

        public double GetCalories()
        {
            return 4*this.GetProteins()+9*this.GetFats()+4*this.GetCarbohydrates();
        }

        
        public double GetCaloriesNorm()
        {
            return this.GetCalories()/GetMassa()*100;
        }
               

        public string GetName()
        {
            return Name;
        }

        public void Sort()
        {
            salat.Sort();
        }

        public List<FoodStuff> RangeOfValuesToList(double a, double b)
        {
            List<FoodStuff> salatt = new List<FoodStuff>();
            foreach (FoodStuff ingr in salat)
            {
                if (ingr.GetCaloriesNorm()>=a && ingr.GetCaloriesNorm()<=b)
                {
                    salatt.Add(ingr);
                }
            }
            
            return salatt;
        }

        public void RangeOfValuesToType(double a, double b)
        {
            Console.WriteLine("Ingredients of salad \"{0}\"  with calorie \nfrom {1} to {2} kilocalories in 100 gramms", 
                this.GetName(), a, b);
            
            foreach (FoodStuff ingr in salat)
            {
                if (ingr.GetCaloriesNorm() >= a && ingr.GetCaloriesNorm() <= b)
                {
                    Console.WriteLine("{0}, {1}", ingr.GetName(), ingr.GetCaloriesNorm());
                }
            }
        }

    }
}