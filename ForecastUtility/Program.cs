using ForecastUtility;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ForecastCompactResponse weather = await ForecastClient.GetForecast(11, 30);
        Console.WriteLine("Hello, World!");
    }
}