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
    public class OrderStatService: IDisposable
    {
        private OrderStat OrderViewToOrderStat(OrderView order, double totalPrice)
        {
            OrderStat orderStat=new OrderStat();
            orderStat.Id = order.Id; 
            orderStat.CustomerID= order.CustomerID;
            orderStat.ManagerID=order.ManagerID;
            orderStat.ProductID=order.ProductID;
            orderStat.OrderDate=order.OrderDate;
            orderStat.Price=order.Price;
            orderStat.Percentage =orderStat.Price/totalPrice;
            orderStat.CustomerName= order.CustomerName;
            orderStat.ProductName= order.CustomerName;
            orderStat.ManagerName = order.ManagerName;
            return orderStat;
        }

        private IList<OrderStat> OrderViewToOrderStat(IEnumerable<OrderView> orders, double totalPrice)
        {
            List<OrderStat> ordersStat=new List<OrderStat>();
            foreach (var item in orders)
            {
                ordersStat.Add(OrderViewToOrderStat(item, totalPrice));
            }
            return ordersStat;
        }

        private OrderService service = new OrderService();

        public virtual List<OrderStat> Get(double? beginPercentage, double? endPercentage, 
            out double totalPrice, out double averagePrice, Func<OrderView, bool> filter = null)
        {
            IEnumerable<OrderView> allOrders = service.Get(filter);
            totalPrice = 0;

            foreach (var item in allOrders)
            {
                totalPrice += item.Price;
            }

            IEnumerable<OrderStat> allOrderStat = OrderViewToOrderStat(allOrders,totalPrice);
            if ((beginPercentage==null)||(beginPercentage<0)) beginPercentage=0;
            if ((endPercentage==null)||(endPercentage>100)) endPercentage=100;
            
            var query = from item in allOrderStat
                        where ((item.Percentage>=beginPercentage)&&(item.Percentage<=endPercentage))
                        select item;

            averagePrice = 0;
            if (query.Count() > 0)
            {
                averagePrice = totalPrice / query.Count();
            }

            return query.ToList<OrderStat>();
            
        }
        
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                   service.Dispose();
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
