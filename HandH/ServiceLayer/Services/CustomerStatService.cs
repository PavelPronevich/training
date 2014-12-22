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
    public class CustomerStatService : IDisposable
    {
        private CustomerStat ToCustomerStat(string customer, int id, IEnumerable<OrderView> orders, double totalPrice)
        {
            CustomerStat customerStat = new CustomerStat();
            customerStat.Id = id;
            customerStat.Name = customer;
            customerStat.TotalOrderPrice = orders.Sum(s => s.Price);
            if (totalPrice > 0)
            {
                customerStat.Percentage = customerStat.TotalOrderPrice / totalPrice;
            }
            else
            {
                customerStat.Percentage = 0;
            }
            if (orders.Count() > 0)
            {
                customerStat.AverageOrderPrice = orders.Average(s => s.Price);
            }

            return customerStat;
        }

        private OrderService orderService = new OrderService();
        private CustomerService customerService = new CustomerService();

        public virtual List<CustomerStat> Get(double? beginPercentage, double? endPercentage, decimal minTotalPrice, 
            decimal maxTotalPrice, double minTotalAverageOrderPrice, double maxTotalAverageOrderPricee, 
            out double totalPrice, Func<OrderView, bool> filter = null)
        {
            totalPrice=0;
            
            IEnumerable<OrderView> allOrders = orderService.Get(filter);
            List<CustomerStat> customersStat = new List<CustomerStat>();
            if (allOrders.Count() > 0)
            {
                IEnumerable<CustomerView> allCustomers = customerService.Get();
                totalPrice = allOrders.Sum(s => s.Price);

                var groupJoinQuery2 = from customer in allCustomers
                                      join order in allOrders on customer.Id equals order.CustomerID into orderGroup
                                      select new
                                      {
                                          Customer = customer.Name,
                                          Id = customer.Id,
                                          Orders = from order2 in orderGroup
                                                   select order2
                                      };

                if ((beginPercentage == null) || (beginPercentage < 0)) beginPercentage = 0;
                if ((endPercentage == null) || (endPercentage > 100)) endPercentage = 100;

                foreach (var item in groupJoinQuery2)
                {
                    if (item.Orders.Count() > 0)
                    {
                        CustomerStat customer = ToCustomerStat(item.Customer, item.Id, item.Orders, totalPrice);
                        if ((customer.Percentage >= beginPercentage) && (customer.Percentage <= endPercentage)
                            && (customer.AverageOrderPrice >= minTotalAverageOrderPrice)
                            && (customer.AverageOrderPrice <= maxTotalAverageOrderPricee)
                            && (customer.TotalOrderPrice >= (double)minTotalPrice)
                            && (customer.TotalOrderPrice <= (double)maxTotalPrice))
                        {
                            customersStat.Add(ToCustomerStat(item.Customer, item.Id, item.Orders, totalPrice));
                        }
                    }
                }
            }
            return customersStat;
        }
        

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    orderService.Dispose();
                    customerService.Dispose();
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
