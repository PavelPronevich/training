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
    public class CustomerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        private CustomerView EntityToDTO(Customer customer)
        {
            return (new CustomerView { Id = customer.Id, Name = customer.CustomerName });
        }
        private List<CustomerView> EntityToDTO(IEnumerable<Customer> customers)
        {
            List<CustomerView> customersv = new List<CustomerView>();
            foreach (var item in customers)
            {
                customersv.Add(EntityToDTO(item));
            }
            return customersv;
        }


        public ViewResult Index()
        {
            IEnumerable<Customer> customers = unitOfWork.CustomerRepository.Get();
            return View(EntityToDTO(customers));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(EntityToDTO(unitOfWork.CustomerRepository.GetByID(id)));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        private Customer DTOToEntity(CustomerView customerView)
        {
            return new Customer() { CustomerName = customerView.Name };
        }
        private Customer DTOToEntityFull(CustomerView customerView)
        {
            return new Customer() {Id=customerView.Id, CustomerName = customerView.Name };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Name")] CustomerView customerView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CustomerRepository.Insert(DTOToEntity(customerView));
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
         return View(customerView);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(EntityToDTO(unitOfWork.CustomerRepository.GetByID(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Name")] CustomerView customerView)
        {
            try
         {
            if (ModelState.IsValid)
            {
               unitOfWork.CustomerRepository.Update(DTOToEntityFull(customerView));
               unitOfWork.Save();
               return RedirectToAction("Index");
            }
         }
         catch (DataException /* dex */)
         {
              ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
         }
         return View(customerView);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = unitOfWork.CustomerRepository.GetByID(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(EntityToDTO(customer));
            }
             
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
         //Customer customer = unitOfWork.CustomerRepository.GetByID(id);
         unitOfWork.CustomerRepository.Delete(id);
         unitOfWork.Save();
         return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
         base.Dispose(disposing);
            
        }


        public ActionResult CustomerSearch(string name)
        {
            var allCustomers = unitOfWork.CustomerRepository.Get(a => a.CustomerName.Contains(name));
            List<CustomerView> customersv = new List<CustomerView>();
            foreach (var item in allCustomers)
            {
                customersv.Add(new CustomerView { Id = item.Id, Name = item.CustomerName });
            }
            return PartialView(customersv);
        }
         
    }
}
