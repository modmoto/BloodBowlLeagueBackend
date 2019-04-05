using System;
using Microwave.Application;
using Microwave.Application.Discovery;

namespace ServiceConfig
{
    public class ServiceConfiguration
    {
        public static ServiceBaseAddressCollection ServiceAdresses => new ServiceBaseAddressCollection
        {
            new Uri("http://localhost:5000"),
            new Uri("http://localhost:5002"),
            new Uri("http://localhost:5004"),
            new Uri("http://localhost:5500")
        };
    }
}