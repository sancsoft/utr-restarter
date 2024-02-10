namespace UTR_Restarter
{
    using Microsoft.Extensions.Configuration;
    using RestSharp;

    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize the Console Logger for application-wide logging
            ILogger logger = new ConsoleLogger();

            // Set up the startup message handler with the logger and display startup messages
            var messageHandler = new ConsoleStartupMessageHandler(logger);
            messageHandler.DisplayStartupMessages();

            // Load the application settings from the 'appsettings-sample.json' file
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings-sample.json").Build();
            AppSettings settings = config.GetRequiredSection("Settings").Get<AppSettings>() ?? new AppSettings();

            // Initialize the Uptime Robot client with API key and logger
            var uptimeRobotClient = new UpTimeRobotClient("https://api.uptimerobot.com/v2", settings.ApiKey, logger);

            // Set up the process manager with the necessary settings and logger
            var processManager = new ProcessManager(settings.ProcessName, settings.Run, settings.Args, settings.Pause, logger);

            // Log the action of checking the Uptime Robot monitor status
            logger.Log(LogLevel.Information, $"Checking monitor #{settings.Monitor}");

            // Check if the monitored service is up and restart it if needed
            bool isMonitorUp = uptimeRobotClient.IsMonitorUp(settings.Monitor, settings.ProcessName);
            processManager.RestartProcessIfNeeded(!isMonitorUp);
        }
    }

}