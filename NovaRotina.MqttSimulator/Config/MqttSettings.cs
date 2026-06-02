namespace NovaRotina.MqttSimulator.Config;

public static class MqttSettings
{
    public const string Broker = "c9e410c37105493594c09032bcfd99b0.s1.eu.hivemq.cloud";

    public const int Port = 8883;

    public const string Username = "Admin";

    public const string Password = "Senai2026@";

    public const string TemperatureTopic = "sensores/temperatura";

    public const string HumidityTopic = "sensores/umidade";

    public const string ProximityTopic = "sensores/proximidade";
    public const string StartTopic = "controle/start";

    public const string StopTopic = "controle/stop";
}
