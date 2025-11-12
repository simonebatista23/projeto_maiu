using LoginUserAdmin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LoginUserAdmin.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
        }

        public async Task<List<TicketDto>> GetTicketsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Ticket/ativos");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<TicketDto>>();
        }
    }
}
