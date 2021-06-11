using ISTDB.spreadsheetRead;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ISTDB.NewsFeed;

namespace ISTDB.Controllers
{
    public class ISTDBController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Logs in specified user to system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Users unique Id or -1 if login was unsuccesful</returns>
        [HttpPost]
        [Route("login")]
        public int login(string username, string password)
        {
            login login = new login();
            return login.handoutId(username, password);
        }

        /// <summary>
        /// Signs new user up to program
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool indicating succes</returns>
        [HttpPost]
        [Route("signup")]
        public bool createAccount(string username, string password)
        {
            signUp sign = new signUp();
            return sign.createAccount(username, password);
        }

        /// <summary>
        /// Fetches timetable data for current date
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns>List<classList> containing sessions for day</classList></returns>
        [HttpGet]
        [Route("getTimetable")]
        public List<classList> getTimetable(int uniqueId)
        {
            importData data = new importData();
            var classes = data.readTimetable(uniqueId);
            return classes;
        }

        /// <summary>
        /// gets full timetable
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns>List<classList> of student full timetable</classList></returns>
        [HttpGet]
        [Route("getFullTimetable")]
        public List<classList> getFullTimetable(int uniqueId)
        {
            importData data = new importData();
            List<classList> stuff = data.getFullTimetable(uniqueId);
            return stuff;
        }

        /// <summary>
        /// Adds a news item to the database
        /// </summary>
        /// <param name="newsItem"></param>
        /// <param name="itemBody"></param>
        /// <returns>bool indicating success</returns>
        [HttpPost]
        [Route("addNewsFeed")]
        public bool addNewsFeed(string newsItem, long itemBody)
        {
            newsFeed news = new newsFeed();
            news.newsFeedAdd(newsItem, itemBody);
            return true;
        }

        /// <summary>
        /// API endpoint for getting news items
        /// </summary>
        /// <returns>List of current news items</returns>
        [HttpGet]
        [Route("getNewsItems")]
        public List<newsFeedList> getNewsItems()
        {
            newsFeed news = new newsFeed();
            List<newsFeedList> newsFeed = news.getNewsItems();
            return newsFeed;
        }

        [HttpDelete]
        [Route("deleteNewsitem")]
        public void deleteNewsItem(string itemName)
        {
            newsFeed news = new newsFeed();
            news.deletenewsItem(itemName);
        }
    }
}
