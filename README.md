# utr-restarter

Uptime Robot Triggered Process Restart

UTR-Restarter is a .NET Core program that checks the Uptime Robot (UTR) version 2 API to check on the status of a single monitor. If UTR reports the monitor as down, the program kills the associated process, delays, and attempts to start the process.

UTR-Restarter can be run on demand or as a scheduled task in Windows. 

## Settings 

The program is configured through appsettings.json using the following key-value pairs.

* Monitor: UTR monitor number
* ApiKey: UTR read-only api key
* ProcessName: Friendly name of the process killed (ex: "Notepad")
* Pause: Delay before attempting to start in milliseconds
* Run: Command line to run to start the program
* Args: Passed to the program on the command line on start

The file appsettings-sample.json shows the format for configuration.

Use "tasklist" from the command line to identify the name of the process to be killed. This is generally the name of the program without the .exe extension.

