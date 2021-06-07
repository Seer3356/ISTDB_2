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
