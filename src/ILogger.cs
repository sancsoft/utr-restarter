using System;
namespace UTR_Restarter
{
	// Defines the severity levels of log messages

	public enum LogLevel
	{
		Information,
		Warning,
		Error,
		Critical
	}

	// Logging service that can log messages at various levels of severity

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

