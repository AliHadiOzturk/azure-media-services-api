using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Options;
using VideoAPI.Infrastructure;

namespace VideoAPI.app
{
    public class StreamLocatorService
    {
        private readonly AzureConfiguration config;

        public StreamLocatorService(IOptions<AzureConfiguration> config)
        {
            this.config = config.Value;
        }
        /// <summary>
        /// Creates a StreamingLocator for the specified asset and with the specified streaming policy name.
        /// Once the StreamingLocator is created the output asset is available to clients for playback.
        /// </summary>
        /// <param name="client">The Media Services client.</param>
        /// <param name="assetName">The name of the output asset.</param>
        /// <param name="locatorName">The StreamingLocator name (unique in this case).</param>
        /// <returns></returns>
        // <CreateStreamingLocator>
        public async Task<StreamingLocator> CreateStreamingLocatorAsync(
            IAzureMediaServicesClient client,
            string assetName,
            string locatorName)
        {
            StreamingLocator locator = await client.StreamingLocators.CreateAsync(
                config.ResourceGroup,
                config.AccountName,
                locatorName,
                new StreamingLocator
                {
                    AssetName = assetName,
                    StreamingPolicyName = PredefinedStreamingPolicy.ClearStreamingOnly
                });

            return locator;
        }

        /// <summary>
        /// Checks if the "default" streaming endpoint is in the running state,
        /// if not, starts it.
        /// Then, builds the streaming URLs.
        /// </summary>
        /// <param name="client">The Media Services client.</param>
        /// <param name="locatorName">The name of the StreamingLocator that was created.</param>
        /// <returns></returns>
        // <GetStreamingURLs>
        public async Task<ListPathsResponse> GetStreamingUrlsAsync(
            IAzureMediaServicesClient client,
            string locatorName)
        {
            string DefaultStreamingEndpointName = config.StreamingEndPointName;

            IList<string> streamingUrls = new List<string>();

            // StreamingEndpoint streamingEndpoint = await client.StreamingEndpoints.GetAsync(config.ResourceGroup, config.AccountName, DefaultStreamingEndpointName);

            // if (streamingEndpoint != null)
            // {
            //     if (streamingEndpoint.ResourceState != StreamingEndpointResourceState.Running)
            //     {
            //         await client.StreamingEndpoints.StartAsync(config.ResourceGroup, config.AccountName, DefaultStreamingEndpointName);
            //     }
            // }

            ListPathsResponse paths = await client.StreamingLocators.ListPathsAsync(config.ResourceGroup, config.AccountName, locatorName);

            foreach (StreamingPath path in paths.StreamingPaths)
            {
            }
            return paths;
        }

        ///<summary>
        ///Gets the locator with given name. If not found returns null
        ///</summary>
        public async Task<StreamingLocator> GetStreamingLocatorAsync(IAzureMediaServicesClient client, string locatorName)
        {
            return await client.StreamingLocators.GetAsync(config.ResourceGroup, config.AccountName, locatorName);
        }
    }
}