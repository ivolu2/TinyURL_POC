using System;
using TinyURL_POC.Contracts;

namespace TinyURL_POC
{
	public class TinyURLService
	{

		public List<string> tokens = new List<string>();

		public Dictionary<string, (string longUrl, int visitedCount)> tinyURLs = new Dictionary<string, (string, int)>();

		private readonly ITokenGenerator _tokenGenerator;

		public TinyURLService(ITokenGenerator tokenGenerator)
		{
			_tokenGenerator = tokenGenerator;
		}

        public string createTinyURL(string longURL)
		{

			try
			{
				string uniqueKey = _tokenGenerator.GenerateToken();

				//If token is NOT unique, create a new one (Ensuring that token is unique)
				while (tokens.Contains(uniqueKey))
				{
					_tokenGenerator.GenerateToken();
				}

				//Add unique key to tokens list
				tokens.Add(uniqueKey);

				//Add new tinyURL to list
				string tinyUrl = "https://www.tinyurl/" + uniqueKey;
				tinyURLs.Add(tinyUrl, (longURL, 0));

				return tinyUrl;
			}
			catch(Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"An error occurred while creating the TinyURL: {ex.Message}");
				Console.ResetColor();
				return string.Empty;
			}
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
			try
			{
                //Search for tinyURL (key) in dictionary
                if (tinyURLs.TryGetValue(tinyURL, out var value))
                {
                    //Key found, return longURL
                    return value.longUrl;
                }
                else
                {
                    throw new InvalidOperationException($"TinyURL '{tinyURL}' is not associated with a existing domain.");
                }
				
            }
			catch(Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"An error occurred while getting URL, the TinyURL: {ex.Message}");
				Console.ResetColor();
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
			try
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
                    throw new InvalidOperationException($"TinyURL '{tinyURL}' does not exist.");
                }
            }
			catch (Exception ex)
			{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while getting stats, the TinyURL: {ex.Message}");
                Console.ResetColor();
                return -1;
			}

		}

		public int visit(string tinyURL)
		{
			try
			{
                //Search for tinyURL (key) in dictionary
                if (tinyURLs.ContainsKey(tinyURL))
                {
                    //Key is found so update visitedCount
                    var urlData = tinyURLs[tinyURL];
                    urlData.visitedCount = urlData.visitedCount + 1;


                    tinyURLs[tinyURL] = urlData;

                    return urlData.visitedCount;
                }
                else
                {
					throw new InvalidOperationException($"TinyURL '{tinyURL}' does not exist.");
                }
            }
			catch (Exception ex)
			{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while visiting the TinyURL: {ex.Message}");
                Console.ResetColor();
                return -1;
            }
        }

	}
}

