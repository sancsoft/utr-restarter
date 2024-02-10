using UTR_Restarter;

public class ConsoleLogger : ILogger
{
    public void Log(LogLevel level, string message)
    {
        switch (level)
        {
            case LogLevel.Information:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case LogLevel.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case LogLevel.Error:
            case LogLevel.Critical:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
        }

        Console.WriteLine($"{DateTime.Now} [{level}]: {message}");
        Console.ResetColor();
    }
}