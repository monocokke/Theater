using System;
using System.Collections.Generic;
using System.Text;

namespace Theater.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        IEnumerable<T> GetList();
        T Get(int id);
        void Delete(int id);
    }
}
