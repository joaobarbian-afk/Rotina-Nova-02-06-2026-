using MQTTnet;
using MQTTnet.Client;
using NovaRotina.MqttSimulator.Config;

namespace NovaRotina.MqttSimulator.Services;

public class MqttControlSubscriber
{
    private readonly IMqttClient _client;

    public bool IsRunning { get; private set; } = false;

    public MqttControlSubscriber()
    {
        var factory = new MqttFactory();

        _client = factory.CreateMqttClient();
    }

    public async Task ConnectAsync()
    {
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(MqttSettings.Broker, MqttSettings.Port)
            .WithCredentials(MqttSettings.Username, MqttSettings.Password)
            .WithTls()
            .Build();

        await _client.ConnectAsync(options);

        await _client.SubscribeAsync(MqttSettings.StartTopic);

        await _client.SubscribeAsync(MqttSettings.StopTopic);

        _client.ApplicationMessageReceivedAsync += e =>
        {
            var topic = e.ApplicationMessage.Topic;

            if (topic == MqttSettings.StartTopic)
            {
                IsRunning = true;

                Console.WriteLine("SIMULAÇÃO INICIADA");
            }

            if (topic == MqttSettings.StopTopic)
            {
                IsRunning = false;

                Console.WriteLine("SIMULAÇÃO PARADA");
            }

            return Task.CompletedTask;
        };
    }
}
