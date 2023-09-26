using AutoMapper;
using DragRacerApi.Models;
using DragRacerApi.Repositories.Race;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragRacerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RacesController : ControllerBase
    {
        private readonly IRaceRepository _raceRepository;

        private readonly IMapper _mapper;

        public RacesController(IRaceRepository raceRepository, IMapper mapper)
        {
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceResponseDto>>> GetRaces()
        {
            var races = await _raceRepository.GetAllAsync();

            return _mapper.Map<List<RaceResponseDto>>(races);
        }

        [HttpGet("byBestTime")]
        public ActionResult<IEnumerable<RaceResponseDto>> GetRacesByBestTime()
        {
            var races = _raceRepository.GetRacesByBestTime();

            return _mapper.Map<List<RaceResponseDto>>(races);
        }
    }
}
