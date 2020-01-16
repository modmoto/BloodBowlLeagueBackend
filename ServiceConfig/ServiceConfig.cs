using System;
using Microwave;

namespace ServiceConfig
{
    public class ServiceConfiguration
    {
        private static string MatchHostPort = ":5003";
        private static string TeamHostPort = ":5001";
        private static string PlayerHostPort = ":5002";
        private static string SeasonHostPort = ":5004";
        private static string RaceHostPort = ":5007";

        // private static string SeasonReadHostPort = ":5006";
        // private static string TeamReadHostPort = ":5000";

        public static ServiceBaseAddressCollection ServiceAdressesFrom(string baseAdress)
        {
            return new ServiceBaseAddressCollection
            {
                new Uri(baseAdress + MatchHostPort),
                new Uri(baseAdress + TeamHostPort),
                new Uri(baseAdress + PlayerHostPort),
                new Uri(baseAdress + SeasonHostPort),
                new Uri(baseAdress + RaceHostPort)
            };
        }
    }
}