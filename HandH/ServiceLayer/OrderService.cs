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
    public class OrderService: IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        CustomerService customerService = new CustomerService();
        ManagerService managerService = new ManagerService();
        ProductService productService = new ProductService();

        private OrderView EntityToDTO(Order order)
        {
            return (new OrderView 
            {
                Id = order.Id, 
                CustomerID= order.CustomerID, 
                ManagerID=order.ManagerID, 
                ProductID=order.ProductID,
                OrderDate=order.OrderDate,
                Price=order.Price,
                CustomerName= customerService.GetByID(order.CustomerID).Name,
                ProductName=productService.GetByID(order.ProductID).Name,
                ManagerName=managerService.GetByID(order.ManagerID).Name
            });
        }
        
        private IList<OrderView> EntityToDTO(IEnumerable<Order> orders)
        {
            List<OrderView> ordersv=new List<OrderView>();
            foreach (var item in orders)
            {
                ordersv.Add(EntityToDTO(item));
            }
            return ordersv;
        }

        
        /*private Order DTOToEntity(OrderView orderView)
        {

            return new Product() {ProductName = productView.Name };
        }
        */
         
        /*
        private Product DTOToEntityFull(ProductView productView)
        {
            Product _product = unitOfWork.ProductRepository.GetByID(productView.Id);
            _product.ProductName = productView.Name;
            return _product;
        }
         */

        public virtual List<OrderView> Get(Func<OrderView, bool> filter = null,
            Func<IEnumerable<OrderView>, IOrderedEnumerable<OrderView>> orderBy = null)
        {
            IEnumerable<Order> allOrders = unitOfWork.OrderRepository.Get();
            IEnumerable<OrderView> allOrderView = EntityToDTO(allOrders);
            if (filter != null)
            {
                allOrderView=allOrderView.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(allOrderView).ToList();
            }
            else
            {
                return allOrderView.ToList();
            }
        }
        
        public virtual OrderView GetByID(int id)
        {
            return EntityToDTO(unitOfWork.OrderRepository.GetByID(id));
        }

        /*
        
        public virtual void Insert(OrderView orderView)
        {
            IEnumerable<OrderView> allOrdersView = Get();
            bool isNew=true;
            List<OrderView> ordersView=Get(a=>(a.ManagerName==orderView.ManagerName)&&(a.CustomerName==orderView.CustomerName)&&
                (a.ProductName==orderView.ProductName)&&(a.OrderDate==orderView.OrderDate)&&(a.Price==orderView.Price));
            
            if (ordersView.Count==0)
                {
                    isNew=false;
                }

            //foreach (var item in allOrdersView)
            //{
            //    if ((item.ManagerName==orderView.ManagerName)&&(item.CustomerName==orderView.CustomerName)
            //        &&(item.ProductName==orderView.ProductName)&&(item.OrderDate==orderView.OrderDate)
            //        &&(item.Price==orderView.Price))
            //    {
            //        isNew=false;
            //        break;
            //    }
            //}
          

            if (isNew)
            {
                if (managerService.Get(a=>a.))
                unitOfWork.ProductRepository.Insert(DTOToEntity(productView));
                unitOfWork.Save();
            }
        }

        public virtual void Delete(int id)
        {
            unitOfWork.ProductRepository.Delete(unitOfWork.ProductRepository.GetByID(id));
            unitOfWork.Save();
        }

        public virtual void Delete(ProductView entityViewToDelete)
        {
            Delete(entityViewToDelete.Id);
        }
        public virtual void Update(ProductView entityToUpdate)
        {
            IEnumerable<ProductView> allProductsView = Get();
            bool isNew=true;
            foreach (var item in allProductsView)
            {
                if (item.Name == entityToUpdate.Name)
                {
                    isNew=false;
                    break;
                }
            }
            if (isNew)
            {
                unitOfWork.ProductRepository.Update(DTOToEntityFull(entityToUpdate));
                unitOfWork.Save();
            }
        }
        */

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
                    customerService.Dispose();
                    managerService.Dispose();
                    productService.Dispose();
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
