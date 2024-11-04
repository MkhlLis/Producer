using avro;
using Avro.IO;
using Avro.Specific;
using Confluent.Kafka;
using Producer.Interfaces;

namespace Producer.Implementations;

public class MessageProducer : IMessageProducer
{
    private readonly IProducer<Null, byte[]> _producer;
    
    public MessageProducer(IProducer<Null, byte[]> producer)
    {
        _producer = producer;
    }

    public async Task SendAsync<T>(T message) where T : ISpecificRecord
    {
        try
        {
            // Сериализация объекта User в байты с помощью Avro
            byte[] avroData;
            using (var ms = new MemoryStream())
            {
                var writer = new BinaryEncoder(ms);
                var datumWriter = new SpecificDatumWriter<T>(message.Schema);
                datumWriter.Write(message, writer);
                avroData = ms.ToArray();
            }
            
            var result = await _producer.ProduceAsync("test-topic", new Message<Null, byte[]> { Value = avroData });
            Console.WriteLine($"Message '{result.Value}' " +
                              $"sent to '{result.TopicPartitionOffset.Topic}', " +
                              $"offset is {result.TopicPartitionOffset.Offset}");
            
            
        }
        catch (ProduceException<Null, string> e)
        {
            Console.WriteLine($"Failed to send message: {e.Error.Reason}");
        }
    }
}