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

        private Order DTOToEntity(OrderView orderView)
        {
            Order order = new Order();
                order.ManagerID = orderView.ManagerID;
                order.ProductID = orderView.ProductID;
                order.CustomerID = orderView.CustomerID;
                order.Price = orderView.Price;
                order.OrderDate = orderView.OrderDate;
                order.ReportDate = orderView.OrderDate;
            return order;
        }

        private Order DTOToEntityFull(OrderView orderView)
        {
            Order order = unitOfWork.OrderRepository.GetByID(orderView.Id);
            order.CustomerID=orderView.CustomerID;
            order.ProductID=orderView.ProductID;
            order.ManagerID=orderView.ManagerID;
            order.Price=orderView.Price;
            order.OrderDate = orderView.OrderDate;
            return order;
        }
         

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

          
        public virtual void Insert(OrderView orderView)
        {
            List<OrderView> allOrdersView = Get(a=>(a.ManagerID==orderView.ManagerID)
                &&(a.CustomerID==orderView.CustomerID)&&(a.ProductID==orderView.ProductID)&&(a.Price==orderView.Price)
                &&(a.OrderDate==orderView.OrderDate));
            bool isNew=true;

            if (allOrdersView.Count>0)
                {
                    isNew=false;
                }
            if (isNew)
            {
                Order order=DTOToEntity(orderView);
                unitOfWork.OrderRepository.Insert(order);
                unitOfWork.Save();
            }
        }
        

        public virtual void Delete(int id)
        {
            unitOfWork.OrderRepository.Delete(unitOfWork.OrderRepository.GetByID(id));
            unitOfWork.Save();
        }

        public virtual void Delete(ProductView entityViewToDelete)
        {
            Delete(entityViewToDelete.Id);
        }

        public virtual void Update(OrderView entityToUpdate)
        {
            List<OrderView> allOrdersView = Get(a=>(a.ManagerID==entityToUpdate.ManagerID)
                &&(a.CustomerID==entityToUpdate.CustomerID)&&(a.ProductID==entityToUpdate.ProductID)&&(a.Price==entityToUpdate.Price)
                &&(a.OrderDate==entityToUpdate.OrderDate));
            bool isNew=true;

            if (allOrdersView.Count>0)
                {
                    isNew=false;
                }
            if (isNew)
            {
                unitOfWork.OrderRepository.Update(DTOToEntityFull(entityToUpdate));
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
