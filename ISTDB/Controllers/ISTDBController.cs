﻿using ISTDB.spreadsheetRead;
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
    }
}
