﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class RootVegetables : BaseClass   
    {
        private string vegetableType="Root vegetables (корнеплоды)";
        public string getVegetableType() 
        {
            return vegetableType;
        }
       
             
    }
    class carrot:  RootVegetables
    {
        private string vegetableName = "Carrot (морковь)";
        private string vegetableIso = "3216771";
        public double massa=0;
        
        private const double proteins = 1.2;
        private const double fats = 0.1;
        private const double carbohydrates = 7.2;
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


    class radish : RootVegetables
    {
        private string vegetableName = "radish (редис)";
        private string vegetableIso = "32829371";
        public double massa = 0;

        private const double proteins = 1.2;
        private const double fats = 0.1;
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

    class beets : RootVegetables
    {
        private string vegetableName = "beets (свекла отварная)";
        private string vegetableIso = "1839371";
        public double massa = 0;

        private const double proteins = 1.8;
        private const double fats = 0;
        private const double carbohydrates = 10.8;
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
