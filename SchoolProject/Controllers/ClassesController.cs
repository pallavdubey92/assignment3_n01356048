using System.Web.Mvc;

namespace SchoolProject.Controllers
{
    public class ClassesController : Controller
    {
        private ClassesDataController dataController = new ClassesDataController();

        /// <summary>
        /// GET: /classes
        /// </summary>
        /// <returns>
        /// returns view to display list of all classes
        /// </returns>
        public ActionResult Index()
        {
            var classes = dataController.GetAll();
            return View(classes);
        }

        /// <summary>
        /// GET: /classes/show/{id}
        /// </summary>
        /// <param name="id">int id of class to view</param>
        /// <returns>
        /// returns view to display details of a class
        /// </returns>
        public ActionResult Show(int id)
        {
            var classModel = dataController.GetById(id);
            return View(classModel);
        }
    }
}