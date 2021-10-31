using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Dto;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Gateways
{
    public interface IPreferencesGateway
    {
        Task<IEnumerable<PreferenceDto>> GetPreferencesAsync();
    }
}