using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.PreferencesDict.WebHost.Core.Abstraction;
using Otus.Teaching.Pcf.PreferencesDict.WebHost.Core.Domain;

namespace Otus.Teaching.Pcf.PreferencesDict.WebHost.Controllers
{
    /// <summary>
    /// Предпочтения клиентов
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferencesController : ControllerBase
    {
        private readonly IRepository<Preference> _preferencesRepository;

        public PreferencesController(IRepository<Preference> preferencesRepository)
        {
            _preferencesRepository = preferencesRepository;
        }

        /// <summary>
        /// Получить список предпочтений
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Preference>>> GetPreferencesAsync()
        {
            var preferences = await _preferencesRepository.GetAllAsync();

            return Ok(preferences);
        }
    }
}