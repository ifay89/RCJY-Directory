using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.DataDB;
using RCJY_Project.Services;
using RCJY_Project.Controllers;

namespace RCJY_Project.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        
        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            RcjyDBContext rcjyDBContext = new RcjyDBContext();
            var user = await rcjyDBContext.EmpData.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);

            if (user != null)
            {
                // تم العثور على المستخدم، يمكنك هنا تحديد دور المستخدم وإعادة القيمة.
                return true;
            }

            return false;
        }
    }
}