using System.Collections.Generic;
using System.Threading.Tasks;

namespace Theater.Services.Interfaces
{
    public interface IBaseService<TDTO> where TDTO : class
    {
        Task<bool> CreateAsync(TDTO dto);
        Task<TDTO> GetByIdAsync(int id);
        Task<IEnumerable<TDTO>> GetAllAsync();
        Task<bool> UpdateAsync(TDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
