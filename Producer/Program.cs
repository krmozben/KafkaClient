using Confluent.Kafka;
using System.Net;



var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    while (true)
    {
        var message = Console.ReadLine();

        //  await producer.ProduceAsync("ElasticTopic", new Message<Null, string> { Value = message })
        /// Yukarıda ki kullanım da partitionlara message atamasını Zookeeper yönetir.

        await producer.ProduceAsync(new TopicPartition("ElasticTopic",new Partition(1)), new Message<Null, string> { Value = message })
        .ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Console.WriteLine(task.Exception.Message);
            }
            else
            {
                Console.WriteLine($"Wrote to offset: {task.Result.Offset}, partition: {task.Result.Partition}");
            }
        });

        if (message.ToLower().Equals("exit"))
            return;
    }
}
