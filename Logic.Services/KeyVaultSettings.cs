namespace Logic.Services;

public class KeyVaultSettings
{
    public string VaultUri { get; set; }
    public string AzureOpenAiKeyName { get; set; }
    public string SqlConnectionStringName { get; set; }
    public string MediatrKeyName { get; set; }
    public string ServiceBusConnectionStringName { get; set; }

}