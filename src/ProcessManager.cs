using System.Diagnostics;

namespace UTR_Restarter
{
	public class ProcessManager
	{
        private readonly string processName;
        private readonly string runCommand;
        private readonly string arguments;
        private readonly int pauseTime;
        private readonly ILogger logger;

        public ProcessManager(string processName, string runCommand, string arguments, int pauseTime, ILogger logger)
        {
            this.processName = processName;
            this.runCommand = runCommand;
            this.arguments = arguments;
            this.pauseTime = pauseTime;
            this.logger = logger;
        }

        public void RestartProcessIfNeeded(bool isMonitorUp)
        {
            if (!isMonitorUp)
            {
                KillProcess(processName);
                Thread.Sleep(pauseTime);
                StartProcess(runCommand, arguments);
            }
        }

        private void KillProcess(string processName)
        {
            try
            {
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    logger.Log(LogLevel.Warning, $"Killing process {process.ProcessName}");
                    process.Kill();
                }
            }
            catch(Exception exception)
            {
                logger.Log(LogLevel.Error, $"Failed to kill process {processName}. Error: {exception.Message}");
            }
        }

        private void StartProcess(string command, string arguments)
        {
            logger.Log(LogLevel.Information, $"Running program {command} with {arguments}");

            try
            {
                var process = Process.Start(command, arguments);
                if (process == null)
                {
                    logger.Log(LogLevel.Error, "Failed to start process.");
                }
                else
                {
                    logger.Log(LogLevel.Information, $"Process started successfully with ID {process.Id}");
                }
            }
            catch (Exception exexception)
            {
                Console.WriteLine($"Failed to start process {command}. Error: {exexception.Message}");
            }
        }
    }
}

