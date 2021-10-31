using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Dto;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration
{
    public class PreferencesGateway : IPreferencesGateway
    {
        private readonly HttpClient _httpClient;

        public PreferencesGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PreferenceDto>> GetPreferencesAsync()
        {
            var response = await _httpClient.GetStringAsync($"api/v1/preferences");
            return JsonSerializer.Deserialize<IEnumerable<PreferenceDto>>(response,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}