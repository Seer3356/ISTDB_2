using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ISTDB.Login
{
    public class deleteUser
    {
        List<loginList> storage;
        public deleteUser()
        {
            loadData();
        }
        
        public void loadData()
        {
            string directory = Directory.GetCurrentDirectory();
            //Potential MacOS DIfference, filing system works differently, will need revision on port to windows
            string filePath = directory + "/Resources/login.json";
            string json = File.ReadAllText(filePath);
            storage = JsonSerializer.Deserialize<List<loginList>>(json);
            if (storage == null)
            {
                storage = new List<loginList>();
            }
        }

        /// <summary>
        /// Deletes a specified user from database
        /// </summary>
        /// <param name="user"></param>
        public void deletelogin(string user)
        {
            storage.RemoveAll(x => x.username == user);
        }
    }
}
