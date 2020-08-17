using Microsoft.Azure.Management.Media;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAPI.Infrastructure
{
    /// <summary>
    /// Use for creating AzureMediaClient
    /// </summary>
    public class AzureService
    {
        private readonly IOptions<AzureConfiguration> config;

        public AzureService(IOptions<AzureConfiguration> config)
        {
            this.config = config;
        }
        /// <summary>
        /// Create the ServiceClientCredentials object based on the credentials
        /// supplied in local configuration file.
        /// </summary>
        /// <param name="config">The parm is of type ConfigWrapper. This class reads values from local configuration file.</param>
        /// <returns></returns>
        // <GetCredentialsAsync>
        private async Task<ServiceClientCredentials> GetCredentialsAsync()
        {
            // Use ApplicationTokenProvider.LoginSilentWithCertificateAsync or UserTokenProvider.LoginSilentAsync to get a token using service principal with certificate
            //// ClientAssertionCertificate
            //// ApplicationTokenProvider.LoginSilentWithCertificateAsync

            // Use ApplicationTokenProvider.LoginSilentAsync to get a token using a service principal with symetric key
            ClientCredential clientCredential = new ClientCredential(config.Value.AadClientId, config.Value.AadSecret);
            return await ApplicationTokenProvider.LoginSilentAsync(config.Value.AadTenantId, clientCredential, ActiveDirectoryServiceSettings.Azure);
        }
        // </GetCredentialsAsync>

        /// <summary>
        /// Creates the AzureMediaServicesClient object based on the credentials
        /// supplied in local configuration file.
        /// </summary>
        /// <param name="config">The parm is of type ConfigWrapper. This class reads values from local configuration file.</param>
        /// <returns></returns>
        // <CreateMediaServicesClient>
        public async Task<IAzureMediaServicesClient> CreateMediaServicesClientAsync()
        {
            var credentials = await GetCredentialsAsync();
            IAzureMediaServicesClient client = new AzureMediaServicesClient(new Uri(config.Value.ArmEndpoint), credentials)
            {
                SubscriptionId = config.Value.SubscriptionId,
            };
            // AzureMediaClient.client = client;
            return client;
        }
    }

}
