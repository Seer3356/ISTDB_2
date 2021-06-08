using System;
using System.Collections.Generic;
using System.IO;

namespace ISTDB
{
    public class login
    {
        public List<loginList> storage;
        public login()
        {
            loadData();
        }

        /// <summary>
        /// Loads list of valid users and Id's
        /// </summary>
        public void loadData()
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
        }

        /// <summary>
        /// Checks to see if user exists in the system
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool indicating users presence</returns>
        private bool userExists(string username)
        {
            return storage.Exists(x => x.username == username);
        }

        private bool authenticate(string username, string password)
        {
            if (userExists(username))
            {
                int index = storage.FindIndex(x => x.username == username);
                loginList credentials = storage[index];
                string requiredPassword = credentials.password;
                if(password == requiredPassword)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// hand out unique Id for user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>unique user id or -1 if unsuccesful</returns>
        public int handoutId(string username, string password)
        {
            int uniqueId;
            if (authenticate(username, password))
            {
                uniqueId = storage.FindIndex(x => x.username == username);
            }
            else
            {
                uniqueId = -1;
            }
            return uniqueId;
        }
    }
}
