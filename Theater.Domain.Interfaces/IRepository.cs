using System;
using System.Collections.Generic;
using System.Text;

namespace Theater.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        T Get(int id);
        IEnumerable<T> GetList();
        void Update(T item);
        void Delete(int id);
    }
}
