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

        /// <summary>
        /// Restarts the process if the monitor indicates it is down.
        /// </summary>
        /// <param name="isMonitorUp">A flag indicating whether the monitor is up.</param>
        public void RestartProcessIfNeeded(bool isMonitorUp)
        {
            if (!isMonitorUp)
            {
                KillProcess(processName);
                Thread.Sleep(pauseTime);
                StartProcess(runCommand, arguments);
            }
        }

        /// <summary>
        /// Kills all processes with the specified name.
        /// </summary>
        /// <param name="processName">The name of the process to kill.</param>
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


        /// <summary>
        /// Starts a process using the specified command and arguments.
        /// </summary>
        /// <param name="command">The command or executable to start.</param>
        /// <param name="arguments">The arguments to pass to the executable.</param>
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

