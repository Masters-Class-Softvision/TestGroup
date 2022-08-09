using GarconLibrary.DBFramework;
using GarconLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarconLibrary.Repository
{
    public class EFRepository : RepositoryBase, IEFRepository
    {
        public GarconEntities Context { get; set; }

        private bool IsDisposed = false;

        /// <summary>
        /// Create an instance of an EDM (Entity Data Model)
        /// </summary>
        /// <param name="context"></param>
        public EFRepository()
        {
            DbContext obj = new DbContext();
            Context = obj.Context;
        }

        //return the EntitySetName 
        protected string GetEntitySetName<T>()
        {
            return String.Format("{0}", typeof(T).Name);
        }

        #region IGenericRepository Members

        /// <returns>returns all EDM of type T into a querable list</returns>
        public override IQueryable<T> List<T>()
        {
            return Context.CreateQuery<T>(GetEntitySetName<T>());
        }

        /// <returns>returns T from queryable list of specific id </returns>
        public override T Get<T>(int id)
        {
            return List<T>().FirstOrDefault<T>(CreateGetExpression<T>(id));
        }
        public override T Get<T>(long id)
        {
            return List<T>().FirstOrDefault<T>(CreateGetExpression<T>(id));
        }

        /// <summary>
        /// Adds a new record of type T to T and saves it.
        /// </summary>
        public override void Create<T>(T entityToCreate)
        {
            var entitySetName = GetEntitySetName<T>();
            Context.AddObject(entitySetName, entityToCreate);
            Context.SaveChanges();
        }

        /// <summary>
        /// Edits record of id in type T
        /// </summary>
        public override void Edit<T>(T entityToEdit)
        {
            var orginalEntity = Get<T>(GetKeyPropertyValue<T>(entityToEdit));
            Context.ApplyPropertyChanges(GetEntitySetName<T>(), entityToEdit);
            Context.SaveChanges();
        }

        /// <summary>
        /// Delets record of id in type T
        /// </summary>
        public override void Delete<T>(T entityToDelete)
        {
            var orginalEntity = Get<T>(GetKeyPropertyValue<T>(entityToDelete));
            Context.DeleteObject(orginalEntity);
            Context.SaveChanges();
        }
        #endregion

        //Call Dispose to free resources explicitly
        public void Dispose()
        {
            //Pass true in dispose method to clean managed resources too and say GC to skip finalize in next line.
            Dispose(true);
            //If dispose is called already then say GC to skip finalize on this instance.
            GC.SuppressFinalize(this);
        }
        ~EFRepository()
        {
            //Pass false as param because no need to free managed resources when you call finalize it will be done
            //by GC itself as its work of finalize to manage managed resources.
            Dispose(false);
        }

        protected virtual void Dispose(bool disposedStatus)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                // Released unmanaged Resources
                if (disposedStatus)
                {
                    this.Context.Dispose();
                }
            }
        }
    }
}
