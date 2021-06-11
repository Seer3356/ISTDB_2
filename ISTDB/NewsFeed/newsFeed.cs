using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ISTDB.NewsFeed
{
    public class newsFeed
    {
        List<newsFeedList> news;
        public newsFeed()
        {
            loadNewsFeedList();
        }

        private void loadNewsFeedList()
        {
            string directory = Directory.GetCurrentDirectory();
            //Potential MacOS DIfference, filing system works differently, will need revision on port to windows
            string filePath = directory + "/Resources/login.json";
            string json = File.ReadAllText(filePath);
            news = System.Text.Json.JsonSerializer.Deserialize<List<newsFeedList>>(json);
            if (news == null)
            {
                news = new List<newsFeedList>();
            }
        }

        public List<newsFeedList> newsFeedAdd(string itemName, long itemBody)
        {
            newsFeedList feed = new newsFeedList();
            feed.newsItem = itemName;
            feed.itemBody = itemBody;
            news.Add(feed);
            saveList();
            return news;
        }

        private void saveList()
        {
            string json = JsonSerializer.Serialize(news);
            string directory = Directory.GetCurrentDirectory();
            //Potential MacOS DIfference, filing system works differently, will need revision on port to windows
            string filePath = directory + "\\Resources\\newsFeed.json";
            File.WriteAllText(filePath, json);
        }
    }
}
