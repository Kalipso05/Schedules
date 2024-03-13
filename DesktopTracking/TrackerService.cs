using DataCenter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesktopTracking
{
    public class TrackerService
    {
        private readonly HttpClient _httpClient;
        private const string TrackApiUrl = "http://localhost:8080/api/track";

        public TrackerService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<SecurityAccessLog>> GetTrackingDataAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(TrackApiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                List<SecurityAccessLog> trackingData = JsonConvert.DeserializeObject<List<SecurityAccessLog>>(jsonResponse);
                return trackingData; // Возвращаем уже список объектов SecurityAccessLog
            }
            else
            {
                // Обработка ошибок или неудачных статусов ответа
                throw new HttpRequestException($"Failed to get tracking data: {response.ReasonPhrase}");
            }
        }

    }
}
