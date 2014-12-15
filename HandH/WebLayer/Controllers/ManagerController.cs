﻿using System;
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
    public class ManagerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        private ManagerView EntityToDTO(Manager manager)
        {
            return (new ManagerView { Id = manager.Id, Name = manager.ManagerSurname });
        }
        private List<ManagerView> EntityToDTO(IEnumerable<Manager> managers)
        {
            List<ManagerView> managersv = new List<ManagerView>();
            foreach (var item in managers)
            {
                managersv.Add(EntityToDTO(item));
            }
            return managersv;
        }


        public ViewResult Index()
        {
            IEnumerable<Manager> managers = unitOfWork.ManagerRepository.Get();
            return View(EntityToDTO(managers));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(EntityToDTO(unitOfWork.ManagerRepository.GetByID(id)));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        private Manager DTOToEntity(ManagerView managerView)
        {
            return new Manager() { ManagerSurname = managerView.Name };
        }
        private Manager DTOToEntityFull(ManagerView managerView)
        {
            return new Manager() {Id=managerView.Id, ManagerSurname = managerView.Name };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Name")] ManagerView managerView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ManagerRepository.Insert(DTOToEntity(managerView));
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

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(EntityToDTO(unitOfWork.ManagerRepository.GetByID(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Name")] ManagerView managerView)
        {
            try
         {
            if (ModelState.IsValid)
            {
               unitOfWork.ManagerRepository.Update(DTOToEntityFull(managerView));
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

        [Authorize(Roles = "admin")]
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
                return View(EntityToDTO(manager));
            }
             
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
         //Manager manager = unitOfWork.ManagerRepository.GetByID(id);
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
