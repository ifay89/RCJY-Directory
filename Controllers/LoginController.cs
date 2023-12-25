using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestProject.DataDB;
using RCJY_Project.Services;
using RCJY_Project.Controllers;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RCJY_Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly RCJY_Project.Services.IAuthenticationService _authenticationService; 

        public LoginController(ILogger<LoginController> logger, RCJY_Project.Services.IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            try
            {
                var isSuccess = await _authenticationService.AuthenticateUserAsync(email, password);

                if (isSuccess)
                {
                    RcjyDBContext rcjyDBContext = new RcjyDBContext();
                    var user = await rcjyDBContext.EmpData.FirstOrDefaultAsync(e => e.Email == email);
                    if (user != null)
                    {
                        var DeptID = user.Department;
                        var Department = await rcjyDBContext.Departments.FirstOrDefaultAsync(e => e.DeptID == DeptID);
                        
                        TempData["UserId"] = user.UserID;
                        TempData["UserName"] = user.FullName;
                        TempData["UserJobTitle"] = user.JobTitle;
                        TempData["UserEmail"] = user.Email;
                        TempData["UserCity"] = user.City; 
                        TempData["UserCISCO"] = user.CISCO; 
                        TempData["UserDepartment"] = Department.DeptName; 
                        if (!user.HidePhoneNumber)
                        {
                            TempData["UserPhoneNumber"] = user.EmpNo; 
                        }
                        TempData["HidePhoneNumber"] = user.HidePhoneNumber;

                        if (user.RoleID == 1)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (user.RoleID == 2)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ViewBag.Username = string.Format("Login Failed for user: {0}", email);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user authentication.");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
