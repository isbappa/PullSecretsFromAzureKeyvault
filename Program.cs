using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace PullSecretsFromAzureKeyvault;
static class Program
{
    static async Task Main(string[] args)
    {
        // Load the appsettings.json file
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        IConfiguration configuration = builder.Build();

        var spnConfig = configuration.GetSection(nameof(SpnConfiguration)).Get<SpnConfiguration>() ?? new SpnConfiguration();
        var credential = new ClientSecretCredential(spnConfig.TenantId, spnConfig.ClientId, spnConfig.ClientSecret);

        var keyVaultEndpoint = configuration["KeyVaultEndpoint"];
        if (string.IsNullOrEmpty(keyVaultEndpoint) || !Uri.IsWellFormedUriString(keyVaultEndpoint, UriKind.Absolute))
        {
            Console.WriteLine($"KeyVaultEndpoint is missing from the appsettings.json file or it is Invalid Key Vault endpoint: {keyVaultEndpoint}");
            return;
        }

        builder.AddAzureKeyVault(new Uri(keyVaultEndpoint), credential);

        // Get the Key Vault endpoint from the appsettings.json : "https://YourKeyVaultName.vault.azure.net/"

        configuration = builder.Build();

        // Now you can use the configuration object to get values from Azure Key Vault
        var secretValue = configuration["SecretKey1"];



        // Or you can use the SecretClient class to get the secret value

        var secretClient = new SecretClient(new Uri(keyVaultEndpoint), credential);

        // Get all secrets from the Key Vault
        await foreach (SecretProperties secretProperties in secretClient.GetPropertiesOfSecretsAsync())
        {
            if (secretProperties.Enabled == true)
            {
                // Get the secret value
                KeyVaultSecret secretWithValue = await secretClient.GetSecretAsync(secretProperties.Name);

                // Now you can use secretWithValue which is of type KeyVaultSecret
                Console.WriteLine($"Secret: {secretWithValue.Name}, Value: {secretWithValue.Value}");
            }
        }
    }
}