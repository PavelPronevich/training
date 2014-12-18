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

namespace WebLayer.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private OrderService service = new OrderService();
        private ManagerService serviceManager = new ManagerService();
        private CustomerService serviceCustomer = new CustomerService();
        private ProductService serviceProduct = new ProductService();

        public ViewResult Index()
        {
            return View(service.Get());
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
