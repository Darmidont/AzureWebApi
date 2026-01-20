namespace Logic.Interfaces.Interfaces
{
    public interface IAzureTopicService
    {
        Task SendMessageAsync(string jsonMessage, string topicName, string subscriptionName, CancellationToken cancellationToken); }
}
