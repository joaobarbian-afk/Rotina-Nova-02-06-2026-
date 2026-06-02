using NovaRotina.MqttSimulator.Config;
using NovaRotina.MqttSimulator.Services;

var mqttPublisher = new MqttPublisher();

await mqttPublisher.ConnectAsync();

var sensorSimulator = new SensorSimulator();

var controlSubscriber = new MqttControlSubscriber();

await controlSubscriber.ConnectAsync();

Console.WriteLine("Aguardando comando START...");

while (true)
{
    if (controlSubscriber.IsRunning)
    {
        var temperature = sensorSimulator.GetTemperature();

        var humidity = sensorSimulator.GetHumidity();

        var proximity = sensorSimulator.GetProximity();

        await mqttPublisher.PublishAsync(MqttSettings.TemperatureTopic, temperature.ToString());

        await mqttPublisher.PublishAsync(MqttSettings.HumidityTopic, humidity.ToString());

        await mqttPublisher.PublishAsync(MqttSettings.ProximityTopic, proximity.ToString());

        Console.Clear();

        Console.WriteLine("NOVA ROTINA (COM MQTT)");

        Console.WriteLine("SIMULAÇÃO ATIVA");

        Console.WriteLine($"Temperatura: {temperature}°C");

        Console.WriteLine($"Umidade: {humidity}%");

        Console.WriteLine($"Proximidade: {proximity} cm");
    }

    await Task.Delay(3000);
}
