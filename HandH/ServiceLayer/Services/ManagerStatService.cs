using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOModels;
using DBLayer;
using ServiceLayer.DTOStat;

namespace ServiceLayer
{
    public class ManagerStatService: IDisposable
    {
        private ManagerStat ToManagerStat(string manager, int id, IEnumerable<OrderView> orders, double totalPrice)
        {
            ManagerStat managerStat=new ManagerStat();
            managerStat.Id = id;
            managerStat.Name = manager;
            managerStat.TotalOrderPrice = orders.Sum(s => s.Price);
            if (totalPrice > 0)
            {
                managerStat.Percentage = managerStat.TotalOrderPrice / totalPrice;
            }
            else
            {
                managerStat.Percentage = 0;
            }
            if (orders.Count() > 0)
            {
                managerStat.AverageOrderPrice = orders.Average(s => s.Price);
            }

            return managerStat;
        }

        private OrderService orderService = new OrderService();
        private ManagerService managerService = new ManagerService();

        public virtual List<ManagerStat> Get(double? beginPercentage, double? endPercentage, decimal minTotalPrice, 
            decimal maxTotalPrice, double minTotalAverageOrderPrice, double maxTotalAverageOrderPricee, 
            out double totalPrice, Func<OrderView, bool> filter = null)
        {
            totalPrice=0;
            
            IEnumerable<OrderView> allOrders = orderService.Get(filter);
            List<ManagerStat> managersStat = new List<ManagerStat>();
            if (allOrders.Count() > 0)
            {
                IEnumerable<ManagerView> allManagers = managerService.Get();
                totalPrice = allOrders.Sum(s => s.Price);

                var groupJoinQuery2 = from manager in allManagers
                                      join order in allOrders on manager.Id equals order.ManagerID into orderGroup
                                      select new
                                      {
                                          Manager = manager.Name,
                                          Id = manager.Id,
                                          Orders = from order2 in orderGroup
                                                   select order2
                                      };

                if ((beginPercentage == null) || (beginPercentage < 0)) beginPercentage = 0;
                if ((endPercentage == null) || (endPercentage > 100)) endPercentage = 100;

                foreach (var item in groupJoinQuery2)
                {
                    if (item.Orders.Count() > 0)
                    {
                        ManagerStat manager = ToManagerStat(item.Manager, item.Id, item.Orders, totalPrice);
                        if ((manager.Percentage >= beginPercentage) && (manager.Percentage <= endPercentage) 
                            &&(manager.AverageOrderPrice>=minTotalAverageOrderPrice)
                            && (manager.AverageOrderPrice <= maxTotalAverageOrderPricee)
                            &&(manager.TotalOrderPrice>=(double)minTotalPrice)
                            && (manager.TotalOrderPrice<=(double)maxTotalPrice))
                        {
                            managersStat.Add(ToManagerStat(item.Manager, item.Id, item.Orders, totalPrice));
                        }
                    }
                }
            }
            return managersStat;
        }
        

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    orderService.Dispose();
                    managerService.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
         
    }
}
