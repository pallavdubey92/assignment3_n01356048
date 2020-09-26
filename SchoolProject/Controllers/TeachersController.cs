﻿using System.Web.Mvc;

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
        /// <returns>
        /// view to display list of teachers
        /// </returns>
        public ActionResult Index(
            string nameSearchKey,
            decimal minSalary = 0,
            decimal maxSalary = 0)
        {
            var teachers = dataController.GetAll(nameSearchKey, minSalary, maxSalary);
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
    }
}