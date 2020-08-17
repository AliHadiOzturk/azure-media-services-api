using System;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Options;
using VideoAPI.Infrastructure;

namespace VideoAPI.app
{
    public class JobService
    {


        private readonly AzureConfiguration config;

        public JobService(IOptions<AzureConfiguration> config)
        {
            this.config = config.Value;
        }
        /// <summary>
        /// Submits a request to Media Services to apply the specified Transform to a given input video.
        /// </summary>
        /// <param name="client">AzureMediaClient</param>
        /// <param name="jobName">The (unique) name of the job.</param>
        /// <param name="inputAssetName">The name of the input asset.</param>
        /// <param name="outputAssetName">The (unique) name of the  output asset that will store the result of the encoding job. </param>
        // <SubmitJob>
        public async Task<Job> SubmitJobAsync(
            IAzureMediaServicesClient client,
            string jobName,
            string inputAssetName,
            string outputAssetName)
        {
            // Use the name of the created input asset to create the job input.
            JobInput jobInput = new JobInputAsset(assetName: inputAssetName);

            JobOutput[] jobOutputs =
            {
                new JobOutputAsset(outputAssetName),
            };

            // In this example, we are assuming that the job name is unique.
            //
            // If you already have a job with the desired name, use the Jobs.Get method
            // to get the existing job. In Media Services v3, the Get method on entities returns null 
            // if the entity doesn't exist (a case-insensitive check on the name).
            Job job = await client.Jobs.CreateAsync(
                config.ResourceGroup,
                config.AccountName,
                AzureConstants.AdaptiveStreamingTransformName,
                jobName,
                new Job
                {
                    Input = jobInput,
                    Outputs = jobOutputs,
                });

            return job;
        }

        /// <summary>
        /// Polls Media Services for the status of the Job.
        /// </summary>
        /// <param name="client">AzureMediaServicesClient</param>
        /// <param name="jobName">The name of the job you submitted.</param>
        /// <returns></returns>
        // <WaitForJobToFinish>
        public async Task<Job> WaitForJobToFinishAsync(
            IAzureMediaServicesClient client,
            string jobName)
        {
            const int SleepIntervalMs = 60 * 1000;

            Job job = null;

            do
            {
                job = await client.Jobs.GetAsync(config.ResourceGroup, config.AccountName, AzureConstants.AdaptiveStreamingTransformName, jobName);

                Console.WriteLine($"Job is '{job.State}'.");
                for (int i = 0; i < job.Outputs.Count; i++)
                {
                    JobOutput output = job.Outputs[i];
                    Console.Write($"\tJobOutput[{i}] is '{output.State}'.");
                    if (output.State == JobState.Processing)
                    {
                        Console.Write($"  Progress: '{output.Progress}'.");
                    }

                    Console.WriteLine();
                }

                if (job.State != JobState.Finished && job.State != JobState.Error && job.State != JobState.Canceled)
                {
                    await Task.Delay(SleepIntervalMs);
                }
            }
            while (job.State != JobState.Finished && job.State != JobState.Error && job.State != JobState.Canceled);

            return job;
        }

        public async Task<Job> CheckJobExistAndFinished(IAzureMediaServicesClient client, string jobName)
        {

            try
            {
                Job job = await client.Jobs.GetAsync(config.ResourceGroup, config.AccountName, AzureConstants.AdaptiveStreamingTransformName, jobName);
                return job;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}