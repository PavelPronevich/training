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

namespace WebLayer.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductService service = new ProductService();

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

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Name")] ProductView productView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Insert(productView);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
         return View(productView);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(service.GetByID((int)id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Name")] ProductView productView)
        {
            try
         {
            if (ModelState.IsValid)
            {
                service.Update(productView);
                return RedirectToAction("Index");
            }
         }
         catch (DataException /* dex */)
         {
              ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
         }
         return View(productView);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(service.GetByID((int)id));
            
            /*Product product = unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(EntityToDTO(product));
            }
             */
             
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
        
        public ActionResult ProductSearch(string name)
        {
            return PartialView(service.Get(a => a.Name.Contains(name)));
        }
         
    }
}
