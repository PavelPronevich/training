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
    public class ProductStatService : IDisposable
    {
        private ProductStat ToProductStat(string product, int id, IEnumerable<OrderView> orders, double totalPrice)
        {
            ProductStat productStat = new ProductStat();
            productStat.Id = id;
            productStat.Name = product;
            productStat.TotalOrderPrice = orders.Sum(s => s.Price);
            if (totalPrice > 0)
            {
                productStat.Percentage = productStat.TotalOrderPrice / totalPrice;
            }
            else
            {
                productStat.Percentage = 0;
            }
            if (orders.Count() > 0)
            {
                productStat.AverageOrderPrice = orders.Average(s => s.Price);
            }

            return productStat;
        }

        private OrderService orderService = new OrderService();
        private ProductService productService = new ProductService();

        public virtual List<ProductStat> Get(double? beginPercentage, double? endPercentage, decimal minTotalPrice, 
            decimal maxTotalPrice, double minTotalAverageOrderPrice, double maxTotalAverageOrderPricee, 
            out double totalPrice, Func<OrderView, bool> filter = null)
        {
            totalPrice=0;
            
            IEnumerable<OrderView> allOrders = orderService.Get(filter);
            List<ProductStat> productsStat = new List<ProductStat>();
            if (allOrders.Count() > 0)
            {
                IEnumerable<ProductView> allProducts = productService.Get();
                totalPrice = allOrders.Sum(s => s.Price);

                var groupJoinQuery2 = from product in allProducts
                                      join order in allOrders on product.Id equals order.ProductID into orderGroup
                                      select new
                                      {
                                          Product = product.Name,
                                          Id = product.Id,
                                          Orders = from order2 in orderGroup
                                                   select order2
                                      };

                if ((beginPercentage == null) || (beginPercentage < 0)) beginPercentage = 0;
                if ((endPercentage == null) || (endPercentage > 100)) endPercentage = 100;

                foreach (var item in groupJoinQuery2)
                {
                    if (item.Orders.Count() > 0)
                    {
                        ProductStat product = ToProductStat(item.Product, item.Id, item.Orders, totalPrice);
                        if ((product.Percentage >= beginPercentage) && (product.Percentage <= endPercentage)
                            && (product.AverageOrderPrice >= minTotalAverageOrderPrice)
                            && (product.AverageOrderPrice <= maxTotalAverageOrderPricee)
                            && (product.TotalOrderPrice >= (double)minTotalPrice)
                            && (product.TotalOrderPrice <= (double)maxTotalPrice))
                        {
                            productsStat.Add(ToProductStat(item.Product, item.Id, item.Orders, totalPrice));
                        }
                    }
                }
            }
            return productsStat;
        }
        

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    orderService.Dispose();
                    productService.Dispose();
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
