using System;
namespace UTR_Restarter
{
	public interface IStartupMessageHandler
	{
        ILogger;
        void DisplayStartupMessages();
    }
}

