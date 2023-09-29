using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForecastUtility
{
    public class ForecastClient
    {
        public static async Task<ForecastCompactResponse> GetForecast(double latitude, double longitude) {
            string apiUrl = "https://api.met.no/weatherapi/locationforecast/2.0/compact?" +
                "lat="+ latitude +
                "&lon=" + longitude; 

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "SkiStatusDemoApp kamilkubuszek@gmail.com");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializacja odpowiedzi do klasy Root
                        ForecastCompactResponse? weatherData = JsonSerializer.Deserialize<ForecastCompactResponse>(jsonResponse);

                        // Teraz możesz korzystać z obiektu "weatherData" do dostępu do danych z JSON

                        if (weatherData != null)
                        {
                            // Przykład dostępu do temperatury
                            double airTemperature = weatherData.feature.properties.timeseries[0].data.instant.details.air_temperature;
                            Console.WriteLine($"Temperatura: {airTemperature}°C");

                            // Przykład dostępu do wilgotności
                            double relativeHumidity = weatherData.feature.properties.timeseries[0].data.instant.details.relative_humidity;
                            Console.WriteLine($"Wilgotność: {relativeHumidity}%");
                        }
                        else 
                            Console.WriteLine("Couldnt serialize object");
                    }
                    else
                    {
                        Console.WriteLine($"Błąd: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }



            return new ForecastCompactResponse();
        }  
    }
}
