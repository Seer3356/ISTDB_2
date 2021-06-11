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

        /// <summary>
        /// loads news feed list to new list
        /// </summary>
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
        
        /// <summary>
        /// Adds news item to dtabase
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemBody"></param>
        /// <returns>list of news items</returns>
        public List<newsFeedList> newsFeedAdd(string itemName, long itemBody)
        {
            newsFeedList feed = new newsFeedList();
            feed.newsItem = itemName;
            feed.itemBody = itemBody;
            news.Add(feed);
            saveList();
            return news;
        }

        public List<newsFeedList> getNewsItems()
        {
            return news;
        }

        /// <summary>
        /// saves changes to list to file
        /// </summary>
        private void saveList()
        {
            string json = JsonSerializer.Serialize(news);
            string directory = Directory.GetCurrentDirectory();
            //Potential MacOS DIfference, filing system works differently, will need revision on port to windows
            string filePath = directory + "/Resources/newsFeed.json";
            File.WriteAllText(filePath, json);
        }
    }
}
