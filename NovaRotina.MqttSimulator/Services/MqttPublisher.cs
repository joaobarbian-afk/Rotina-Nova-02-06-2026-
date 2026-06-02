using MQTTnet;
using MQTTnet.Client;
using NovaRotina.MqttSimulator.Config;

namespace NovaRotina.MqttSimulator.Services;

public class MqttPublisher
{
    private readonly IMqttClient _client;

    public MqttPublisher()
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

        Console.WriteLine("MQTT conectado com sucesso!");
    }

    public async Task PublishAsync(string topic, string payload)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .Build();

        await _client.PublishAsync(message);
    }
}
