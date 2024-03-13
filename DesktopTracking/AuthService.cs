using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;
using DataCenter.Model;

namespace DesktopTracking
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _authUrl = "http://localhost:8080/api/login";

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> AuthenticateUserAsync(string login, string password)
        {
            var user = new 
            {
                Login = login,
                Password = password
            };

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_authUrl, data);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

                if (authResponse.Message == "Authentication successful")
                {
                    return true;
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                MessageBox.Show(errorContent);
            }

            return false;
        }

    }

}
