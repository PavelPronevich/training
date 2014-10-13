using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class Salad<X> : IFoodStuff, IList<X> where X : FoodStuff
    {
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

        public double Massa
        {
            get
            {
                double massaSalad = 0;
                foreach (FoodStuff ingr in salat)
                {
                    massaSalad += ingr.Massa;
                }

                return massaSalad;
            }

             
        }

        public double Proteins
        {
            get
            {
                double proteinsSalad=0;
                foreach (FoodStuff ingr in salat)
                {
                    proteinsSalad += ingr.proteinsAll;
                }
                return (proteinsSalad / Massa* 100);
            }
            
        }
        
        public double Fats
        {
            get
            {
                double fatsSalad=0;
            foreach (FoodStuff ingr in salat)
                {
                    fatsSalad += ingr.fatsAll;
                } 
               return fatsSalad / Massa * 100;
            }
            
        }

        public double Carbohydrates
        {
            get
            {
                double carbohydratesSalad = 0;
                foreach (FoodStuff ingr in salat)
                {
                    carbohydratesSalad += ingr.carbohydratesAll;
                    
                }
                return carbohydratesSalad / Massa * 100;
            }
            
        }


        double caloriesAll = 0;
        public double Calories
        {
            get{
                
                foreach (FoodStuff ingr in salat)
                {
                    caloriesAll += ingr.caloriesAll;
                    
                }
                return caloriesAll / Massa * 100;
            
            }

        
        }
        

        
        public string Name { get; set; }

        public void Sort()
        {
            salat.Sort();
        }

        public List<FoodStuff> RangeOfValuesToList(double a, double b)
        {
            List<FoodStuff> salatt = new List<FoodStuff>();
            foreach (FoodStuff ingr in salat)
            {
                if (ingr.Calories>=a && ingr.Calories<=b)
                {
                    salatt.Add(ingr);
                }
            }
            
            return salatt;
        }

        public void RangeOfValuesToType(double a, double b)
        {
            Console.WriteLine("Ingredients of salad \"{0}\"  with calorie \nfrom {1} to {2} kilocalories in 100 gramms", 
                this.Name, a, b);
            
            foreach (FoodStuff ingr in salat)
            {
                if (ingr.Calories >= a && ingr.Calories <= b)
                {
                    Console.WriteLine("{0}, {1}", ingr.Name, ingr.Calories);
                }
            }
        }

    }
}
