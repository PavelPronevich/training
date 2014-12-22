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
    public class ProductService: IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private ProductView EntityToDTO(Product product)
        {
            return (new ProductView { Id = product.Id, Name = product.ProductName });
        }
        private IList<ProductView> EntityToDTO(IEnumerable<Product> products)
        {
            List<ProductView> productsv=new List<ProductView>();
            foreach (var item in products)
            {
                productsv.Add(EntityToDTO(item));
            }
            return productsv;
        }
        private Product DTOToEntity(ProductView productView)
        {
            return new Product() {ProductName = productView.Name };
        }
        private Product DTOToEntityFull(ProductView productView)
        {
            Product _product = unitOfWork.ProductRepository.GetByID(productView.Id);
            _product.ProductName = productView.Name;
            return _product;
        }

        public virtual List<ProductView> Get(Func<ProductView, bool> filter = null,
            Func<IEnumerable<ProductView>, IOrderedEnumerable<ProductView>> orderBy = null)
        {
            IEnumerable<Product> allProducts=unitOfWork.ProductRepository.Get();
            IEnumerable<ProductView> allProductView = EntityToDTO(allProducts);
            if (filter != null)
            {
                allProductView=allProductView.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(allProductView).ToList();
            }
            else
            {
                return allProductView.ToList();
            }
        }

        public virtual ProductView GetByID(int id)
        {
            return EntityToDTO(unitOfWork.ProductRepository.GetByID(id));
        }


        public virtual void Insert(ProductView productView)
        {
            IEnumerable<ProductView> allProductsView = Get();
            bool isNew=true;
            foreach (var item in allProductsView)
            {
                if (item.Name==productView.Name)
                {
                    isNew=false;
                    break;
                }
            }
            if (isNew)
            {
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
