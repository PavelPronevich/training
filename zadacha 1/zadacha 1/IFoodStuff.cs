using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    interface IFoodStuff
    {
          double GetCalories();
          double GetProteins();
          double GetFats();
          double GetCarbohydrates();
          //double GetEnergeticValue();
          string GetName();

    }
}
