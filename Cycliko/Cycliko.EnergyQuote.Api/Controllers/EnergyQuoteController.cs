﻿using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Cycliko.EnergyQuote.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Cycliko.EnergyQuote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("cyclikoFixed")]
    public class EnergyQuoteController : ControllerBase
    {
        private readonly IEnergyQuoteService _energyService;

        public EnergyQuoteController(IEnergyQuoteService energyService)
        {
            _energyService = energyService;
        }

        [HttpGet]
        [Route("/")]
        public string GetEnergyWelcomeInfo()
        {
            return "Welcome to Cycliko Energy Quotes";
        }

        [HttpPost]
        [Authorize(Policy = "EnergyQuoteWritePolicy")]
        [ProducesResponseType<EnergyQuoteResponseDTO>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EnergyQuoteResponseDTO>> CreateEnergyQuoteAsync([FromBody] EnergyQuoteRequestDTO request)
        {
            var result = await _energyService.CreateEnergyQuoteAsync(request);
            return CreatedAtAction(nameof(GetEnergyQuoteAsync), new { id = result.Id }, result);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy = "EnergyQuoteReadPolicy")]
        [ProducesResponseType<EnergyQuoteResponseDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EnergyQuoteResponseDTO?>> GetEnergyQuoteAsync(string id)
        {
            var result = await _energyService.GetEnergyQuoteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}