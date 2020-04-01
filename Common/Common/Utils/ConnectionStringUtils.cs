using System;

namespace Common.Utils
{
    public static class ConnectionStringUtils
    {
        public static string GetDefaultConnectionString()
        {
            var machineName = Environment.MachineName;

            switch (machineName)
            {
                case "DESKTOP-NTNHKPL":
                    return "Server=DESKTOP-NTNHKPL;Database=RedisApp;Trusted_Connection=True;";
                
                case "iMacIzabelaMaraszkiewiczIt":
                    return "Server=127.0.0.1,1433;Database=RedisApp;User Id=SA;Password=Grubson@2020;";
                
                default:
                    throw new Exception($"Connection string for {machineName} not exists");
            }
        }
    }
}