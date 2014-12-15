using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var managers = unitOfWork.ManagerRepository.Get();
            foreach (var item in managers)
            {
                Console.WriteLine("{0} -- {1}",item.Id,item.ManagerSurname);
            }
            Console.ReadKey();

        }
    }
}
