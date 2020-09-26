using System;

namespace SchoolProject.Models
{
    public class ClassModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}