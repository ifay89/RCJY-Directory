using System.Threading.Tasks;
namespace RCJY_Project.Services
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateUserAsync(string email, string password);
        // other authentication-related methods can be added here
    }
}
