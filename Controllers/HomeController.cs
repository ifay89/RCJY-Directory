using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestProject.DataDB;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Search(string keyword)
        {
            
            RcjyDBContext rcjyDBContext = new RcjyDBContext();
            var searchResults = rcjyDBContext.EmpData
                .Where(e => e.FullName.Contains(keyword) || e.JobTitle.Contains(keyword) ||
                rcjyDBContext.Departments.Any(d => d.DeptID == e.Department && d.DeptName.Contains(keyword)) || 
                rcjyDBContext.Sections.Any( s => s.SecID == e.SecID && s.SecName.Contains(keyword))
                ).ToList();

            EmployeeModel employeeModel = new EmployeeModel
            {
                EmplDetailList = searchResults.Select(item => new EmplDetail
                {
                    UserID = item.UserID,
                    Password = item.Password,
                    FullName = item.FullName,
                    City = item.City,
                    Department = item.Department,
                    CISCO = item.CISCO,
                    Email = item.Email,
                    EmpNo = item.EmpNo,
                    JobTitle = item.JobTitle,
                    SecID = item.SecID,
                    RoleID = item.RoleID
                }).ToList()
            };

            return View("Index", employeeModel);
        }

        public IActionResult Index()
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmplDetailList = new List<EmplDetail>();

            RcjyDBContext rcjyDBContext = new RcjyDBContext();
            var data = rcjyDBContext.EmpData.ToList();

            foreach (var item in data)
            {
                employeeModel.EmplDetailList.Add(new EmplDetail
                {
                    UserID = item.UserID,
                    Password = item.Password,
                    FullName = item.FullName,
                    City = item.City,
                    Department = item.Department,
                    CISCO = item.CISCO,
                    Email = item.Email,
                    EmpNo = item.EmpNo,
                    JobTitle = item.JobTitle,
                    SecID = item.SecID,
                    RoleID = item.RoleID,
                    HidePhoneNumber = item.HidePhoneNumber
                });
            }
            return View(employeeModel);
        }

        [HttpPost]
        public IActionResult UpdateHidePhoneNumber(int userId, bool hidePhoneNumber)
        {
            try
            {
                RcjyDBContext rcjyDBContext = new RcjyDBContext();
                var user = rcjyDBContext.EmpData.FirstOrDefault(e => e.UserID == userId);

                if (user != null)
                {
                    user.HidePhoneNumber = hidePhoneNumber;

                    rcjyDBContext.SaveChanges();

                    return Json(new { success = true, message = "Hide phone number updated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Employee not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating hide phone number." });
            }
        }



        public IActionResult EmployeeInfCard(int UserId)
        {
            RcjyDBContext rcjyDBContext = new RcjyDBContext();
            var employee = rcjyDBContext.EmpData.FirstOrDefault(e => e.UserID == UserId);
            
            if (employee == null)
            {
                // Employee not found
                return NotFound();
            }

            var DeptID = employee.Department;
            var department = rcjyDBContext.Departments.FirstOrDefault(e => e.DeptID == DeptID);
            EmployeeModel employeeModel = new EmployeeModel
            {
                EmployeeDetail = new EmplDetail
                {
                    UserID = employee.UserID,
                    Password = employee.Password,
                    FullName = employee.FullName,
                    City = employee.City,
                    DeptName = department.DeptName,
                    CISCO = employee.CISCO,
                    Email = employee.Email,
                    EmpNo = employee.EmpNo,
                    JobTitle = employee.JobTitle,
                    SecID = employee.SecID,
                    RoleID = employee.RoleID,
                    HidePhoneNumber = employee.HidePhoneNumber
                }
            };

            ViewData["EmployeeData"] = employee;

            return View(employeeModel);
        }
        public IActionResult Profile()
        {
            // Retrieve user information from TempData or another storage mechanism
            var userId = TempData["UserId"]?.ToString();
            var userName = TempData["UserName"]?.ToString();
            var userJobTitle = TempData["UserJobTitle"]?.ToString();
            var userEmail = TempData["UserEmail"]?.ToString();
            var userCity = TempData["UserCity"]?.ToString();
            var userCISCO = TempData["UserCISCO"]?.ToString();
            var userDepartment = TempData["UserDepartment"]?.ToString();
            var userPhoneNumber = TempData["UserPhoneNumber"]?.ToString();
            var hidePhoneNumber = TempData["HidePhoneNumber"] as bool? ?? false; // Default to false if not found
            // Retrieve more properties as needed

            // Pass user information to the view
            ViewBag.UserId = userId;
            ViewBag.UserName = userName;
            ViewBag.UserJobTitle = userJobTitle;
            ViewBag.UserEmail = userEmail;
            ViewBag.UserCity = userCity;
            ViewBag.UserCISCO = userCISCO;
            ViewBag.UserDepartment = userDepartment;
            ViewBag.UserPhoneNumber = userPhoneNumber;
            ViewBag.HidePhoneNumber = hidePhoneNumber; // Pass HidePhoneNumber to the view

            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Departments()
        {
            DepartmentModel departmentModel = new DepartmentModel();
            departmentModel.DepartmentDetailList = new List<DepartmentDetail>();
            RcjyDBContext rcjyDBContext = new RcjyDBContext();
            var departmentData = rcjyDBContext.Departments.ToList();
            foreach (var item in departmentData)
            {
                departmentModel.DepartmentDetailList.Add(new DepartmentDetail
                {
                    DeptID = item.DeptID,
                    DeptName = item.DeptName,
                    DeptNo = item.DeptNo,
                    Email = item.Email,
                    SecID = item.SecID
                });
            }
            return View(departmentModel);
        }

        public IActionResult SearchDepartments(string keyword)
        {
            RcjyDBContext rcjyDBContext = new RcjyDBContext();
            var searchResults = rcjyDBContext.Departments
                .Where(d => d.DeptName.Contains(keyword) || rcjyDBContext.Sections.Any(s => s.SecID == d.SecID && s.SecName.Contains(keyword))
                ).ToList();

            DepartmentModel departmentModel = new DepartmentModel
            {
                DepartmentDetailList = searchResults.Select(item => new DepartmentDetail
                {
                    DeptID = item.DeptID,
                    DeptName = item.DeptName,
                    DeptNo = item.DeptNo,
                    Email = item.Email,
                    SecID = item.SecID
                }).ToList()
            };

            return View("Departments", departmentModel);
        }


        [HttpPost]
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}