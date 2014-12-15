using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBLayer;
using WebLayer.Models.DTOModels;
using RepositoryLayer;

namespace WebLayer.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        private ProductView EntityToDTO(Product product)
        {
            return (new ProductView { Id = product.Id, Name = product.ProductName });
        }
        private List<ProductView> EntityToDTO(IEnumerable<Product> products)
        {
            List<ProductView> productsv = new List<ProductView>();
            foreach (var item in products)
            {
                productsv.Add(EntityToDTO(item));
            }
            return productsv;
        }
        private Product DTOToEntity(ProductView productView)
        {
            return new Product() { ProductName = productView.Name };
        }
        private Product DTOToEntityFull(ProductView productView)
        {
            return new Product() { Id = productView.Id, ProductName = productView.Name };
        }


        public ViewResult Index()
        {
            return View(EntityToDTO(unitOfWork.ProductRepository.Get()));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(EntityToDTO(unitOfWork.ProductRepository.GetByID(id)));
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
                    unitOfWork.ProductRepository.Insert(DTOToEntity(productView));
                    unitOfWork.Save();
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
            return View(EntityToDTO(unitOfWork.ProductRepository.GetByID(id)));
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
               unitOfWork.ProductRepository.Update(DTOToEntityFull(productView));
               unitOfWork.Save();
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
            Product product = unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(EntityToDTO(product));
            }
             
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
         unitOfWork.ProductRepository.Delete(id);
         unitOfWork.Save();
         return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
            
        }


        public ActionResult ProductSearch(string name)
        {
            return PartialView(EntityToDTO(unitOfWork.ProductRepository.Get(a => a.ProductName.Contains(name))));
        }
         
    }
}
