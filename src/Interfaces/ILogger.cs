namespace UTR_Restarter
{

    /// <summary>
    /// Logging service that can log messages at various levels of severity
    /// </summary>
    public interface ILogger
	{
		/// <summary>
		/// Logs a message with a specific level
		/// </summary>
		/// <param name="logLevel">the severity level of message</param>
		/// <param name="message">the message content</param>
		/// 
		void Log(LogLevel logLevel, string message);
	}
}

