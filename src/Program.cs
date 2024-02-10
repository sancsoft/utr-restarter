namespace UTR_Restarter
{
    using Microsoft.Extensions.Configuration;
    using RestSharp;

    internal class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            var messageHandler = new ConsoleStartupMessageHandler(logger);
            messageHandler.DisplayStartupMessages();
             IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings-sample.json").Build();
            AppSettings settings = config.GetRequiredSection("Settings").Get<AppSettings>() ?? new AppSettings();
            
            var uptimeRobotClient = new UpTimeRobotClient("https://api.uptimerobot.com/v2", settings.ApiKey, logger);
            var processManager = new ProcessManager(settings.ProcessName, settings.Run, settings.Args, settings.Pause, logger);
            logger.Log(LogLevel.Information, $"Checking monitor #{settings.Monitor}");
            bool isMonitorUp = uptimeRobotClient.IsMonitorUp(settings.Monitor, settings.ProcessName);
            processManager.RestartProcessIfNeeded(!isMonitorUp);
    }
    }
}