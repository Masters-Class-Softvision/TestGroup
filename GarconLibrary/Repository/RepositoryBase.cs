using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using GarconLibrary.Interfaces;

namespace GarconLibrary.Repository
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        //id property of our database table we'll be obtaining.
        const string keyPropertyName = "ID";

        /// <summary>
        /// Using System.Linq.Expressions to obtain lambda 
        /// </summary>
        /// <typeparam name="T">generic class where the class is unkown</typeparam>
        /// <param name="id">id of T</param>
        /// <returns>lambda</returns>
        protected Expression<Func<T, bool>> CreateGetExpression<T>(int id)
        {
            ParameterExpression e = Expression.Parameter(typeof(T), "e");
            PropertyInfo propinfo = typeof(T).GetProperty(keyPropertyName);
            MemberExpression m = Expression.MakeMemberAccess(e, propinfo);
            ConstantExpression c = Expression.Constant(id, typeof(int));
            BinaryExpression b = Expression.Equal(m, c);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(b, e);
            return lambda;
        }
        protected Expression<Func<T, bool>> CreateGetExpression<T>(long id)
        {
            ParameterExpression e = Expression.Parameter(typeof(T), "e");
            PropertyInfo propinfo = typeof(T).GetProperty(keyPropertyName);
            MemberExpression m = Expression.MakeMemberAccess(e, propinfo);
            ConstantExpression c = Expression.Constant(id, typeof(long));
            BinaryExpression b = Expression.Equal(m, c);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(b, e);
            return lambda;
        }

        //protected int GetKeyPropertyValue<T>(object entity)
        //{
        //    return (int)typeof(T).GetProperty(keyPropertyName).GetValue(entity, null);
        //}
        protected long GetKeyPropertyValue<T>(object entity)
        {
            return (long)typeof(T).GetProperty(keyPropertyName).GetValue(entity, null);
        }


        /// <summary>
        /// Inherits IRepository makes them abstract, so they can be used in EFRepository
        /// </summary>
        /// <typeparam name="T">generic class where the class is unknown</typeparam>
        /// <returns></returns>
        #region IGenericRepository Members
        public abstract IQueryable<T> List<T>() where T : class;
        public abstract T Get<T>(int id) where T : class;
        public abstract T Get<T>(long id) where T : class;
        public abstract void Create<T>(T entityToCreate) where T : class;
        public abstract void Edit<T>(T entityToEdit) where T : class;
        public abstract void Delete<T>(T entityToDelete) where T : class;

        #endregion
    }
}
