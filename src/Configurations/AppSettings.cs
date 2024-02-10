namespace UTR_Restarter
{
    public sealed class AppSettings
    {
        public AppSettings() { Monitor = 0; ApiKey = ""; ProcessName = ""; Pause = 0; Run = ""; Args = ""; }
        public int Monitor { get; set; }
        public string ApiKey { get; set; }
        public string ProcessName { get; set; }
        public int Pause { get; set; }
        public string Run { get; set; }
        public string Args { get; set; }
    }
}