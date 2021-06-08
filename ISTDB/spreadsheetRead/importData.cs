using System;
using System.Collections.Generic;
using System.IO;
using IronXL;
using System.Linq;

namespace ISTDB.spreadsheetRead
{
    public class importData
    {
        public importData()
        {
            //loadStudentList();
        }

        /*private void loadStudentList()
        {
            string directory = Directory.GetCurrentDirectory();
            //Potential MacOS DIfference, filing system works differently, will need revision on port to windows
            string filePath = directory + "/Resources/login.json";
            string json = File.ReadAllText(filePath);
            storage = System.Text.Json.JsonSerializer.Deserialize<List<loginList>>(json);
            if (storage == null)
            {
                storage = new List<loginList>();
            }
        }*/


        /// <summary>
        /// Gets directory of specified resource 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>string of resource directory</returns>
        private static string getDirectory(string filename)
        {
            string directory = Directory.GetCurrentDirectory();
            //Potential MacOS DIfference, filing system works differently, will need revision on port to windows
            string filePath = directory + filename;
            return filePath;
        }


        /// <summary>
        /// Loads the worksheet containing timetable data for student
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <param name="file"></param>
        /// <returns>Worksheet object</returns>
        private static WorkSheet loadWorkbook(int uniqueId, string file)
        {
            string filepath = getDirectory(file);
            var workbook = WorkBook.Load(filepath);
            string Id = Convert.ToString(uniqueId);
            var worksheet = workbook.GetWorkSheet(Id);
            return worksheet;
        }


        /// <summary>
        /// Sets the string for range of day
        /// </summary>
        /// <returns>String for range</returns>
        private static string getSessions()
        {
            //string week = getweek();
            string week = "A";
            string day = DateTime.Now.ToString("dddd");
            string dayWeek = day + " " + week;
            string sessions;
            switch (dayWeek)
            {
                case "Monday A":
                    sessions = "C2:E9";
                    break;
                case "Tuesday A":
                    sessions = "C10:E17";
                    break;
                case "Wednesday A":
                    sessions = "C18:E25";
                    break;
                case "Thursday A":
                    sessions = "C26:E33";
                    break;
                case "Friday A":
                    sessions = "C34:E41";
                    break;
                case "Monday B":
                    sessions = "C42:E49";
                    break;
                case "Tuesday B":
                    sessions = "C50:E57";
                    break;
                case "Wednesday B":
                    sessions = "C58:E65";
                    break;
                case "Thursday B":
                    sessions = "C66:E73";
                        break;
                case "Friday B":
                    sessions = "C74:E81";
                    break;
                default:
                    sessions = null;
                    break;
            }
            return sessions;
        }

        /// <summary>
        /// Collates session data
        /// </summary>
        /// <param name="range"></param>
        /// <returns>List of session data for current day</returns>
        private List<classList> RowData(IronXL.Range range)
        {
            List<classList> sessions = new List<classList>();
            for(int i = 0; i < 8; i = i + 1)
            {
                classList data = new classList();
                string teacher = range.Rows[i].Columns[0].ToString();
                string session = range.Rows[i].Columns[1].ToString();
                string room = range.Rows[i].Columns[2].ToString();
                data.sessionNo = i + 1;
                data.session = session;
                data.teacher = (teacher);
                data.room = (room);
                sessions.Add(data);
            }
            return sessions;
        }

        /// <summary>
        /// gets timetable data
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns>list of session data</returns>
        public List<classList> readTimetable(int uniqueId)
        {
            string filepath = "/Resources/timetable.xlsx";
            var worksheet = loadWorkbook(uniqueId, filepath);
            string sessionsRange = getSessions();
            var range = worksheet.GetRange(sessionsRange);
            List<classList> data = RowData(range);
            return data;
        }
    }
}
