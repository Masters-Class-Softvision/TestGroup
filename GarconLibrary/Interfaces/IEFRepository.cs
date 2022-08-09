using GarconLibrary.DBFramework;
using System.Linq;

namespace GarconLibrary.Interfaces
{
    public interface IEFRepository
    {
        GarconEntities Context { get; set; }
        void Create<T>(T entityToCreate) where T : class;
        void Delete<T>(T entityToDelete) where T : class;
        void Dispose();
        void Edit<T>(T entityToEdit) where T : class;
        T Get<T>(int id) where T : class;
        T Get<T>(long id) where T : class;
        IQueryable<T> List<T>() where T : class;
    }
}