using System;

namespace SchoolProject.Models
{
    public class AddTeacherModel
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string EmpNo { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        public bool Validate()
        {
            if (FName == null || FName == "")
            {
                return false;
            }

            if (LName == null || LName == "")
            {
                return false;
            }

            if (EmpNo == null || EmpNo == "")
            {
                return false;
            }

            if (HireDate > DateTime.Now)
            {
                return false;
            }

            if (Salary < 1)
            {
                return false;
            }

            return true;
        }
    }
}