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
    public class CustomerService : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private CustomerView EntityToDTO(Customer customer)
        {
            return (new CustomerView { Id = customer.Id, Name = customer.CustomerName });
        }
        private IList<CustomerView> EntityToDTO(IEnumerable<Customer> customers)
        {
            List<CustomerView> customersv = new List<CustomerView>();
            foreach (var item in customers)
            {
                customersv.Add(EntityToDTO(item));
            }
            return customersv;
        }
        private Customer DTOToEntity(CustomerView customerView)
        {
            return new Customer() { CustomerName = customerView.Name };
        }
        private Customer DTOToEntityFull(CustomerView customerView)
        {
            Customer _customer = unitOfWork.CustomerRepository.GetByID(customerView.Id);
            _customer.CustomerName = customerView.Name;
            return _customer;
        }

        public virtual List<CustomerView> Get(Func<CustomerView, bool> filter = null,
            Func<IEnumerable<CustomerView>, IOrderedEnumerable<CustomerView>> orderBy = null)
        {
            IEnumerable<Customer> allCustomers = unitOfWork.CustomerRepository.Get();
            IEnumerable<CustomerView> allCustomerView = EntityToDTO(allCustomers);
            if (filter != null)
            {
                allCustomerView = allCustomerView.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(allCustomerView).ToList();
            }
            else
            {
                return allCustomerView.ToList();
            }
        }

        public virtual CustomerView GetByID(int id)
        {
            return EntityToDTO(unitOfWork.CustomerRepository.GetByID(id));
        }


        public virtual void Insert(CustomerView customerView)
        {
            IEnumerable<CustomerView> allCustomersView = Get();
            bool isNew = true;
            foreach (var item in allCustomersView)
            {
                if (item.Name == customerView.Name)
                {
                    isNew = false;
                    break;
                }
            }
            if (isNew)
            {
                unitOfWork.CustomerRepository.Insert(DTOToEntity(customerView));
                unitOfWork.Save();
            }
        }

        public virtual void Delete(int id)
        {
            unitOfWork.CustomerRepository.Delete(unitOfWork.CustomerRepository.GetByID(id));
            unitOfWork.Save();
        }

        public virtual void Delete(CustomerView entityViewToDelete)
        {
            Delete(entityViewToDelete.Id);
        }
        public virtual void Update(CustomerView entityToUpdate)
        {
            IEnumerable<CustomerView> allCustomersView = Get();
            bool isNew = true;
            foreach (var item in allCustomersView)
            {
                if (item.Name == entityToUpdate.Name)
                {
                    isNew = false;
                    break;
                }
            }
            if (isNew)
            {
                unitOfWork.CustomerRepository.Update(DTOToEntityFull(entityToUpdate));
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
