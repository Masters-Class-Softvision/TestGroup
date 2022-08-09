using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarconLibrary.Interfaces
{
    public interface IRepositoryBase
    {
        T Get<T>(long id) where T : class;
        T Get<T>(int id) where T : class;
        IQueryable<T> List<T>() where T : class;
        void Create<T>(T entityToCreate) where T : class;
        void Delete<T>(T entityToDelete) where T : class;
        void Edit<T>(T entityToEdit) where T : class;
    }
}
