using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;

namespace Logic.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly SecretClient _secretClient;
    private readonly KeyVaultSettings _settings;

    public ConfigurationService(IOptions<KeyVaultSettings> options, SecretClient secretClient)
    {
        _secretClient = secretClient;
        _settings = options.Value;
    }

    public string GetSqlConnectionString()
    {
        KeyVaultSecret secret = _secretClient.GetSecret(_settings.SqlConnectionStringName);
        return secret.Value;
    }

    public string GetAzureOpenAiKey()
    {
        KeyVaultSecret secret = _secretClient.GetSecret(_settings.AzureOpenAiKeyName);
        return secret.Value;
    }

    public string GetMediatrKey()
    {
        KeyVaultSecret secret = _secretClient.GetSecret(_settings.MediatrKeyName);
        return secret.Value;
    }

    public string GetServiceBusConnectionString()
    {
        KeyVaultSecret secret = _secretClient.GetSecret(_settings.ServiceBusConnectionStringName);
        return secret.Value;
    }
}