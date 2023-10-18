namespace Restaurant.MessageBus
{
    public interface IMessageBus
    {
        Task PublishedMessage(BaseMessage message, string topicName);
    }
}