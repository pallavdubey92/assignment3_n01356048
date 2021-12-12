using SchoolProject.Models;
using System;
using System.Web.Mvc;

namespace SchoolProject.Controllers
{
    public class TeachersController : Controller
    {
        private TeachersDataController dataController = new TeachersDataController();

        /// <summary>
        /// Get: /Teachers
        /// </summary>
        /// <param name="nameSearchKey">key to filter list by name</param>
        /// <param name="minSalary">to filter list by min salary (requires both min and max value)</param>
        /// <param name="maxSalary">to filter list by max salary (requires both min and max value)</param>
        /// <param name="startDate">to filter list by hire date, start date for the range</param>
        /// <param name="endDate">to filter list by hire date, end date for the range</param>
        /// <returns>
        /// view to display list of teachers
        /// </returns>
        public ActionResult Index(
            string nameSearchKey,
            decimal minSalary = 0,
            decimal maxSalary = 0,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var teachers = dataController.GetAll(nameSearchKey, minSalary, maxSalary, startDate, endDate);
            return View(teachers);
        }

        /// <summary>
        /// GET: /Teachers/Show/{id}
        /// </summary>
        /// <param name="id">int id of the teacher to show</param>
        /// <returns>
        /// view to display details of a teacher
        /// </returns>
        public ActionResult Show(int id)
        {
            var teacher = dataController.GetById(id);
            return View(teacher);
        }

        /// <summary>
        /// GET: /teachers/add
        /// </summary>
        /// <returns>
        /// returns view with form to add new teacher
        /// </returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// POST: /teachers/create
        /// </summary>
        /// <param name="newTeacher">details of new teacher</param>
        /// <returns>
        /// redirects to teacher list
        /// </returns>
        [HttpPost]
        public ActionResult Create(AddTeacherModel newTeacher)
        {
            dataController.Add(newTeacher);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: /teachers/update 
        /// </summary>
        /// <param name="id">int id of the teacher to be updated</param>
        /// <returns>
        /// View with update form
        /// </returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var showTeacher = dataController.GetById(id);

            var editTeacher = new AddTeacherModel();
            editTeacher.Id = showTeacher.Teacher.Id;
            editTeacher.FName = showTeacher.Teacher.Name.Split(' ')[0];
            editTeacher.LName = showTeacher.Teacher.Name.Split(' ')[1];
            editTeacher.EmpNo = showTeacher.Teacher.EmployeeNumber;
            editTeacher.HireDate = showTeacher.Teacher.HireDate;
            editTeacher.Salary = showTeacher.Teacher.Salary;

            return View(editTeacher);
        }

        /// <summary>
        /// GET: /teachers/delete
        /// </summary>
        /// <param name="id">id of the teacher to delete</param>
        /// <returns>redirects to teacher list</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            dataController.Delete(id);
            return RedirectToAction("Index");
        }
    }
}