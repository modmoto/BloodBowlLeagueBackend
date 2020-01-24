using System;
using Microwave.Discovery;

namespace ServiceConfigNew
{
    public class ServiceConfiguration
    {
        public static ServiceBaseAddressCollection ServiceAdresses => new ServiceBaseAddressCollection
        {
            MatchHost,
            TeamHost,
            PlayerHost,
            SeasonHost,
            RaceHost
        };

        public static Uri MatchHost { get; } = new Uri("http://matches-host.blood-bowl-league.com/");
        public static Uri TeamHost { get; } = new Uri("http://teams-host.blood-bowl-league.com/");
        public static Uri PlayerHost { get; }  = new Uri("http://players-host.blood-bowl-league.com/");
        public static Uri SeasonHost { get; } = new Uri("http://seasons-host.blood-bowl-league.com/");
        public static Uri RaceHost { get; } = new Uri("http://races-host.blood-bowl-league.com/");

        public static Uri SeasonReadHost { get; } = new Uri("http://seasons-readhost.blood-bowl-league.com/");
        public static Uri TeamReadHost { get; } = new Uri("http://teams-readhost.blood-bowl-league.com/");

//        public static Uri MatchHost { get; } = new Uri("http://localhost:5003");
//        public static Uri TeamHost { get; } = new Uri("http://localhost:5001");
//        public static Uri PlayerHost { get; }  = new Uri("http://localhost:5002");
//        public static Uri SeasonHost { get; } = new Uri("http://localhost:5004");
//        public static Uri RaceHost { get; } = new Uri("http://localhost:5007");
//
//        public static Uri SeasonReadHost { get; } = new Uri("http://localhost:5006");
//        public static Uri TeamReadHost { get; } = new Uri("http://localhost:5000");
    }
}