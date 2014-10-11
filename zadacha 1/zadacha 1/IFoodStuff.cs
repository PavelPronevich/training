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
          double GetCaloriesNorm();
          double GetProteins();
          double GetProteinsNorm();
          double GetFats();
          double GetFatsNorm();
          double GetCarbohydrates();
          double GetCarbohydratesNorm();
          string GetName();

    }
}
