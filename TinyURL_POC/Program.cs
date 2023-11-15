
using System;
using Microsoft.Extensions.DependencyInjection;
using TinyURL_POC;
using TinyURL_POC.Contracts;

class Program
{
    static void Main()
    {

        var serviceProvider = new ServiceCollection()
            .AddTransient<ITokenGenerator, DefaultTokenGenerator>()
            .AddTransient<TinyURLService>()
            .BuildServiceProvider();

        //Initializing Service to help manage tinyURLs
        var tinyURLService = serviceProvider.GetRequiredService<TinyURLService>();

        //Create string to store action list. Combine all actions into one string
        //Use @ symbol to indicate a new line
        string action_list = "@Please provide an action. The options are below:@delete: delete tinyurl@create: create tinyurl@getLong: get long url@getStats: get TinyURL statistics@visit: visit tinyUrl@help: list actions@exit: exit the program@";

        //Creating a new line in the string for every @ symbol
        action_list = action_list.Replace("@", System.Environment.NewLine);

        //Display Action List for User when Application Starts
        Console.WriteLine("Welcome to the Custom TinyURL Converter!");
        Console.WriteLine(action_list);


        //Store List of Actions for Quick Searching 
        List<string> actions = new List<string> { "delete", "create", "getlong", "getstats", "visit", "help", "exit" };


        //initialize action string to start
        string action = "start";


        //Keep App Running until User Exits
        while (action != "exit")
        {
            action = Console.ReadLine();
            action = action.ToLower();

            //Checking if valid action was provided. Searching through List actions to determine if input is valid
            if (!actions.Contains(action)) //invalid action provided
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid action selected, please try again");
                Console.ResetColor();
                Console.WriteLine("Type 'help' to re-list actions");
                Console.WriteLine();

            }
            else //valid action provided
            {
                if (action == "help") //help action provided, re-list actions
                {
                    Console.WriteLine(action_list);
                }
                else if (action == "delete") //delete action
                {
                    Console.WriteLine("Deleting...please provide tinyURL to delete: ");
                    string url = Console.ReadLine();
                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute)) //Checking if tinyURL is a valid format for Uri
                    {
                        tinyURLService.deleteTinyURL(url);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Action Completed");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("URL provided is in an invalid format.");
                    }
                }
                else if (action == "create") //create action
                {
                    Console.WriteLine("Creating... please provide URL to shorten:");
                    string url = Console.ReadLine();
                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute))//Checking if longURL provided is a valid format for Uri
                    {
                        string tiny = tinyURLService.createTinyURL(url);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("tinyURL = " + tiny);

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("URL provided is in an invalid format.");
                        Console.ResetColor();
                    }
                }
                else if (action == "getlong") //getLong action
                {

                    Console.WriteLine("Getting long url... please provide tinyURL:");
                    string url = Console.ReadLine();

                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute))//Checking if tinyURL provided is a valid format for Uri
                    {
                        string longURL = tinyURLService.getLongURL(url);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(longURL);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("URL provided is in an invalid format.");

                    }

                }
                else if (action == "getstats") //getStats action
                {
                    Console.WriteLine("Getting stats... please provide tinyURL:");
                    string url = Console.ReadLine();
                    int count = tinyURLService.getStats(url);

                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute))//Checking if tinyURL provided is a valid format for Uri
                    {
  
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Visited Count is: " + count);
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("URL provided is in an invalid format.");

                    }
                }
                else if (action == "visit")
                {
                    Console.WriteLine("Visiting... please provide tinyURL:");
                    string url = Console.ReadLine();
                    int count = tinyURLService.visit(url);

                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute))//Checking if tinyURL provided is a valid format for Uri
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("tinyURL has been visited.");
                        string longURL = tinyURLService.getLongURL(url);
                        Console.WriteLine("redirected to: " + longURL);
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("URL provided is in an invalid format.");

                    }

                }
                Console.ResetColor();
                Console.WriteLine("Please provide another action. Type 'help' to re-list actions");
                Console.WriteLine("");

            }


        }
    }
}
