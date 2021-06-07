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


        private int getIndex()
        {
            return login.Count;
        }
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
        private bool userExists(string username)
        {
            return login.Exists(x => x.username == username);
        }
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
