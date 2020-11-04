using System.Threading.Tasks;
using Theater.Domain.Core.DTO;

namespace Theater.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(UserDTO userDTO);
        Task<bool> Login(UserDTO userDTO);
        Task Logout();
    }
}
