using System.Text.Json.Nodes;
using RestSharp;
namespace UTR_Restarter
{
    public class UpTimeRobotClient
    {
        private readonly RestClient client;
        private readonly string apiKey;
        private readonly ILogger logger;

        public UpTimeRobotClient(string baseURL, string apiKey, ILogger logger)
        {
            this.client = new RestClient(new RestClientOptions(baseURL));
            this.apiKey = apiKey;
            this.logger = logger;
        }

        public bool IsMonitorUp(int monitorID, string processName)
        {
            var request = new RestRequest("getMonitors", Method.Post)
            .AddParameter("format", "json")
            .AddParameter("monitors", monitorID)
            .AddParameter("api_key", apiKey);

            var response = client.Execute(request);

            if (!response.IsSuccessful) {
                logger.Log(LogLevel.Warning, "Unable to reach Uptime Robot.");
                return false;
            }
           

            var obj = JsonObject.Parse(response.Content ?? "");
            if (obj?["stat"]?.ToString().ToLower() == "ok")
            {
                var status = obj?["monitors"]?[0]?["status"]?.ToString();
                if (status == "2")
                {
                    logger.Log(LogLevel.Information, $"Process {processName} is reporting OK on UTR.");
                    return true;
                }
            }
            else
            {
                logger.Log(LogLevel.Error, $"Process {processName} is not reporting status.");
            }
            return false;

        }

    }
}

