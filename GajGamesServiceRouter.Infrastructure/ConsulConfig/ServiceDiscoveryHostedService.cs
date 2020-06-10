using System;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Hosting;

namespace GajGamesServiceRouter.Infrastructure.ConsulConfig
{
    public class ServiceDiscoveryHostedService : IHostedService
    {
        private ServiceConfig _serviceConfig;
        private IConsulClient _consulClient;
        private string _registrationId;

        public ServiceDiscoveryHostedService(ServiceConfig serviceConfig, IConsulClient consulClient)
        {
            _serviceConfig = serviceConfig;
            _consulClient = consulClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registrationId = $"{_serviceConfig.ServiceName}-{_serviceConfig.ServiceId}";

            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = _serviceConfig.ServiceName,
                Address = _serviceConfig.ServiceAddress.Host,
                Port = _serviceConfig.ServiceAddress.Port
            };

            await _consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _consulClient.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}
