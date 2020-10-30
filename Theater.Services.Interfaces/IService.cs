using System;
using System.Collections.Generic;
using System.Text;

namespace Theater.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        bool CreateItem(T item);

        T GetItem(int id);

        IEnumerable<T> GetItems();

        bool Delete(int id);
    }
}
