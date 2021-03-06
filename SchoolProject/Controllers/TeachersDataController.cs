using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SchoolProject.Controllers
{
    [RoutePrefix("api/teachers")]
    public class TeachersDataController : ApiController
    {
        private SchoolDbContext dbContext = new SchoolDbContext();

        /// <summary>
        /// GET: api/teachers
        /// returns list of all teachers
        /// </summary>
        /// <param name="nameSearchKey">key to filter list by name</param>
        /// <param name="minSalary">to filter list by min salary (requires both min and max value)</param>
        /// <param name="maxSalary">to filter list by max salary (requires both min and max value)</param>
        /// <param name="startDate">to filter list by hire date, start date for the range</param>
        /// <param name="endDate">to filter list by hire date, end date for the range</param>
        /// <returns>
        /// <ArrayOfTeacher>
        ///     <Teacher>
        ///         <EmployeeNumber>T378</EmployeeNumber>
        ///         <HireDate>2016-08-05T00:00:00</HireDate>
        ///         <Id>1</Id>
        ///         <Name>Alexander Bennett</Name>
        ///         <Salary>55.30</Salary>
        ///     </Teacher>
        ///     <Teacher>
        ///         <EmployeeNumber>T381</EmployeeNumber>
        ///         <HireDate>2014-06-10T00:00:00</HireDate>
        ///         <Id>2</Id>
        ///         <Name>Caitlin Cummings</Name>
        ///         <Salary>62.77</Salary>
        ///     </Teacher>
        /// <ArrayOfTeacher>
        /// </returns>
        [HttpGet]
        [Route("")]
        public List<Teacher> GetAll(
            string nameSearchKey = null,
            decimal minSalary = 0,
            decimal maxSalary = 0,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var teachers = new List<Teacher>();

            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();

            string query = "SELECT * FROM teachers t";

            // add where clause for name is search key is not null
            if (nameSearchKey != null)
            {
                query += " WHERE t.teacherfname LIKE @nameSearchKey OR t.teacherlname LIKE @nameSearchKey";
                cmd.Parameters.AddWithValue("@nameSearchKey", $"%{nameSearchKey}%");
            }

            // add where clause for salary if values are not 0
            if (minSalary != 0 && maxSalary != 0)
            {
                query += " WHERE t.salary >= @min AND t.salary <= @max";
                cmd.Parameters.AddWithValue("@min", minSalary);
                cmd.Parameters.AddWithValue("@max", maxSalary);
            }

            // add where clause for hiring date if date values are not null
            if (startDate != null && endDate != null)
            {
                query += " WHERE t.hiredate >= @start AND t.hiredate<= @end";
                cmd.Parameters.AddWithValue("@start", startDate);
                cmd.Parameters.AddWithValue("@end", endDate);
            }

            cmd.CommandText = query;

            var result = cmd.ExecuteReader();

            while (result.Read())
            {
                var teacher = new Teacher()
                {
                    Id = (int)result["teacherid"],
                    EmployeeNumber = (string)result["employeenumber"],
                    Name = result["teacherfname"] + " " + result["teacherlname"],
                    HireDate = (DateTime)result["hiredate"],
                    Salary = (decimal)result["salary"]
                };

                teachers.Add(teacher);
            }

            conn.Close();
            return teachers;
        }

        /// <summary>
        /// GET: api/teacher/{id}
        /// returns a teacher data inlcuding classes
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// <ShowTeacher>
        ///     <Classes>
        ///         <ClassModel>
        ///             <Code>http5102</Code>
        ///             <FinishDate>2018-12-14T00:00:00</FinishDate>
        ///             <Id>2</Id>
        ///             <Name>Project Management</Name>
        ///             <StartDate>2018-09-04T00:00:00</StartDate>
        ///             <TeacherId>2</TeacherId>
        ///         </ClassModel>
        ///         <ClassModel>
        ///             <Code>http5201</Code>
        ///             <FinishDate>2019-04-27T00:00:00</FinishDate>
        ///             <Id>6</Id>
        ///             <Name>Security & Quality Assurance</Name>
        ///             <StartDate>2019-01-08T00:00:00</StartDate>
        ///             <TeacherId>2</TeacherId>
        ///         </ClassModel>
        ///     </Classes>
        ///     <Teacher>
        ///         <EmployeeNumber>T381</EmployeeNumber>
        ///         <HireDate>2014-06-10T00:00:00</HireDate>
        ///         <Id>2</Id>
        ///         <Name>Caitlin Cummings</Name>
        ///         <Salary>62.77</Salary>
        ///     </Teacher>
        /// </ShowTeacher>
        /// </returns>
        [HttpGet]
        [Route("{id:int}")]
        public ShowTeacherModel GetById(int id)
        {
            var teacher = GetTeacherById(id);
            var classes = GetClassByTeacherId(id);
            var showTeacherModel = new ShowTeacherModel()
            {
                Teacher = teacher,
                Classes = classes,
            };

            return showTeacherModel;
        }

        /// <summary>
        /// returns a teacher with id provided in the input
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// <Teacher>
        ///     <EmployeeNumber>T378</EmployeeNumber>
        ///     <HireDate>2016-08-05T00:00:00</HireDate>
        ///     <Id>1</Id>
        ///     <Name>Alexander Bennett</Name>
        ///     <Salary>55.30</Salary>
        /// </Teacher>
        /// </returns>
        private Teacher GetTeacherById(int id)
        {
            Teacher teacher = new Teacher();
            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM teachers where teacherid = " + id;

            var result = cmd.ExecuteReader();

            if (result.Read())
            {
                teacher.Id = (int)result["teacherid"];
                teacher.EmployeeNumber = (string)result["employeenumber"];
                teacher.Name = result["teacherfname"] + " " + result["teacherlname"];
                teacher.HireDate = (DateTime)result["hiredate"];
                teacher.Salary = (decimal)result["salary"];
            }

            conn.Close();
            return teacher;
        }

        /// <summary>
        /// returns list of classes for a teacher
        /// </summary>
        /// <param name="teacherId">int id of teacher to view</param>
        /// <returns>
        ///         <ClassModel>
        ///             <Code>http5102</Code>
        ///             <FinishDate>2018-12-14T00:00:00</FinishDate>
        ///             <Id>2</Id>
        ///             <Name>Project Management</Name>
        ///             <StartDate>2018-09-04T00:00:00</StartDate>
        ///             <TeacherId>2</TeacherId>
        ///         </ClassModel>
        ///         <ClassModel>
        ///             <Code>http5201</Code>
        ///             <FinishDate>2019-04-27T00:00:00</FinishDate>
        ///             <Id>6</Id>
        ///             <Name>Security & Quality Assurance</Name>
        ///             <StartDate>2019-01-08T00:00:00</StartDate>
        ///             <TeacherId>2</TeacherId>
        ///         </ClassModel>
        /// </returns>
        private List<ClassModel> GetClassByTeacherId(int teacherId)
        {
            var classes = new List<ClassModel>();

            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM classes where teacherid = " + teacherId;

            var result = cmd.ExecuteReader();

            while (result.Read())
            {
                var classModel = new ClassModel()
                {
                    Id = (int)result["classid"],
                    Code = (string)result["classcode"],
                    Name = (string)result["classname"],
                    TeacherId = (long)result["teacherid"],
                    StartDate = (DateTime)result["startdate"],
                    FinishDate = (DateTime)result["finishdate"],
                };

                classes.Add(classModel);
            }

            conn.Close();
            return classes;
        }

        /// <summary>
        /// method to add new teacher to the DB
        /// </summary>
        /// <param name="newTeacher">details of new teacher</param>
        [HttpPost]
        [Route("")]
        public bool Add(AddTeacherModel newTeacher)
        {
            var isValid = newTeacher.Validate();

            if (isValid == false)
            {
                return false;
            }

            var sql = @"INSERT INTO school.teachers
                        (teacherfname, teacherlname, employeenumber, hiredate, salary)
                        VALUES(@fName, @lName, @empNo, @hireDate, @salary)";

            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Prepare();

            cmd.Parameters.AddWithValue("@fName", newTeacher.FName);
            cmd.Parameters.AddWithValue("@lName", newTeacher.LName);
            cmd.Parameters.AddWithValue("@empNo", newTeacher.EmpNo);
            cmd.Parameters.AddWithValue("@hireDate", newTeacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", newTeacher.Salary);

            cmd.ExecuteNonQuery();
            conn.Close();

            return true;
        }

        /// <summary>
        /// deletes a teacher from the database
        /// </summary>
        /// <param name="id">int id of the teacher to delete</param>
        /// <returns>
        /// true, if delete is successful
        /// flase, if teacher has associated classes and cannot be deleted
        /// </returns>
        [HttpDelete]
        [Route("{id}")]
        public bool Delete(int id)
        {
            var classes = GetClassByTeacherId(id);

            if (classes.Count > 0)
            {
                return false;
            }

            var sql = @"DELETE FROM school.teachers
                        WHERE teacherid=@id;";

            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Prepare();

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Close();
            return true;
        }

        /// <summary>
        /// update teacher
        /// </summary>
        /// <param name="id">id of the teacher to be updates</param>
        /// <param name="updatedTeacher">new data for teacher</param>
        [HttpPut]
        [Route("{id:int}")]
        public bool Update([FromUri] int id, [FromBody] AddTeacherModel updatedTeacher)
        {
            if (!updatedTeacher.Validate())
                return false;

            var sql = @"UPDATE school.teachers
                        SET teacherfname=@fname,
                            teacherlname=@lname,
                            employeenumber=@empNo,
                            hiredate=@hireDate,
                            salary=@salary
                        WHERE teacherid=@id";

            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Prepare();

            cmd.Parameters.AddWithValue("@fname", updatedTeacher.FName);
            cmd.Parameters.AddWithValue("@lname", updatedTeacher.LName);
            cmd.Parameters.AddWithValue("@empNo", updatedTeacher.EmpNo);
            cmd.Parameters.AddWithValue("@hireDate", updatedTeacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", updatedTeacher.Salary);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Close();

            return true;
        }
    }
}
