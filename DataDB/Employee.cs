using System;
using System.Collections.Generic;

#nullable disable

namespace TestProject.DataDB
{
    public partial class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public partial class Section
    {
        public int SecID { get; set; }
        public string SecName { get; set; }
        public int DeptID { get; set; }
    }

    public partial class Sector
    {
        public int SectID { get; set; }
        public string SectName { get; set; }
        public int DeptID { get; set; }
    }

    public partial class Department
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public string DeptNo { get; set; }
        public string Email { get; set; }
        public int SecID { get; set; }
    }

    public partial class EmpData
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public int Department { get; set; }
        public string CISCO { get; set; }
        public string Email { get; set; }
        public int EmpNo { get; set; }
        public string JobTitle { get; set; }
        public int SecID { get; set; }
        public int RoleID { get; set; }
        public bool HidePhoneNumber { get; set; }
    }

}
