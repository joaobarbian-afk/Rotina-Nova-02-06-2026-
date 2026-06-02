namespace NovaRotina.MqttSimulator.Services;

public class SensorSimulator
{
    private readonly Random _random = new();

    public double GetTemperature()
    {
        return _random.Next(20, 40);
    }

    public double GetHumidity()
    {
        return _random.Next(30, 90);
    }

    public double GetProximity()
    {
        return _random.Next(0, 100);
    }
}
