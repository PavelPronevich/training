using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public interface ITariffPlan
    {
        int GetNumberOfMinutes(Calls call);
        int GetNumberOfMinutes(List<Calls> calls);
        double GetDebt(List<Calls> calls);
        double GetDebt(Calls coll);
        string Name { get; }
    }
}
