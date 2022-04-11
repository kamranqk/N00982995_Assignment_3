using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject_1_w2022.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace SchoolProject_1_w2022.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <param name="SearchKey"> search key (optional) of teacher name </param>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <example>GET api/TeacherData/ListTeachers/Linda</example>
        /// <returns>
        /// A list of teachers objects (including fnames, id and lnames)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public List<Teacher> ListTeachers(string SearchKey = null)
        {
            if (SearchKey != null) { 
                Debug.WriteLine("The search key is "+SearchKey);
            }

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            String query = "SELECT * FROM teachers";

            if (SearchKey != null)
            {
                query = query + " where lower(teacherfname) = lower(@key)";
                cmd.Parameters.AddWithValue("@key", SearchKey);
                cmd.Prepare();

            }

            cmd.CommandText = query;

            Debug.WriteLine("The query is:"+query); ;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
             //   NewTeacher.ClassName = ResultSet["classname"].ToString();

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers names
            return Teachers;
        }


        [HttpGet]
        [Route("api/teacherdata/findteacher/{teacherid}")]

        public Teacher FindTeacher(int teacherid)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers Where teacherid= + @id";

            cmd.Parameters.AddWithValue("@id", teacherid);


            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            Teacher SelectedTeacher = new Teacher();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                
                SelectedTeacher.TeacherFName = ResultSet["Teacherfname"].ToString();
                SelectedTeacher.TeacherLName = ResultSet["Teacherlname"].ToString();
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["Teacherid"]);
                //  SelectedTeacher.ClassName = ResultSet["classname"].ToString();
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return SelectedTeacher;
        }   

    }
}