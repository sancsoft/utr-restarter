namespace UTR_Restarter
{
    using System;
    using System.Diagnostics;
    using System.Text.Json.Nodes;
    using Microsoft.Extensions.Configuration;
    using RestSharp;

    public sealed class AppSettings
    {
        public AppSettings() { Monitor = 0; ApiKey = ""; ProcessName = ""; Pause = 0; Run = ""; Args = ""; }
        public int Monitor { get; set; }
        public string ApiKey { get; set; }
        public string ProcessName { get; set; }
        public int Pause { get; set; }
        public string Run { get; set; }
        public string Args { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UTR-Restarter: Uptime Robot Triggered Process Restart");
            Console.WriteLine("Copyright (c) 2024 - )|( Sanctuary Software Studio, Inc. - All rights reserved.");

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            AppSettings settings = config.GetRequiredSection("Settings").Get<AppSettings>() ?? new AppSettings();

            var restClientOptions = new RestClientOptions("https://api.uptimerobot.com/v2");
            var restClient = new RestClient(restClientOptions);
            var request = new RestRequest("getMonitors", Method.Post);
            request.AddParameter("format", "json");
            request.AddParameter("monitors", settings.Monitor);
            request.AddParameter("api_key", settings.ApiKey);
            var response = restClient.Execute(request);

            Console.WriteLine($"Checking monitor #{settings.Monitor}");

            if (response.IsSuccessful)
            {
                var obj = JsonObject.Parse(response.Content ?? "");
                if (obj?["stat"]?.ToString().ToLower() == "ok")
                {
                    Console.WriteLine($"Process {settings.ProcessName} is reporting OK on UTR.");
                }
                else
                {
                    foreach (var process in Process.GetProcessesByName(settings.ProcessName))
                    {
                        Console.WriteLine($"Killing process {process.ProcessName}");
                        process.Kill();
                    }

                    Console.WriteLine("Waiting for process to clean up");
                    Thread.Sleep(settings.Pause);

                    Console.WriteLine($"Running program {settings.Run} with {settings.Args}");
                    Process.Start(settings.Run, settings.Args);
                }
            }
            else
            {
                Console.WriteLine("Unable to reach Uptime Robot.");
            }
        }
     }
}