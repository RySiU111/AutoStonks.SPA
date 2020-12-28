using System.Threading.Tasks;
using AutoStonks.SPA.Models;

namespace AutoStonks.SPA.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<User>> Register(User user);
        Task<ServiceResponse<User>> Login(User credentials);
        Task<User> GetUser();
        Task Logout();
    }
}