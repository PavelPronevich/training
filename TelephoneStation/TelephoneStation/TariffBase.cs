using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class TariffBase: ITariffPlan
    {
        
        DateTime _dateOfChange;
        static string _namePlan = "Base";
        static double _monthlyPayment=15.0;
        static double _minutePrice=0.02;

        public DateTime DateOfChange { get { return _dateOfChange; } }
        public double MonthlyPayment { get { return _monthlyPayment; } }
        public double MinutePrice { get { return _minutePrice; } }
        public string Name { get { return _namePlan; } }

        public TariffBase()
        {
            _dateOfChange=Time.Now;
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

        public double GetDebt(Calls call)
        {
            return GetNumberOfMinutes(call) * MinutePrice;
        }

        public double GetDebt(List<Calls> calls)
        {
            return (MonthlyPayment + GetNumberOfMinutes(calls) * MinutePrice) ;
        }
    }
}
