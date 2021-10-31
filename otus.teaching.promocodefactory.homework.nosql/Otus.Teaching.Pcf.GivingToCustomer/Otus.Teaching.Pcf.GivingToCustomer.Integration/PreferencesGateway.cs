using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Dto;

namespace Otus.Teaching.Pcf.GivingToCustomer.Integration
{
    public class PreferencesGateway: IPreferencesGateway
    {
        private readonly HttpClient _httpClient;

        public PreferencesGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PreferenceDto>> GetPreferencesAsync()
        {
            var response = await _httpClient.GetStringAsync($"api/v1/preferences");
            return JsonConvert.DeserializeObject<IEnumerable<PreferenceDto>>(response);
        }
        
    }
}