using ISTDB.spreadsheetRead;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ISTDB.Controllers
{
    public class ISTDBController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        public int login(string username, string password)
        {
            login login = new login();
            return login.handoutId(username, password);
        }
        [HttpPost]
        [Route("signup")]
        public bool createAccount(string username, string password)
        {
            signUp sign = new signUp();
            return sign.createAccount(username, password);
        }

        [HttpGet]
        [Route("getTimetable")]
        public List<classList> getTimetable(int uniqueId)
        {
            importData data = new importData();
            var classes = data.readTimetable(uniqueId);
            return classes;
        }
    }
}
