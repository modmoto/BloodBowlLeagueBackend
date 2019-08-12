using System;
using Microwave;

namespace ServiceConfig
{
    public class ServiceConfiguration
    {
        public static ServiceBaseAddressCollection ServiceAdresses => new ServiceBaseAddressCollection
        {
            new Uri("http://localhost:5001"),
            new Uri("http://localhost:5002"),
            new Uri("http://localhost:5003"),
            new Uri("http://localhost:5004"),
            new Uri("http://localhost:5007"),
        };
    }
}