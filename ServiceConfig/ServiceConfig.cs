using System;
using Microwave.Discovery;

namespace ServiceConfig
{
    public static class ServiceConfiguration
    {
        private static string TeamHostPort = ":6001";
        private static string PlayerHostPort = ":6002";
        private static string MatchHostPort = ":6003";
        private static string SeasonHostPort = ":6004";
        private static string RaceHostPort = ":6005";

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