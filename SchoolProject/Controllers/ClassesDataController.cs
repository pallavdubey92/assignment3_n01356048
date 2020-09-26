using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SchoolProject.Controllers
{
    [RoutePrefix("api/classes")]
    public class ClassesDataController : ApiController
    {
        private SchoolDbContext dbContext = new SchoolDbContext();

        /// <summary>
        /// GET: api/classes
        /// returns list of all classes
        /// </summary>
        /// <returns>
        /// <ArrayOfClassModel>
        ///     <ClassModel>
        ///         <Code>http5101</Code>
        ///         <FinishDate>2018-12-14T00:00:00</FinishDate>
        ///         <Id>1</Id>
        ///         <Name>Web Application Development</Name>
        ///         <StartDate>2018-09-04T00:00:00</StartDate>
        ///         <TeacherId>1</TeacherId>
        ///     </ClassModel>
        ///     <ClassModel>
        ///         <Code>http5102</Code>
        ///         <FinishDate>2018-12-14T00:00:00</FinishDate>
        ///         <Id>2</Id>
        ///         <Name>Project Management</Name>
        ///         <StartDate>2018-09-04T00:00:00</StartDate>
        ///         <TeacherId>2</TeacherId>
        ///     </ClassModel>
        /// <ArrayOfClassModel>
        /// </returns>
        [HttpGet]
        [Route("")]
        public List<ClassModel> GetAll()
        {
            var classes = new List<ClassModel>();

            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM classes";

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

            return classes;
        }

        /// <summary>
        /// GET: api/classes/{id}
        /// returns a class with id provided in the input
        /// </summary>
        /// <param name="id">int id of the class to view</param>
        /// <returns>
        ///     <ClassModel>
        ///         <Code>http5102</Code>
        ///         <FinishDate>2018-12-14T00:00:00</FinishDate>
        ///         <Id>2</Id>
        ///         <Name>Project Management</Name>
        ///         <StartDate>2018-09-04T00:00:00</StartDate>
        ///         <TeacherId>2</TeacherId>
        ///     </ClassModel>
        /// </returns>
        [HttpGet]
        [Route("{id:int}")]
        public ClassModel GetById(int id)
        {
            ClassModel classModel = new ClassModel(); ;
            var conn = dbContext.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM classes where classid = " + id;

            var result = cmd.ExecuteReader();

            if (result.Read())
            {
                classModel.Id = (int)result["classid"];
                classModel.Code = (string)result["classcode"];
                classModel.Name = (string)result["classname"];
                classModel.TeacherId = (long)result["teacherid"];
                classModel.StartDate = (DateTime)result["startdate"];
                classModel.FinishDate = (DateTime)result["finishdate"];
            }

            return classModel;
        }
    }
}
