using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LibraryManagement.Models.Repositories
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T:class
    {
        private DbContext _db;
        IDbSet<T> entity;
        public EfGenericRepository(DbContext db)
        {
            _db = db;
            entity = _db.Set<T>();

        }
        public void Add(T itemToAdd)
        {
            entity.Add(itemToAdd);
        }

        public void Delete(int Id)
        {
            var entityToDelete = entity.Find(Id);
            _db.Entry<T>(entityToDelete).State = EntityState.Deleted;
        }

        public void Edit(T itemToUpdate)
        {
            entity.Attach(itemToUpdate);
            _db.Entry<T>(itemToUpdate).State = EntityState.Modified; 
        }

        public T Find(int Id)
        {
            return entity.Find(Id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return entity.FirstOrDefault(predicate);
        }



        public ICollection<T> List()
        {
            return entity.ToList();
        }
    }
}