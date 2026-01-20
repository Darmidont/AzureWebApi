namespace Logic;

public interface IConfigurationService
{
    string GetSqlConnectionString();
    string GetAzureOpenAiKey();
    string GetMediatrKey();
}