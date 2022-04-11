using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject_1_w2022.Models;
using System.Diagnostics;

namespace SchoolProject_1_w2022.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher

        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List 
        // showing the page of all the Teacher in the system
        [Route("Teacher/List/{SearchKey}")]
        public ActionResult List(string SearchKey)
        {
            Debug.WriteLine("The key is "+SearchKey);
            // connects to out data access layer
            // get our teachers
            // pass the Teacher to the veiw Teacher/list cshtml

            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);
        }
        // GET: Teacher/Show/{id} 
        // showing the page of teachers in the system by id  
        // [Route("/Teacher/Show/{Teacherid}")]
        public ActionResult Show(int id)
        {
            // how do we get the selected Teacher
            // get our Teachers
            // pass the Teacher to the veiw Teacher/list cshtml

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            //routes the single Teacher info to show.cshtml
            return View(SelectedTeacher);
        }
    }
}