using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SchoolProject.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsDataController : ApiController
    {
        private SchoolDbContext dbContext = new SchoolDbContext();

        /// <summary>
        /// GET: api/students
        /// returns list of all students
        /// </summary>
        /// <returns>
        /// <ArrayOfStudent>
        ///    <Student>
        ///        <EnrolDate>2018-06-18T00:00:00</EnrolDate>
        ///        <Id>1</Id>
        ///        <Name>Sarah Valdez</Name>
        ///        <StudentNumber>N1678</StudentNumber>
        ///    </Student>
        ///    <Student>
        ///        <EnrolDate>2018-08-02T00:00:00</EnrolDate>
        ///        <Id>2</Id>
        ///        <Name>Jennifer Faulkner</Name>
        ///        <StudentNumber>N1679</StudentNumber>
        ///    </Student>
        /// <ArrayOfStudent>
        /// </returns>
        [HttpGet]
        [Route("")]
        public List<Student> GetAll()
        {
            var students = new List<Student>();

            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM students";

            var result = cmd.ExecuteReader();

            while (result.Read())
            {
                var student = new Student()
                {
                    Id = (uint)result["studentid"],
                    Name = result["studentfname"] + " " + result["studentlname"],
                    StudentNumber = (string)result["studentnumber"],
                    EnrolDate = (DateTime)result["enroldate"],
                };

                students.Add(student);
            }

            return students;
        }

        /// <summary>
        /// GET: api/student/{id}
        /// returns a student with id provided in the input
        /// </summary>
        /// <param name="id">int id of student to view</param>
        /// <returns>
        ///    <Student>
        ///        <EnrolDate>2018-06-18T00:00:00</EnrolDate>
        ///        <Id>1</Id>
        ///        <Name>Sarah Valdez</Name>
        ///        <StudentNumber>N1678</StudentNumber>
        ///    </Student>
        /// </returns>
        [HttpGet]
        [Route("{id:int}")]
        public Student GetById(int id)
        {
            var student = new Student();
            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM students where studentid = " + id;

            var result = cmd.ExecuteReader();

            if (result.Read())
            {
                student.Id = (uint)result["studentid"];
                student.Name = result["studentfname"] + " " + result["studentlname"];
                student.StudentNumber = (string)result["studentnumber"];
                student.EnrolDate = (DateTime)result["enroldate"];
            }

            return student;
        }
    }
}
