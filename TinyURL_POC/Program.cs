
using System;
using Microsoft.Extensions.DependencyInjection;
using TinyURL_POC;
using TinyURL_POC.Contracts;

class Program
{
    //Function for Invalid URL Format on User Input
    private static void HandleInvalidUrlFormat()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("URL provided is in an invalid format.");
    }

    //Function for Invalid Action on User Input
    private static void HandleInvalidAction()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid action selected, please try again");
    }

    static void Main()
    {

        //Dependency Injection for Helper Service and Token Generator
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

            switch (action)
            {
       
                case "help":
                    Console.WriteLine(action_list);
                    break;

                case "delete":
                    Console.WriteLine("Deleting...please provide tinyURL to delete: ");
                    string deleteUrl = Console.ReadLine();
                    if (Uri.IsWellFormedUriString(deleteUrl, UriKind.Absolute)) //Checking if tinyURL is a valid format for Uri
                    {
                        tinyURLService.deleteTinyURL(deleteUrl);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Action Completed");
                    }
                    else
                    {
                        HandleInvalidUrlFormat();
                    }
                    break;

                case "create":
                    Console.WriteLine("Creating... please provide URL to shorten:");
                    string createUrl = Console.ReadLine();
                    if (Uri.IsWellFormedUriString(createUrl, UriKind.Absolute))//Checking if longURL provided is a valid format for Uri
                    {
                        string tiny = tinyURLService.createTinyURL(createUrl);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("tinyURL = " + tiny);

                    }
                    else
                    {
                        HandleInvalidUrlFormat();
                    }
                    break;

                case "getlong":
                    Console.WriteLine("Getting long url... please provide tinyURL:");
                    string getLongUrl = Console.ReadLine();

                    if (Uri.IsWellFormedUriString(getLongUrl, UriKind.Absolute))//Checking if tinyURL provided is a valid format for Uri
                    {
                        string longURL = tinyURLService.getLongURL(getLongUrl);

                        if (longURL != "err") //longURL of "err" means exception was thrown in Service
                        {                            
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(longURL);
                        }
                    }
                    else
                    {
                        HandleInvalidUrlFormat();

                    }
                    break;

                case "getstats":
                    Console.WriteLine("Getting stats... please provide tinyURL:");
                    string getStatsUrl = Console.ReadLine();
                    int getStatsCount = tinyURLService.getStats(getStatsUrl);

                    if (Uri.IsWellFormedUriString(getStatsUrl, UriKind.Absolute))//Checking if tinyURL provided is a valid format for Uri
                    {
                        if (getStatsCount != -1) //count of -1 means exception was thrown in Service
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Visited Count is: " + getStatsCount);
                        }

                    }
                    else
                    {
                        HandleInvalidUrlFormat();

                    }
                    break;

                case "visit":
                    Console.WriteLine("Visiting... please provide tinyURL:");
                    string visitUrl = Console.ReadLine();
                    int visitCount = tinyURLService.visit(visitUrl);

                    if (Uri.IsWellFormedUriString(visitUrl, UriKind.Absolute))//Checking if tinyURL provided is a valid format for Uri
                    {
                        if (visitCount != -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("tinyURL has been visited.");
                            string longURL = tinyURLService.getLongURL(visitUrl);
                            Console.WriteLine("redirected to: " + longURL);
                        }

                    }
                    else
                    {
                        HandleInvalidUrlFormat();

                    }
                    break;

                default:
                    HandleInvalidAction();
                    break;    
            }

            Console.ResetColor();
            Console.WriteLine("Please provide another action. Type 'help' to re-list actions");
            Console.WriteLine("");

        }
    }
}
