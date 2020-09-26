using System.Collections.Generic;

namespace SchoolProject.Models
{
    public class ShowTeacherModel
    {
        public Teacher Teacher { get; set; }
        public List<ClassModel> Classes { get; set; }
    }
}