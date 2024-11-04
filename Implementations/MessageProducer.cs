using Confluent.Kafka;
using Producer.Interfaces;

namespace Producer.Implementations;

public class MessageProducer : IMessageProducer
{
    private readonly IProducer<Null, string> _producer;
    
    public MessageProducer(IProducer<Null, string> producer)
    {
        _producer = producer;
    }

    public async Task SendAsync<T>(T message)
    {
        try
        {
            var result = await _producer.ProduceAsync("test-topic", new Message<Null, string> { Value = "Hello Kafka!" });
            Console.WriteLine($"Message '{result.Value}' sent to '{result.TopicPartitionOffset.Topic}', offset is {result.TopicPartitionOffset.Offset}");
            
            
        }
        catch (ProduceException<Null, string> e)
        {
            Console.WriteLine($"Failed to send message: {e.Error.Reason}");
        }
    }
}