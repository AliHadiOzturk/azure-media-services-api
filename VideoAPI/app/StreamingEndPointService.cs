using System.Threading.Tasks;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Options;
using VideoAPI.Infrastructure;

namespace VideoAPI.app
{
    public class StreamingEndPointService
    {
        private readonly AzureConfiguration config;

        public StreamingEndPointService(IOptions<AzureConfiguration> config)
        {
            this.config = config.Value;
        }

        public async Task<StreamingEndpoint> GetStreamingEndPointAsync(IAzureMediaServicesClient client)
        {
            StreamingEndpoint streamingEndpoint = await client.StreamingEndpoints.GetAsync(config.ResourceGroup, config.AccountName, config.StreamingEndPointName);

            if (streamingEndpoint != null)
            {
                if (streamingEndpoint.ResourceState != StreamingEndpointResourceState.Running)
                {
                    await client.StreamingEndpoints.StartAsync(config.ResourceGroup, config.AccountName, config.StreamingEndPointName);
                }
            }

            return streamingEndpoint;
        }
    }
}