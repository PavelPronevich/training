using ServiceLayer;
using ServiceLayer.DTOModels;
using ServiceLayer.DTOStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLayer.Models;

namespace WebLayer.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private ManagerStatService managerStatService = new ManagerStatService();
        public ActionResult Managers(string searchManagerString, string beginTime, string endTime, string minPrice, string maxPrice,
            string minPercentage, string maxPercentage, string minAverageOrderPrice, string maxinAverageOrderPrice, string orderBy)
        {
            if (string.IsNullOrEmpty(searchManagerString)) searchManagerString = "";
            DateTime beginDateTime;
            DateTime endDateTime;
            if (!DateTime.TryParse(beginTime, out beginDateTime)) beginDateTime = new DateTime();
            if (!DateTime.TryParse(endTime, out endDateTime))
            {
                endDateTime = DateTime.Now;
            }
            else
            {
                endDateTime = endDateTime.AddDays(1);
            }
            decimal minTotalPrice;
            decimal maxTotalPrice;
            if (!decimal.TryParse(minPrice, out minTotalPrice)) minTotalPrice = decimal.MinValue;
            if (!decimal.TryParse(maxPrice, out maxTotalPrice)) maxTotalPrice = decimal.MaxValue;
            double minTotalPercentage;
            double maxTotalPercentage;
            if (!double.TryParse(minPercentage, out minTotalPercentage)) minTotalPercentage = 0;
            if (!double.TryParse(maxPercentage, out maxTotalPercentage)) maxTotalPercentage = 100;
            minTotalPercentage /= 100;
            maxTotalPercentage /= 100;
            double minTotalAverageOrderPrice;
            double maxTotalAverageOrderPricee;
            if (!double.TryParse(minAverageOrderPrice, out minTotalAverageOrderPrice)) minTotalAverageOrderPrice = 0;
            if (!double.TryParse(maxinAverageOrderPrice, out maxTotalAverageOrderPricee)) maxTotalAverageOrderPricee = double.MaxValue;
            List<SelectListItem> sortBy = new List<SelectListItem>();
            sortBy.Add(new SelectListItem { Value = "1", Text = "Manager" });
            sortBy.Add(new SelectListItem { Value = "2", Text = "Manager Descending" });
            sortBy.Add(new SelectListItem { Value = "3", Text = "Total Order Price" });
            sortBy.Add(new SelectListItem { Value = "4", Text = "Total Order Price Descending" });
            sortBy.Add(new SelectListItem { Value = "5", Text = "Percentage" });
            sortBy.Add(new SelectListItem { Value = "6", Text = "Percentage Descending" });
            sortBy.Add(new SelectListItem { Value = "7", Text = "Average Order Price" });
            sortBy.Add(new SelectListItem { Value = "8", Text = "Average Order Price Descending" });

            ViewBag.orderBy = new SelectList(sortBy, "Value", "Text");

            Func<OrderView, bool> filter = x => ((x.ManagerName.ToUpper().Contains(searchManagerString.ToUpper()))
                && (x.OrderDate <= endDateTime)
                && (beginDateTime <= x.OrderDate));

            double totalPrice;
            
            IEnumerable<ManagerStat> managers=managerStatService.Get(minTotalPercentage, maxTotalPercentage,
                minTotalPrice,maxTotalPrice, minTotalAverageOrderPrice,maxTotalAverageOrderPricee, 
                out totalPrice, filter);
            @ViewBag.totalPrice = string.Format("{0:C2}",totalPrice);
            switch (orderBy)
            {
                case "1":
                    managers = managers.OrderBy(s => s.Name);
                    break;
                case "2":
                    managers = managers.OrderByDescending(s => s.Name);
                    break;
                case "3":
                    managers = managers.OrderBy(s => s.TotalOrderPrice);
                    break;
                case "4":
                    managers = managers.OrderByDescending(s => s.TotalOrderPrice);
                    break;
                case "5":
                    managers = managers.OrderBy(s => s.Percentage);
                    break;
                case "6":
                    managers = managers.OrderByDescending(s => s.Percentage);
                    break;
                case "7":
                    managers = managers.OrderBy(s => s.AverageOrderPrice);
                    break;
                case "8":
                    managers = managers.OrderByDescending(s => s.AverageOrderPrice);
                    break;
                default:
                    managers = managers.OrderBy(s => s.Name);
                    break;
            }

            return View(managers);
        }
        private CustomerStatService customerStatService = new CustomerStatService();
        public ActionResult Customers(string searchCustomerString, string beginTime, string endTime, string minPrice, string maxPrice,
            string minPercentage, string maxPercentage, string minAverageOrderPrice, string maxinAverageOrderPrice, string orderBy)
        {
            if (string.IsNullOrEmpty(searchCustomerString)) searchCustomerString = "";
            DateTime beginDateTime;
            DateTime endDateTime;
            if (!DateTime.TryParse(beginTime, out beginDateTime)) beginDateTime = new DateTime();
            if (!DateTime.TryParse(endTime, out endDateTime))
            {
                endDateTime = DateTime.Now;
            }
            else
            {
                endDateTime = endDateTime.AddDays(1);
            }
            decimal minTotalPrice;
            decimal maxTotalPrice;
            if (!decimal.TryParse(minPrice, out minTotalPrice)) minTotalPrice = decimal.MinValue;
            if (!decimal.TryParse(maxPrice, out maxTotalPrice)) maxTotalPrice = decimal.MaxValue;
            double minTotalPercentage;
            double maxTotalPercentage;
            if (!double.TryParse(minPercentage, out minTotalPercentage)) minTotalPercentage = 0;
            if (!double.TryParse(maxPercentage, out maxTotalPercentage)) maxTotalPercentage = 100;
            minTotalPercentage /= 100;
            maxTotalPercentage /= 100;
            double minTotalAverageOrderPrice;
            double maxTotalAverageOrderPricee;
            if (!double.TryParse(minAverageOrderPrice, out minTotalAverageOrderPrice)) minTotalAverageOrderPrice = 0;
            if (!double.TryParse(maxinAverageOrderPrice, out maxTotalAverageOrderPricee)) maxTotalAverageOrderPricee = double.MaxValue;
            List<SelectListItem> sortBy = new List<SelectListItem>();
            sortBy.Add(new SelectListItem { Value = "1", Text = "Customer" });
            sortBy.Add(new SelectListItem { Value = "2", Text = "Customer Descending" });
            sortBy.Add(new SelectListItem { Value = "3", Text = "Total Order Price" });
            sortBy.Add(new SelectListItem { Value = "4", Text = "Total Order Price Descending" });
            sortBy.Add(new SelectListItem { Value = "5", Text = "Percentage" });
            sortBy.Add(new SelectListItem { Value = "6", Text = "Percentage Descending" });
            sortBy.Add(new SelectListItem { Value = "7", Text = "Average Order Price" });
            sortBy.Add(new SelectListItem { Value = "8", Text = "Average Order Price Descending" });

            ViewBag.orderBy = new SelectList(sortBy, "Value", "Text");

            Func<OrderView, bool> filter = x => ((x.CustomerName.ToUpper().Contains(searchCustomerString.ToUpper()))
                && (x.OrderDate <= endDateTime)
                && (beginDateTime <= x.OrderDate));

            double totalPrice;

            IEnumerable<CustomerStat> customers = customerStatService.Get(minTotalPercentage, maxTotalPercentage,
                minTotalPrice, maxTotalPrice, minTotalAverageOrderPrice, maxTotalAverageOrderPricee,
                out totalPrice, filter);
            @ViewBag.totalPrice = string.Format("{0:C2}", totalPrice);
            switch (orderBy)
            {
                case "1":
                    customers = customers.OrderBy(s => s.Name);
                    break;
                case "2":
                    customers = customers.OrderByDescending(s => s.Name);
                    break;
                case "3":
                    customers = customers.OrderBy(s => s.TotalOrderPrice);
                    break;
                case "4":
                    customers = customers.OrderByDescending(s => s.TotalOrderPrice);
                    break;
                case "5":
                    customers = customers.OrderBy(s => s.Percentage);
                    break;
                case "6":
                    customers = customers.OrderByDescending(s => s.Percentage);
                    break;
                case "7":
                    customers = customers.OrderBy(s => s.AverageOrderPrice);
                    break;
                case "8":
                    customers = customers.OrderByDescending(s => s.AverageOrderPrice);
                    break;
                default:
                    customers = customers.OrderBy(s => s.Name);
                    break;
            }

            return View(customers);
        }
        private ProdustStatService productStatService = new ProdustStatService();
        public ActionResult Produsts(string searchProdustString, string beginTime, string endTime, string minPrice, string maxPrice,
            string minPercentage, string maxPercentage, string minAverageOrderPrice, string maxinAverageOrderPrice, string orderBy)
        {
            if (string.IsNullOrEmpty(searchProdustString)) searchProdustString = "";
            DateTime beginDateTime;
            DateTime endDateTime;
            if (!DateTime.TryParse(beginTime, out beginDateTime)) beginDateTime = new DateTime();
            if (!DateTime.TryParse(endTime, out endDateTime))
            {
                endDateTime = DateTime.Now;
            }
            else
            {
                endDateTime = endDateTime.AddDays(1);
            }
            decimal minTotalPrice;
            decimal maxTotalPrice;
            if (!decimal.TryParse(minPrice, out minTotalPrice)) minTotalPrice = decimal.MinValue;
            if (!decimal.TryParse(maxPrice, out maxTotalPrice)) maxTotalPrice = decimal.MaxValue;
            double minTotalPercentage;
            double maxTotalPercentage;
            if (!double.TryParse(minPercentage, out minTotalPercentage)) minTotalPercentage = 0;
            if (!double.TryParse(maxPercentage, out maxTotalPercentage)) maxTotalPercentage = 100;
            minTotalPercentage /= 100;
            maxTotalPercentage /= 100;
            double minTotalAverageOrderPrice;
            double maxTotalAverageOrderPricee;
            if (!double.TryParse(minAverageOrderPrice, out minTotalAverageOrderPrice)) minTotalAverageOrderPrice = 0;
            if (!double.TryParse(maxinAverageOrderPrice, out maxTotalAverageOrderPricee)) maxTotalAverageOrderPricee = double.MaxValue;
            List<SelectListItem> sortBy = new List<SelectListItem>();
            sortBy.Add(new SelectListItem { Value = "1", Text = "Produst" });
            sortBy.Add(new SelectListItem { Value = "2", Text = "Produst Descending" });
            sortBy.Add(new SelectListItem { Value = "3", Text = "Total Order Price" });
            sortBy.Add(new SelectListItem { Value = "4", Text = "Total Order Price Descending" });
            sortBy.Add(new SelectListItem { Value = "5", Text = "Percentage" });
            sortBy.Add(new SelectListItem { Value = "6", Text = "Percentage Descending" });
            sortBy.Add(new SelectListItem { Value = "7", Text = "Average Order Price" });
            sortBy.Add(new SelectListItem { Value = "8", Text = "Average Order Price Descending" });

            ViewBag.orderBy = new SelectList(sortBy, "Value", "Text");

            Func<OrderView, bool> filter = x => ((x.ProductName.ToUpper().Contains(searchProdustString.ToUpper()))
                && (x.OrderDate <= endDateTime)
                && (beginDateTime <= x.OrderDate));

            double totalPrice;

            IEnumerable<ProductStat> products = productStatService.Get(minTotalPercentage, maxTotalPercentage,
                minTotalPrice, maxTotalPrice, minTotalAverageOrderPrice, maxTotalAverageOrderPricee,
                out totalPrice, filter);
            @ViewBag.totalPrice = string.Format("{0:C2}", totalPrice);
            switch (orderBy)
            {
                case "1":
                    products = products.OrderBy(s => s.Name);
                    break;
                case "2":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "3":
                    products = products.OrderBy(s => s.TotalOrderPrice);
                    break;
                case "4":
                    products = products.OrderByDescending(s => s.TotalOrderPrice);
                    break;
                case "5":
                    products = products.OrderBy(s => s.Percentage);
                    break;
                case "6":
                    products = products.OrderByDescending(s => s.Percentage);
                    break;
                case "7":
                    products = products.OrderBy(s => s.AverageOrderPrice);
                    break;
                case "8":
                    products = products.OrderByDescending(s => s.AverageOrderPrice);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            return View(products);
        }
        private OrderStatService orderStatService = new OrderStatService();

        public ActionResult Orders(string searchManagerString, string searchCustomerString,
            string searchProductString, string beginTime, string endTime, string minPrice, string maxPrice,
            string minPercentage, string maxPercentage, string orderBy)
        {
            double totalPrice=0;
            if (string.IsNullOrEmpty(searchManagerString)) searchManagerString = "";
            if (string.IsNullOrEmpty(searchCustomerString)) searchCustomerString = "";
            if (string.IsNullOrEmpty(searchProductString)) searchProductString = "";
            DateTime beginDateTime;
            DateTime endDateTime;
            if (!DateTime.TryParse(beginTime, out beginDateTime)) beginDateTime = new DateTime();
            if (!DateTime.TryParse(endTime, out endDateTime))
                endDateTime = DateTime.Now;
            else endDateTime=endDateTime.AddDays(1);
            decimal minOrderPrice;
            decimal maxOrderPrice;
            if (!decimal.TryParse(minPrice, out minOrderPrice)) minOrderPrice = decimal.MinValue;
            if (!decimal.TryParse(maxPrice, out maxOrderPrice)) maxOrderPrice = decimal.MaxValue;
            double minOrderPercentage;
            double maxOrderPercentage;
            if (!double.TryParse(minPercentage, out minOrderPercentage)) minOrderPercentage = 0;
            if (!double.TryParse(maxPercentage, out maxOrderPercentage)) maxOrderPercentage = 100;
            minOrderPercentage /= 100;
            maxOrderPercentage /= 100;

            List<SelectListItem> sortBy = new List<SelectListItem>();
            sortBy.Add(new SelectListItem{ Value = "1", Text = "Manager" });
            sortBy.Add(new SelectListItem{ Value = "2", Text = "Manager Descending" });
            sortBy.Add(new SelectListItem { Value = "3", Text = "Customer" });
            sortBy.Add(new SelectListItem { Value = "4", Text = "Customer Descending" });
            sortBy.Add(new SelectListItem { Value = "5", Text = "Product" });
            sortBy.Add(new SelectListItem { Value = "6", Text = "Product Descending" });
            sortBy.Add(new SelectListItem { Value = "7", Text = "Order Date Descending" });
            sortBy.Add(new SelectListItem { Value = "8", Text = "Order Date Descending" });
            sortBy.Add(new SelectListItem { Value = "9", Text = "Price" });
            sortBy.Add(new SelectListItem { Value = "10", Text = "Price Descending" });
            sortBy.Add(new SelectListItem { Value = "11", Text = "Percentage" });
            sortBy.Add(new SelectListItem { Value = "12", Text = "Percentage Descending" });
            
            ViewBag.orderBy = new SelectList(sortBy,"Value","Text");

            Func<OrderView, bool> filter = x => ((x.ManagerName.ToUpper().Contains(searchManagerString.ToUpper()))
                && (x.CustomerName.ToUpper().Contains(searchCustomerString.ToUpper()))
                && (x.ProductName.ToUpper().Contains(searchProductString.ToUpper()))
                && (x.OrderDate <= endDateTime)
                && (beginDateTime <= x.OrderDate)
                && ((decimal)x.Price <= maxOrderPrice)
                && ((decimal)x.Price >= minOrderPrice));
            
            double averagePrice;
            IEnumerable<OrderStat> orders = orderStatService.Get(minOrderPercentage,maxOrderPercentage,
                out totalPrice, out averagePrice, filter);

            switch (orderBy)
            {
                case "1":
                    orders = orders.OrderBy(s => s.ManagerName);
                    break;
                case "2":
                    orders = orders.OrderByDescending(s => s.ManagerName);
                    break;
                case "3":
                    orders = orders.OrderBy(s => s.CustomerName);
                    break;
                case "4":
                    orders = orders.OrderByDescending(s => s.CustomerName);
                    break;
                case "5":
                    orders = orders.OrderBy(s => s.ProductName);
                    break;
                case "6":
                    orders = orders.OrderByDescending(s => s.ProductName);
                    break;
                case "7":
                    orders = orders.OrderBy(s => s.OrderDate);
                    break;
                case "8":
                    orders = orders.OrderByDescending(s => s.OrderDate);
                    break;
                case "9":
                    orders = orders.OrderBy(s => s.Price);
                    break;
                case "10":
                    orders = orders.OrderByDescending(s => s.Price);
                    break;
                case "11":
                    orders = orders.OrderBy(s => s.Percentage);
                    break;
                case "12":
                    orders = orders.OrderByDescending(s => s.Percentage);
                    break;
                default:
                    orders = orders.OrderBy(s => s.ManagerName);
                    break;
            }


            ViewBag.totalPrice = string.Format("{0:C2}", totalPrice);
            ViewBag.averagePrice = string.Format("{0:C2}",averagePrice);

            if (beginDateTime.Year == 0) beginDateTime = orders.Min(s => s.OrderDate);
            TimeSpan time = (endDateTime - beginDateTime);
            if (time.Days > 0)
            {
                double averageOrdersNumber = (double)orders.Count() / (double)time.Days;
                ViewBag.averageOrders = string.Format("{0:F2}", averageOrdersNumber);

            }
            else ViewBag.averageOrders = string.Format("The total number of days is less than 1. Please change the reporting time period");
            
            return View(orders.ToList());
        }
	}
}