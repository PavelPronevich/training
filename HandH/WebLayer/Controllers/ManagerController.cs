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
    public class ManagerController : Controller
    {
        //private OrdersContext db = new OrdersContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: /Manager/
        public ViewResult Index()
        {
            IEnumerable<Manager> managers = unitOfWork.ManagerRepository.Get();
            List<ManagerView> managersv = new List<ManagerView>();
            foreach (var item in managers)
            {
                managersv.Add(new ManagerView{Id=item.Id,Name=item.ManagerSurname});
            }
            return View(managersv);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Manager manager = unitOfWork.ManagerRepository.GetByID(id);
            ManagerView managerView = new ManagerView() { Id = manager.Id, Name = manager.ManagerSurname};
            return View(managerView);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name")] ManagerView managerView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ManagerRepository.Insert(new Manager(){ManagerSurname=managerView.Name});
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
         return View(managerView);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = unitOfWork.ManagerRepository.GetByID(id);
            ManagerView managerView = new ManagerView() { Id = manager.Id, Name = manager.ManagerSurname};
            return View(managerView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] ManagerView managerView)
        {
            try
         {
            if (ModelState.IsValid)
            {
                Manager manager = new Manager() { Id = managerView.Id, ManagerSurname = managerView.Name };
               unitOfWork.ManagerRepository.Update(manager);
               unitOfWork.Save();
               return RedirectToAction("Index");
            }
         }
         catch (DataException /* dex */)
         {
              ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
         }
         return View(managerView);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = unitOfWork.ManagerRepository.GetByID(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            else
            {
                ManagerView managerView=new ManagerView(){Id=manager.Id,Name=manager.ManagerSurname};
                return View(managerView);
            }
             
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
         Manager manager = unitOfWork.ManagerRepository.GetByID(id);
         unitOfWork.ManagerRepository.Delete(id);
         unitOfWork.Save();
         return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
         base.Dispose(disposing);
            
        }


        public ActionResult ManagerSearch(string name)
        {
            var allManagers = unitOfWork.ManagerRepository.Get(a => a.ManagerSurname.Contains(name));
            List<ManagerView> managersv = new List<ManagerView>();
            foreach (var item in allManagers)
            {
                managersv.Add(new ManagerView { Id = item.Id, Name = item.ManagerSurname });
            }
            return PartialView(managersv);
        }
         
    }
}
