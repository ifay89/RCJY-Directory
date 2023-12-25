namespace TestProject.Models
{
    public class EmployeeModel
    {
        public List<EmplDetail> EmplDetailList { get; set; }
        public EmplDetail EmployeeDetail { get; set; }
    }
    public class EmplDetail
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public int Department { get; set; }
        public string DeptName { get; set; }
        public string CISCO { get; set; }
        public string Email { get; set; }
        public int EmpNo { get; set; }
        public string JobTitle { get; set; }
        public int SecID { get; set; }
        public int RoleID { get; set; }
        public bool HidePhoneNumber { get; set; }
    }
}
