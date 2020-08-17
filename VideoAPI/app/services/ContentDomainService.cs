using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Configuration;
using VideoAPI.app.models;
using VideoAPI.app.repositories;
using VideoAPI.Infrastructure;

namespace VideoAPI.app.services
{
    public class ContentDomainService
    {
        private readonly IConfiguration configuration;
        private readonly AssetService assetService;
        private readonly AzureService azureService;
        private readonly JobService jobService;
        private readonly StreamLocatorService streamLocatorService;
        private readonly TransformService transformService;
        private readonly IDocumentRepository documentRepository;
        private readonly IStreamingUrlRepository streamingUrlRepository;
        private readonly StreamingEndPointService streamingEndPointService;

        public ContentDomainService(IConfiguration configuration,
                              AssetService assetService,
                              AzureService azureService,
                              JobService jobService,
                              StreamLocatorService streamLocatorService,
                              TransformService transformService,
                              IDocumentRepository documentRepository,
                              IStreamingUrlRepository streamingUrlRepository,
                              StreamingEndPointService streamingEndPointService)
        {
            this.configuration = configuration;
            this.assetService = assetService;
            this.azureService = azureService;
            this.jobService = jobService;
            this.streamLocatorService = streamLocatorService;
            this.transformService = transformService;
            this.documentRepository = documentRepository;
            this.streamingUrlRepository = streamingUrlRepository;
            this.streamingEndPointService = streamingEndPointService;
        }

        private async Task<Transform> CheckTranform(IAzureMediaServicesClient client)
        {
            // Ensure that you have the desired encoding Transform. This is really a one time setup operation.
            Transform transform = await transformService.GetOrCreateTransformAsync(client);
            return transform;
        }

        public async Task UploadDocument(Document document)
        {
            string uniqueness = $"{document.FileName}-{Convert.ToBase64String(Guid.NewGuid().ToByteArray())}";
            string jobName = $"{uniqueness}-job";
            string locatorName = $"{uniqueness}-locator";
            string outputAssetName = $"{uniqueness}-output";
            string inputAssetName = $"{uniqueness}-input";

            IAzureMediaServicesClient client = await azureService.CreateMediaServicesClientAsync();
            client.LongRunningOperationRetryTimeout = 2;
            try
            {
                await CheckTranform(client);

                // Create a new input Asset and upload the specified local video file into it.
                Asset uploadedAsset = await assetService.CreateInputAssetAsync(client, inputAssetName, document.Path);

                // Output from the encoding Job must be written to an Asset, so let's create one
                Asset outputAsset = await assetService.CreateOutputAssetAsync(client, outputAssetName);
                document.AssetName = uploadedAsset.Name;
                document.EncodedAssetName = outputAsset.Name;
                document.EncodeJobName = jobName;
                document.LocatorName = locatorName;

                document = await Task.Run(() => documentRepository.Update(document));

                Job job = await EncodeDocument(client, jobName, inputAssetName, outputAsset.Name);
            }
            catch (Exception exception)
            {
                if (exception.Source.Contains("ActiveDirectory"))
                {
                    Console.Error.WriteLine("TIP: Make sure that you have filled out the appsettings.json file before running this sample.");
                }

                Console.Error.WriteLine($"{exception.Message}");

                ApiErrorException apiException = exception.GetBaseException() as ApiErrorException;
                if (apiException != null)
                {
                    Console.Error.WriteLine(
                        $"ERROR: API call failed with error code '{apiException.Body.Error.Code}' and message '{apiException.Body.Error.Message}'.");
                }
            }






        }

        public async Task<Job> EncodeDocument(IAzureMediaServicesClient client, String jobName, string inputAssetName, string outputAssetName)
        {
            Job job = await jobService.SubmitJobAsync(client, jobName, inputAssetName, outputAssetName);
            // job = await jobService.WaitForJobToFinishAsync(client, jobName);
            return job;
        }

        public async Task PublishDocument(Document document)
        {
            try
            {
                IAzureMediaServicesClient client = await azureService.CreateMediaServicesClientAsync();
                client.LongRunningOperationRetryTimeout = 2;
                // Job job = null;
                // do
                // {
                // await EncodeDocument(client, document.EncodeJobName, document.AssetName, document.EncodedAssetName);
                Job job = await jobService.CheckJobExistAndFinished(client, document.EncodeJobName);
                StreamingLocator locator = await streamLocatorService.GetStreamingLocatorAsync(client, document.LocatorName);
                if (locator == null)
                    locator = await streamLocatorService.CreateStreamingLocatorAsync(client, document.EncodedAssetName, document.LocatorName);

                var urls = await streamLocatorService.GetStreamingUrlsAsync(client, locator.Name);

                StreamingEndpoint streamingEndpoint = await streamingEndPointService.GetStreamingEndPointAsync(client);
                // Document doc = new Document();
                foreach (var item in urls.StreamingPaths)
                {
                    var streamUrl = new StreamingUrl();
                    StreamProtocol protocol = StreamProtocol.EMPTY;
                    if (item.StreamingProtocol == StreamingPolicyStreamingProtocol.Hls)
                        protocol = StreamProtocol.HLS;
                    else if (item.StreamingProtocol == StreamingPolicyStreamingProtocol.Dash)
                        protocol = StreamProtocol.DASH;
                    else if (item.StreamingProtocol == StreamingPolicyStreamingProtocol.SmoothStreaming)
                        protocol = StreamProtocol.SmoothStreaming;

                    UriBuilder uriBuilder = new UriBuilder();
                    uriBuilder.Scheme = "https";
                    uriBuilder.Host = streamingEndpoint.HostName;
                    uriBuilder.Path = item.Paths.Count > 0 ? item.Paths[0] : null;
                    streamUrl.Url = uriBuilder.ToString();
                    streamUrl.StreamingProtocol = protocol;

                    streamUrl = await Task.Run(() => streamingUrlRepository.Save(streamUrl));
                    DocumentStreamingUrl documentStreamingUrl = new DocumentStreamingUrl()
                    {
                        Document = document,
                        DocumentId = document.Id,
                        StreamingUrl = streamUrl,
                        StreamingUrlId = streamUrl.Id
                    };

                    document.StreamingUrls.Add(documentStreamingUrl);
                    documentRepository.Update(document);
                }
                Console.WriteLine("Done. Copy and paste the Streaming URL into the Azure Media Player at 'http://aka.ms/azuremediaplayer'.");
                // } while (job != null && job.State != JobState.Finished && job.State != JobState.Canceling && job.State != JobState.Error);



            }
            catch (Exception exception)
            {
                Console.Error.WriteLine($"{exception.Message}");

                ApiErrorException apiException = exception.GetBaseException() as ApiErrorException;
                if (apiException != null)
                {
                    Console.Error.WriteLine(
                        $"ERROR: API call failed with error code '{apiException.Body.Error.Code}' and message '{apiException.Body.Error.Message}'.");
                }
            }
        }
    }
}