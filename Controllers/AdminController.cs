using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestProject.DataDB;
using TestProject.Models;

namespace RCJY_Project.Controllers
{
    public class AdminController : Controller
    {
        // Admin View
        public IActionResult Search(string keyword)
        {

            RcjyDBContext rcjyDBContext = new RcjyDBContext();

            var searchResults = rcjyDBContext.EmpData
                .Where(e => e.FullName.Contains(keyword) || e.JobTitle.Contains(keyword) ||
                rcjyDBContext.Departments.Any(d => d.DeptID == e.Department && d.DeptName.Contains(keyword)) ||
                rcjyDBContext.Sections.Any(s => s.SecID == e.SecID && s.SecName.Contains(keyword))
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
                    RoleID = item.RoleID
                });
            }
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

            return View();
        }

       


        public IActionResult EmployeeInfCard(int UserId)
        {
            RcjyDBContext rcjyDBContext = new RcjyDBContext();
            var employee = rcjyDBContext.EmpData.FirstOrDefault(e => e.UserID == UserId);

            if (employee == null)
            {
               
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
                    RoleID = employee.RoleID
                }
            };

            ViewData["EmployeeData"] = employee;

            return View(employeeModel);
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
        
        ///////////

        public IActionResult DeleteEmployee(int userId)
        {
            RcjyDBContext rcjyDBContext = new RcjyDBContext();
            var employee = rcjyDBContext.EmpData.FirstOrDefault(e => e.UserID == userId);
            try
            {
                if (employee != null)
                {
                    rcjyDBContext.EmpData.Remove(employee);
                    rcjyDBContext.SaveChanges();
                    return Json(new { success = true, message = "Employee deleted successfully." });
                }
                else
                {
                    // Employee not found
                    return Json(new { success = false, message = "Employee not found." });
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                return Json(new { success = false, message = "An error occurred while deleting the employee." });
            }
        }

        ///////////
        
        [HttpPost]
        public IActionResult UpdateEmployee(int userId, string city, string cisco, string jobTitle)
        {
            try
            {
                RcjyDBContext rcjyDBContext = new RcjyDBContext();
                var user = rcjyDBContext.EmpData.FirstOrDefault(e => e.UserID == userId);

                if (user != null)
                {
                    user.City = city;
                    user.CISCO = cisco;
                    user.JobTitle = jobTitle;


                    rcjyDBContext.SaveChanges();

                    return Json(new { success = true, message = "تم حفظ بيانات الموظف بنجاح." });
                }
                else
                {
                    return Json(new { success = false, message = "لم يتم العثور على الموظف." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء تحديث بيانات الموظف." });
            }
        }
        public IActionResult AddEmployee()
        {
            return View();
        }

    }

}