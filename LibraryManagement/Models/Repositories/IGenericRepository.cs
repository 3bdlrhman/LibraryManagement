using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> List();
        T Find(int Id);
        void Add(T itemToAdd);
        void Delete(int Id);
        void Edit(T itemToUpdate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}
