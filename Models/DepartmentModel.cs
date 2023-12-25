namespace TestProject.Models
{
    public class DepartmentModel
    {
        public List<DepartmentDetail> DepartmentDetailList { get; set; }
        public DepartmentDetail DepartmentDetails { get; set; }
    }
    public class DepartmentDetail
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public string DeptNo { get; set; }
        public string Email { get; set; }
        public int SecID { get; set; }
    }
}
