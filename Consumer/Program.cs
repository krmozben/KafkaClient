using System.Collections.Generic;
using Confluent.Kafka;



var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "foo",
    AutoOffsetReset = AutoOffsetReset.Earliest
};


using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{
    consumer.Subscribe("ElasticsearchDataTopic");

    while (true)
    {
        var consumeResult = consumer.Consume();
    }

    consumer.Close();
}