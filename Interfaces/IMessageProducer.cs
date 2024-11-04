namespace Producer.Interfaces;

public interface IMessageProducer
{
    Task SendAsync<T>(T message);
}