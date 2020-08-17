using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using VideoAPI.app.models;
using VideoAPI.Infrastructure;

namespace VideoAPI.app
{
    public class AssetService
    {
        private readonly AzureConfiguration config;

        public AssetService(IOptions<AzureConfiguration> config)
        {
            this.config = config.Value;
        }
        /// <summary>
        /// Creates a new input Asset and uploads the specified local video file into it.
        /// </summary>
        /// <param name="client">AzureMediaClient</param>
        /// <param name="assetName">The asset name.</param>
        /// <param name="fileToUpload">The file you want to upload into the asset.</param>
        /// <returns></returns>
        // <CreateInputAsset>
        public async Task<Asset> CreateInputAssetAsync(IAzureMediaServicesClient client, string assetName, string fileToUpload)
        {
            // In this example, we are assuming that the asset name is unique.
            //
            // If you already have an asset with the desired name, use the Assets.Get method
            // to get the existing asset. In Media Services v3, the Get method on entities returns null 
            // if the entity doesn't exist (a case-insensitive check on the name).

            // Call Media Services API to create an Asset.
            // This method creates a container in storage for the Asset.
            // The files (blobs) associated with the asset will be stored in this container.
            Asset asset = await client.Assets.CreateOrUpdateAsync(config.ResourceGroup, config.AccountName, assetName, new Asset(name: fileToUpload, description: Path.GetFileName(fileToUpload)));

            // Use Media Services API to get back a response that contains
            // SAS URL for the Asset container into which to upload blobs.
            // That is where you would specify read-write permissions 
            // and the exparation time for the SAS URL.
            var response = await client.Assets.ListContainerSasAsync(
                config.ResourceGroup,
                config.AccountName,
                assetName,
                permissions: AssetContainerPermission.ReadWrite,
                expiryTime: DateTime.UtcNow.AddHours(4).ToUniversalTime());

            var sasUri = new Uri(response.AssetContainerSasUrls.First());

            // Use Storage API to get a reference to the Asset container
            // that was created by calling Asset's CreateOrUpdate method.  
            CloudBlobContainer container = new CloudBlobContainer(sasUri);
            var blob = container.GetBlockBlobReference(Path.GetFileName(fileToUpload));

            // Use Strorage API to upload the file into the container in storage.
            await blob.UploadFromFileAsync(fileToUpload);

            return asset;
        }

        /// <summary>
        /// Creates an ouput asset. The output from the encoding Job must be written to an Asset.
        /// </summary>
        /// <param name="client">AzureMediaClient</param>
        /// <param name="assetName">The output asset name.</param>
        /// <returns></returns>
        public async Task<Asset> CreateOutputAssetAsync(IAzureMediaServicesClient client, string assetName)
        {
            // Check if an Asset already exists
            Asset outputAsset = await client.Assets.GetAsync(config.ResourceGroup, config.AccountName, assetName);
            Asset asset = new Asset()
            {
                Description = $"Encoded - {assetName}"
            };
            string outputAssetName = assetName;

            // if (outputAsset != null)
            // {
            //     // Name collision! In order to get the sample to work, let's just go ahead and create a unique asset name
            //     // Note that the returned Asset can have a different name than the one specified as an input parameter.
            //     // You may want to update this part to throw an Exception instead, and handle name collisions differently.
            //     string uniqueness = $"-{Guid.NewGuid().ToString("N")}";
            //     outputAssetName += uniqueness;

            //     Console.WriteLine("Warning – found an existing Asset with name = " + assetName);
            //     Console.WriteLine("Creating an Asset with this name instead: " + outputAssetName);
            // }

            return await client.Assets.CreateOrUpdateAsync(config.ResourceGroup, config.AccountName, outputAssetName, asset);
        }

    }
}