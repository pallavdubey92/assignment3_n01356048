using System;

namespace SchoolProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
    }
}