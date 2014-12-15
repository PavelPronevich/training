using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer;

namespace RepositoryLayer
{
    public class UnitOfWork : IDisposable
    {
        private OrdersContext context = new OrdersContext();
        private GenericRepository<Manager> managerRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<Customer> customerRepository;
        private GenericRepository<Order> orderRepository;

        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }
        public GenericRepository<Customer> CustomerRepository
        {
            get
            {

                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }
        public GenericRepository<Product> ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }
        public GenericRepository<Manager> ManagerRepository
        {
            get
            {

                if (this.managerRepository == null)
                {
                    this.managerRepository = new GenericRepository<Manager>(context);
                }
                return managerRepository;
            }
        }
       
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
