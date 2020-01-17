using System;
using Microwave;

namespace ServiceConfig
{
    public class ServiceConfiguration
    {
        private static string TeamHostPort = ":6001";
        private static string PlayerHostPort = ":6002";
        private static string MatchHostPort = ":6003";
        private static string SeasonHostPort = ":6004";
        private static string RaceHostPort = ":6005";

        // private static string SeasonReadHostPort = ":5001";
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