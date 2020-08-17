using System.Threading.Tasks;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Options;
using VideoAPI.Infrastructure;

namespace VideoAPI.app
{
    public class TransformService
    {

        public IOptions<AzureConfiguration> config { get; }
        public TransformService(IOptions<AzureConfiguration> config)
        {
            this.config = config;
        }


        /// <summary>
        /// If the specified transform exists, get that transform.
        /// If the it does not exist, creates a new transform with the specified output. 
        /// In this case, the output is set to encode a video using one of the built-in encoding presets.
        /// </summary>
        /// <param name="client">The Media Services client.</param>
        /// <param name="resourceGroupName">The name of the resource group within the Azure subscription.</param>
        /// <param name="accountName"> The Media Services account name.</param>
        /// <param name="transformName">The name of the transform.</param>
        /// <returns></returns>
        // <EnsureTransformExists>
        public async Task<Transform> GetOrCreateTransformAsync(IAzureMediaServicesClient client)
        {
            // Does a Transform already exist with the desired name? Assume that an existing Transform with the desired name
            // also uses the same recipe or Preset for processing content.
            Transform transform = await client.Transforms.GetAsync(config.Value.ResourceGroup, config.Value.AccountName, AzureConstants.AdaptiveStreamingTransformName);

            if (transform == null)
            {
                // You need to specify what you want it to produce as an output
                TransformOutput[] output = new TransformOutput[]
                {
                    new TransformOutput
                    {
                        // The preset for the Transform is set to one of Media Services built-in sample presets.
                        // You can  customize the encoding settings by changing this to use "StandardEncoderPreset" class.
                        Preset = new BuiltInStandardEncoderPreset()
                        {
                            // This sample uses the built-in encoding preset for Adaptive Bitrate Streaming.
                            PresetName = EncoderNamedPreset.AdaptiveStreaming
                        }
                    }
                };
                // Create the Transform with the output defined above
                transform = await client.Transforms.CreateOrUpdateAsync(config.Value.ResourceGroup, config.Value.AccountName, AzureConstants.AdaptiveStreamingTransformName, output);
            }

            return transform;
        }
    }
}