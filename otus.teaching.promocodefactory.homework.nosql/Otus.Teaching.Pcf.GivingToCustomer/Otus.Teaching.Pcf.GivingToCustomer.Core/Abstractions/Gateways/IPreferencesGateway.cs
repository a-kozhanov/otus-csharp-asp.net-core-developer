using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Dto;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Gateways
{
    public interface IPreferencesGateway
    {
        Task<IEnumerable<PreferenceDto>> GetPreferencesAsync();
        
    }
}