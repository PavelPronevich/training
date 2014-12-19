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
            string searchProductString)
        {
            ViewBag.ManagerSortParm = String.IsNullOrEmpty(sortOrder) ? "Manager_desc" : "";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "Customer_desc" : "Customer";
            ViewBag.ProductSortParm = sortOrder == "Product" ? "Product_desc" : "Product";
            ViewBag.DateSortParm = sortOrder == "OrderDate" ? "OrderDate_desc" : "OrderDate";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            
            
            
            Func<OrderView, bool> filter;

            if (!String.IsNullOrEmpty(searchManagerString))
            {
                Expression<Func<OrderView,bool>> filter1 = x=>(x.ManagerName.ToUpper().Contains(searchManagerString.ToUpper()));
            }
            else 
            {
                Expression<Func<OrderView,bool>> filter1 = x=>true;
            }

            if (!String.IsNullOrEmpty(searchCustomerString))
            {
                Expression<Func<OrderView,bool>> filter2 = x=>(x.CustomerName.ToUpper().Contains(searchCustomerString.ToUpper()));
            }
            else 
            {
                Expression<Func<OrderView,bool>> filter2 = x=>true;
            }

            if (!String.IsNullOrEmpty(searchProductString))
            {
                Expression<Func<OrderView,bool>> filter3 = x=>(x.ProductName.ToUpper().Contains(searchProductString.ToUpper()));
            }
            else 
            {
                Expression<Func<OrderView,bool>> filter3 = x=>true;
            }
            
            flter = x=> filter1


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
        
        /*
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
         */

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



        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Name")] ManagerView managerView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Insert(managerView);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException )
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
         return View(managerView);
        }
         */

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
        
        //public ActionResult ManagerSearch(string name)
        //{
        //    return PartialView(service.Get(a => a.Name.Contains(name)));
        //}

        /*[HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddFileToServer(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }
         <p>
    @if (User.IsInRole("admin"))
    {
        using (Html.BeginForm("AddFileToServer", "Order", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {
            <input type="file" name="file" />
            <input type="submit" value="OK" />
        }
    }

</p>
         */

    }
}
