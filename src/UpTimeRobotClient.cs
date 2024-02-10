using System;
using System.Text.Json.Nodes;
using System.Threading;
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

        public bool IsMonitorUp(int monitorID)
        {
            var request = new RestRequest("getMonitors", Method.Post)
            .AddParameter("format", "json")
            .AddParameter("monitors", monitorID)
            .AddParameter("api_key", apiKey);

            var response = client.Execute(request);

            if (!response.IsSuccessful) return false;
            logger.Log(LogLevel.Information, $"Checking monitor #{monitorID}");

            var obj = JsonObject.Parse(response.Content ?? "");
            if (obj?["stat"]?.ToString().ToLower() == "ok")
            {
                var status = obj?["monitors"]?[0]?["status"]?.ToString();
                return status == "2";
            }
            return false;

        }

    }
}

