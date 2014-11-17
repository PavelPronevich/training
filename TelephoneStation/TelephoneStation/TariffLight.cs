using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class TariffLight: ITariffPlan
    {
        DateTime _dateOfChange;
        int _numberOfFreeMinutes=10;
        static double _monthlyPayment=10.0;
        int _favoriteNumber=000000;
        static double _minutePrice=0.02;
        static double _minuteFavoriteNumberPrice=0.01;
        static string _namePlan = "Light+1";

        public DateTime DateOfChange { get { return _dateOfChange; } }
        public double MonthlyPayment { get { return _monthlyPayment; } }
        public double MinutePrice { get { return _minutePrice; } }
        public string Name { get { return _namePlan; } }
        public int NumberOfFreeMinutes { get { return _numberOfFreeMinutes; }}
        public int FavoriteNumber { get { return _favoriteNumber; }}
        public double MinuteFavoriteNumberPrice { get { return _minuteFavoriteNumberPrice;}}

        public TariffLight(int favoriteTelephoneNumber)
        {
            this._dateOfChange = Time.Now;
            this._favoriteNumber = favoriteTelephoneNumber;
        }
        public TariffLight()
        {
            this._dateOfChange = Time.Now;
        }

        public void ChangeFavoriteNumber(int newFavoriteNumber)
        {
            long elapsedTicks = Time.Now.Ticks-this.DateOfChange.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            if (elapsedSpan.TotalDays>=30)
            {
                this._dateOfChange = Time.Now;
                this._favoriteNumber = newFavoriteNumber;
            }
        }
        public int GetNumberOfMinutes(Calls call)
        {
            int numberOfMinutes = 0;
            long elapsedTicks = call.EndCallTime.Ticks - call.BeginCallTime.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            numberOfMinutes = (int)elapsedSpan.TotalMinutes;
            if (!(call.EndCallTime.Second == call.BeginCallTime.Second)) numberOfMinutes++;
            return numberOfMinutes;
        }
        public int GetNumberOfMinutes(List<Calls> calls)
        {
            int numberOfMinutes = 0;
            foreach (Calls item in calls)
            {
                numberOfMinutes += GetNumberOfMinutes(item);
            }
            return numberOfMinutes;
        }

        public int GetNumberOfMinutesToFavoriteNumber(List<Calls> calls)
        {
            int numberOfMinutes = 0;
            var q=from item in calls
                  where item.IncomingPort.Number==this.FavoriteNumber
                  select item;
            foreach (var item in q) 
                numberOfMinutes+=GetNumberOfMinutes(item);
            return numberOfMinutes;
        }

        public double GetDebt(List<Calls> calls)
        {
            double debt= MonthlyPayment;
            
            int allMinutes = GetNumberOfMinutes(calls);
            int favoriteMinutes = GetNumberOfMinutesToFavoriteNumber(calls);
            if (allMinutes > this.NumberOfFreeMinutes)
            {
                allMinutes -= this.NumberOfFreeMinutes;
                if (allMinutes <= favoriteMinutes)
                {
                    debt += allMinutes * this.MinuteFavoriteNumberPrice;
                }
                else 
                {
                    debt+=favoriteMinutes*this.MinuteFavoriteNumberPrice+(allMinutes-favoriteMinutes)*this.MinutePrice;
                }
            }
            return debt;
        }

        public double GetDebt(Calls call)
        {
            if (call.IncomingPort.Number==this._favoriteNumber)
            {
                return GetNumberOfMinutes(call) * this.MinuteFavoriteNumberPrice;
            }
            else return GetNumberOfMinutes(call) * this.MinutePrice;
        
        }

    }
}
