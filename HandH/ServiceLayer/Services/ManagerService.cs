using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOModels;
using DBLayer;

namespace ServiceLayer
{
    public class ManagerService: IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private ManagerView EntityToDTO(Manager manager)
        {
            return (new ManagerView { Id = manager.Id, Name = manager.ManagerSurname });
        }
        private IList<ManagerView> EntityToDTO(IEnumerable<Manager> managers)
        {
            List<ManagerView> managersv=new List<ManagerView>();
            foreach (var item in managers)
            {
                managersv.Add(EntityToDTO(item));
            }
            return managersv;
        }
        private Manager DTOToEntity(ManagerView managerView)
        {
            return new Manager() { ManagerSurname = managerView.Name };
        }
        private Manager DTOToEntityFull(ManagerView managerView)
        {
            Manager _manager=unitOfWork.ManagerRepository.GetByID(managerView.Id);
            _manager.ManagerSurname = managerView.Name;
            return _manager;
        }

        public virtual List<ManagerView> Get(Func<ManagerView, bool> filter = null,
            Func<IEnumerable<ManagerView>, IOrderedEnumerable<ManagerView>> orderBy = null)
        {
            IEnumerable<Manager> allManagers=unitOfWork.ManagerRepository.Get();
            IEnumerable<ManagerView> allManagerView = EntityToDTO(allManagers);
            if (filter != null)
            {
                allManagerView=allManagerView.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(allManagerView).ToList();
            }
            else
            {
                return allManagerView.ToList();
            }
        }

        public virtual ManagerView GetByID(int id)
        {
            return EntityToDTO(unitOfWork.ManagerRepository.GetByID(id));
        }


        public virtual void Insert(ManagerView managerView)
        {
            IEnumerable<ManagerView> allManagersView = Get();
            bool isNew=true;
            foreach (var item in allManagersView)
            {
                if (item.Name==managerView.Name)
                {
                    isNew=false;
                    break;
                }
            }
            if (isNew)
            {
                unitOfWork.ManagerRepository.Insert(DTOToEntity(managerView));
                unitOfWork.Save();
            }
        }

        public virtual void Delete(int id)
        {
            unitOfWork.ManagerRepository.Delete(unitOfWork.ManagerRepository.GetByID(id));
            unitOfWork.Save();
        }

        public virtual void Delete(ManagerView entityViewToDelete)
        {
            Delete(entityViewToDelete.Id);
        }
        public virtual void Update(ManagerView entityViewToUpdate)
        {
            IEnumerable<ManagerView> allManagersView = Get();
            bool isNew=true;
            foreach (var item in allManagersView)
            {
                if (item.Name == entityViewToUpdate.Name)
                {
                    isNew=false;
                    break;
                }
            }
            if (isNew)
            {
                unitOfWork.ManagerRepository.Update(DTOToEntityFull(entityViewToUpdate));
                unitOfWork.Save();
            }
            
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
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
