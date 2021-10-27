using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Preferences
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferencesContoller : ControllerBase
    {
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IMapper _mapper;

        public PreferencesContoller(IPreferenceRepository preferenceRepository, IMapper mapper)
        {
            _preferenceRepository = preferenceRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///Get all preferences
        /// </summary>
        /// <returns>List of preference response</returns>
        [HttpGet]
        public async Task<ActionResult<List<PreferenceResponse>>> GetCustomersAsync()
        {
            var preferences = await _preferenceRepository.GetAllAsync();

            return Ok(_mapper.Map<List<PreferenceResponse>>(preferences));
        }

    }
}
