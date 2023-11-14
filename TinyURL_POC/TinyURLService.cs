using System;
namespace TinyURL_POC
{
	public class TinyURLService
	{

		public List<string> tokens = new List<string>();

		public Dictionary<string, (string longUrl, int visitedCount)> tinyURLs = new Dictionary<string, (string, int)>();


        public string createTinyURL(string longURL)
		{

			//Generating unique tokens based on "characters"
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            string uniqueKey = new string(Enumerable.Repeat(characters, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

			//If token is NOT unique, create a new one (Ensuring that token is unique)
			while (tokens.Contains(uniqueKey))
			{
				uniqueKey = new string(Enumerable.Repeat(characters, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            }

			//Add unique key to tokens list
			tokens.Add(uniqueKey);

			//Add new tinyURL to list
            string tinyUrl = "https://www.tinyurl/" + uniqueKey;
			tinyURLs.Add(tinyUrl,(longURL,0));
		
			return tinyUrl;
		}

		public void deleteTinyURL(string tinyURL)
		{
			//Search for tinyURL (key) in dictionary
			if (tinyURLs.ContainsKey(tinyURL)) 
			{
				//Key Found
				//Grabbing token from tinyURL
				string token = tinyURL.Substring(20);

				//Checking if token exists in list, delete from list if so
                if (tokens.Contains(token))
                {
                    tokens.Remove(token);
                }

				//Removing tinyURL from dictionary
                tinyURLs.Remove(tinyURL);
			}

		}

		public string getLongURL(string tinyURL)
		{
			//Search for tinyURL (key) in dictionary
			if(tinyURLs.TryGetValue(tinyURL, out var value)) 
			{
				//Key found, return longURL
				return value.longUrl;
			}
			else 
			{
                return "err";
            }
			
		}

		public bool tinyURLExist(string tinyURL)
		{
            //Search for tinyURL (key) in dictionary
            if (tinyURLs.ContainsKey(tinyURL)) 
            {
				//Key is found, return true
				return true;
            }
			else 
			{
                return false;
            }
           
		}

		public int getStats(string tinyURL)
		{
            //Search for tinyURL (key) in dictionary
            if (tinyURLs.ContainsKey(tinyURL)) 
            {
				//Key is found so return stats
				var urlData = tinyURLs[tinyURL];
                return urlData.visitedCount;
            }
            else 
            {
				return -1;
            }

		}

		public int visit(string tinyURL)
		{
            //Search for tinyURL (key) in dictionary
            if (tinyURLs.ContainsKey(tinyURL)) 
            {
				//Key is found so update visitedCount
				var urlData = tinyURLs[tinyURL];
				urlData.visitedCount = urlData.visitedCount+1;

				
				tinyURLs[tinyURL] = urlData;

                return urlData.visitedCount;
            }
            else 
            {
                return -1;
            }
        }

	}
}

