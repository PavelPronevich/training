using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceLayer.DTOModels;
using ServiceLayer;
using System.IO;
using System.Linq.Expressions;

namespace WebLayer.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private OrderService service = new OrderService();
        private ManagerService serviceManager = new ManagerService();
        private CustomerService serviceCustomer = new CustomerService();
        private ProductService serviceProduct = new ProductService();

        public ViewResult Index(string sortOrder, string searchManagerString, string searchCustomerString, 
            string searchProductString, string beginTime, string endTime, string minPrice, string maxPrice)
        {
            if (string.IsNullOrEmpty(searchManagerString)) searchManagerString="";
            if (string.IsNullOrEmpty(searchCustomerString)) searchCustomerString="";
            if (string.IsNullOrEmpty(searchProductString)) searchProductString="";
            DateTime beginDateTime;
            DateTime endDateTime;
            if (!DateTime.TryParse(beginTime,out beginDateTime)) beginDateTime=new DateTime();
            if (!DateTime.TryParse(endTime,out endDateTime)) endDateTime=DateTime.Now;
            decimal minOrderPrice;
            decimal maxOrderPrice;
            if (!decimal.TryParse(minPrice,out minOrderPrice)) minOrderPrice=decimal.MinValue;
            if (!decimal.TryParse(maxPrice,out maxOrderPrice)) maxOrderPrice=decimal.MaxValue;

            Func<OrderView, bool> filter=x=>((x.ManagerName.ToUpper().Contains(searchManagerString.ToUpper()))
                && (x.CustomerName.ToUpper().Contains(searchCustomerString.ToUpper()))
                && (x.ProductName.ToUpper().Contains(searchProductString.ToUpper())) 
                && (x.OrderDate<=endDateTime) 
                && (beginDateTime<=x.OrderDate)
                && ((decimal)x.Price<=maxOrderPrice)
                && ((decimal)x.Price>=minOrderPrice));
            
            ViewBag.ManagerSortParm = String.IsNullOrEmpty(sortOrder) ? "Manager_desc" : "";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "Customer_desc" : "Customer";
            ViewBag.ProductSortParm = sortOrder == "Product" ? "Product_desc" : "Product";
            ViewBag.DateSortParm = sortOrder == "OrderDate" ? "OrderDate_desc" : "OrderDate";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            
            IEnumerable<OrderView> ordersView; 
            switch (sortOrder)
            {
                case "Manager_desc":
                    ordersView = service.Get(filter,
                        x=>x.OrderByDescending(s => s.ManagerName));
                    break;
                case "Customer_desc":
                    ordersView = service.Get(filter,x=>x.OrderByDescending(s => s.CustomerName));
                    break;
                case "Customer":
                    ordersView = service.Get(filter,x=>x.OrderBy(s => s.CustomerName));
                    break;
                case "Product":
                    ordersView = service.Get(filter,x=>x.OrderBy(s => s.ProductName));
                    break;
                case "Product_desc":
                    ordersView = service.Get(filter,x=>x.OrderByDescending(s => s.ProductName));
                    break;
                case "OrderDate_desc":
                    ordersView = service.Get(filter,x=>x.OrderByDescending(s => s.OrderDate));
                    break;
                case "OrderDate":
                    ordersView = service.Get(filter,x=>x.OrderBy(s => s.OrderDate));
                    break;
                case "Price":
                    ordersView = service.Get(filter,x=>x.OrderBy(s => s.Price));
                    break;
                case "Price_desc":
                    ordersView = service.Get(filter, x => x.OrderByDescending(s => s.Price));
                    break;
                default:
                    ordersView = service.Get(filter, x => x.OrderBy(s => s.ManagerName));
                    break;
            }

            return View(ordersView);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(service.GetByID((int)id));
        }
        
        
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(serviceCustomer.Get(), "Id", "Name");
            ViewBag.ManagerID = new SelectList(serviceManager.Get(), "Id", "Name");
            ViewBag.ProductID = new SelectList(serviceProduct.Get(), "Id", "Name");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ManagerID,CustomerID,ProductID,Price")] OrderView orderView)
        {
            if (ModelState.IsValid)
            {
                orderView.OrderDate = DateTime.Now;
                service.Insert(orderView);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(serviceCustomer.Get(), "Id", "Name", orderView.CustomerID);
            ViewBag.ManagerID = new SelectList(serviceManager.Get(), "Id", "Name", orderView.ManagerID);
            ViewBag.ProductID = new SelectList(serviceProduct.Get(), "Id", "Name", orderView.ProductID);
            return View(orderView);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderView orderView=service.GetByID((int)id);
            if (orderView == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(serviceCustomer.Get(), "Id", "Name", orderView.CustomerID);
            ViewBag.ManagerID = new SelectList(serviceManager.Get(), "Id", "Name", orderView.ManagerID);
            ViewBag.ProductID = new SelectList(serviceProduct.Get(), "Id", "Name", orderView.ProductID);
            return View(orderView);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include="Id,OrderDate,Price,ManagerID,ProductID,CustomerID")] OrderView orderView)
        {
            try
         {
            if (ModelState.IsValid)
            {
                service.Update(orderView);
                return RedirectToAction("Index");
            }
         }
         catch (DataException )
         {
              ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
         }
            ViewBag.CustomerID = new SelectList(serviceCustomer.Get(), "Id", "Name", orderView.CustomerID);
            ViewBag.ManagerID = new SelectList(serviceManager.Get(), "Id", "Name", orderView.ManagerID);
            ViewBag.ProductID = new SelectList(serviceProduct.Get(), "Id", "Name", orderView.ProductID);
         return View(orderView);
        }
         

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(service.GetByID((int)id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
            
        }
 
    }
}
