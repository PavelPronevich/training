using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class TariffPlan
    {
        public int NumberOfFreeMinutes { get; set; }
        public int MonthlyPayment { get; set; }
        public int FavoriteNumber { get; set; }
        public int MinutePrice { get; set; }
        public int MinuteFavoriteNumberPrice { get; set; }
        Calls call = new Calls();

        public int GetNumberOfMinutes(Calls call)
        {
            int numberOfMinutes=0;
            long elapsedTicks = call.EndCallTime.Ticks-call.BeginCallTime.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            numberOfMinutes=(int)elapsedSpan.TotalMinutes;
            if (!(call.EndCallTime.Second == call.BeginCallTime.Second)) numberOfMinutes++;
            return numberOfMinutes;
        }
        
        public int GetNumberOfMinutes(List<Calls> calls)
        {
            int numberOfMinutes = 0;
            foreach (Calls item in calls)
            {
                numberOfMinutes += GetNumberOfMinutes(calls);
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

        public virtual double GetDebt(List<Calls> calls)
        {
            double debt=this.MonthlyPayment;
            
            int allMinutes = GetNumberOfMinutes(calls);
            int favoriteMinutes = GetNumberOfMinutesToFavoriteNumber(calls);
            if (allMinutes > this.NumberOfFreeMinutes)
            {
                allMinutes -= this.NumberOfFreeMinutes;
                allMinutes -= favoriteMinutes;
                debt+=favoriteMinutes*this.MinuteFavoriteNumberPrice+allMinutes*this.MinutePrice;
            }
            return debt;
        }

    }
}
