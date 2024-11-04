using Avro.Specific;

namespace Producer.Interfaces;

public interface IMessageProducer
{
    Task SendAsync<T>(T message) where T : ISpecificRecord;
}