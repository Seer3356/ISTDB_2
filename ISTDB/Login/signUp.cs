using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace ISTDB
{
    public class signUp
    {
        List<loginList> login;
        public signUp()
        {
            loadData();
        }

        /// <summary>
        /// loads valid logins from file
        /// </summary>
        private void loadData()
        {
            string directory = Directory.GetCurrentDirectory();
            //Potential MacOS DIfference, filing system works differently, will need revision on port to windows
            string filePath = directory + "/Resources/login.json";
            string json = File.ReadAllText(filePath);
            login = JsonSerializer.Deserialize<List<loginList>>(json);
            if (login == null)
            {
                login = new List<loginList>();
            }
        }

        /// <summary>
        /// creates id for user
        /// </summary>
        /// <returns>user id for system to store</returns>
        private int getIndex()
        {
            return login.Count;
        }

        /// <summary>
        /// creates login credentials and Id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool indicating succes of signup</returns>
        public bool createAccount(string username, string password)
        {
            if(!userExists(username))
            {
                int id = getIndex();
                loginList list = new loginList();
                list.id = id;
                list.username = username;
                list.password = password;
                login.Add(list);
                saveList();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checks if user is already in system
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool indicating if user is in system</returns>
        private bool userExists(string username)
        {
            return login.Exists(x => x.username == username);
        }

        /// <summary>
        /// saves user credentials to file
        /// </summary>
        private void saveList()
        {
            string json = JsonSerializer.Serialize(login);
            string directory = Directory.GetCurrentDirectory();
            //Potential MacOS DIfference, filing system works differently, will need revision on port to windows
            string filePath = directory + "/Resources/login.json";
            File.WriteAllText(filePath, json);
        }
    }
}
