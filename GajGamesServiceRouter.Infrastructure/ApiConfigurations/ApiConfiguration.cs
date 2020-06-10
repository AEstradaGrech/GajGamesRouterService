using System;
using System.Collections.Generic;

namespace GajGamesServiceRouter.Infrastructure.ApiConfigurations
{
    public abstract class ApiConfiguration
    {
        public ApiConfiguration()
        {
            Endpoints = new Dictionary<string, string>();
        }

        public string BaseUrl { get; set; }
        public Dictionary<string, string> Endpoints { get; set; }

        public string EndpointByKey(string key)
        {
            return Endpoints[key];
        }
    }
}
