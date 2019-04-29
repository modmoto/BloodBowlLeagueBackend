using System;
using Microwave.Application;

namespace ServiceConfig
{
    public class ServiceConfiguration
    {
        public static ServiceBaseAddressCollection ServiceAdresses => new ServiceBaseAddressCollection
        {
            new Uri("http://localhost:5000"),
            new Uri("http://localhost:5001"),
            new Uri("http://localhost:5002"),
            new Uri("http://localhost:5003"),
            new Uri("http://localhost:5004")
        };
    }
}