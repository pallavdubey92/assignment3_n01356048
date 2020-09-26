using System.Web.Mvc;

namespace SchoolProject.Controllers
{
    public class StudentsController : Controller
    {
        private StudentsDataController dataController = new StudentsDataController();

        /// <summary>
        /// GET: /Students
        /// </summary>
        /// <returns>
        /// view to display list of all students
        /// </returns>
        public ActionResult Index()
        {
            var students = dataController.GetAll();
            return View(students);
        }

        /// <summary>
        /// GET: /students/show/{id}
        /// </summary>
        /// <param name="id">int id of student to show</param>
        /// <returns>
        /// returns view to display details of a student
        /// </returns>
        public ActionResult Show(int id)
        {
            var student = dataController.GetById(id);
            return View(student);
        }
    }
}
