using System.Configuration;

namespace VästeråsSnooker.Helpers
{
    public class ConfigurationReader : IConfigurationReader
    {
        public string GetConString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }
    }
}