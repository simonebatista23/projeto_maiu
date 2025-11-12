using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginUserAdmin.Models;
using System.Net.Http.Json;


namespace LoginUserAdmin.Services
{
    class LoginAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly SettingsService _settingsService;

        public LoginAdminService(SettingsService settingsService)
        {
            _httpClient = new HttpClient();
            _settingsService = settingsService;
        }

        public async Task<HttpResponseMessage> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(_settingsService.BaseUrl) || string.IsNullOrWhiteSpace(_settingsService.LoginAdmin))
                throw new InvalidOperationException("Configuração de URL inválida.");

            var url = $"{_settingsService.BaseUrl}{_settingsService.LoginAdmin}";

            var loginData = new
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync(url, loginData);
            return response;
        }
    }
}
