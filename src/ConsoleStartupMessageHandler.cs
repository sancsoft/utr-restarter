using System;
namespace UTR_Restarter
{
    public class ConsoleStartupMessageHandler : IStartupMessageHandler
    {
        ILogger logger;

        public ConsoleStartupMessageHandler(ILogger logger)
        {
            this.logger = logger;
        }

        public void DisplayStartupMessages()
        {
            string currentYear = DateTime.Now.Year.ToString();
            logger.Log(LogLevel.Information, "UTR-Restarter: Uptime Robot Triggered Process Restart");
            logger.Log(LogLevel.Information, $"Copyright (c) {currentYear} - )|( Sanctuary Software Studio, Inc. - All rights reserved.");
        }
    }

}

