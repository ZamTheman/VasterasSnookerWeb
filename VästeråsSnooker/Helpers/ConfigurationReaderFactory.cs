namespace VästeråsSnooker.Helpers
{
    public class ConfigurationReaderFactory : IConfigurationReaderFactory
    {
        public IConfigurationReader CreateConfigurationReader()
        {
            return new ConfigurationReader();
        }
    }
}