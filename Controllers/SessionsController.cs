using AutoMapper;
using DragRacerApi.Entities;
using DragRacerApi.Models;
using DragRacerApi.Repositories.Session;
using DragRacerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DragRacerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionManagerService _sessionService;
        private readonly IMapper _mapper;

        public SessionsController(ISessionRepository sessionRepository, ISessionManagerService sessionService, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet("start/{id}")]
        public async Task<ActionResult> StartSession(int id)
        {
            try
            {
                await _sessionService.StartSession(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot start session" + ex.Message);
            }
        }

        [HttpGet("stop")]
        public async Task<ActionResult> StopSession()
        {
            try
            {
                await _sessionService.ManualStopSession();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot stop session" + ex.Message);
            }
        }

        [HttpGet("reset")]
        public async Task<ActionResult> ResetSession()
        {
            try
            {
                await _sessionService.ResetSession();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot reset session" + ex.Message);
            }
        }

        [HttpPost("registerAction")]
        public async Task<ActionResult> RegisterAction(StripActionDto session)
        {
            try
            {
                await _sessionService.RegisterAction(session);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot register action" + ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionResponseDto>>> GetSessions()
        {
            var sessions = await _sessionRepository.GetAllAsync();
            return _mapper.Map<List<SessionResponseDto>>(sessions);
        }

        [HttpGet("notStarted")]
        public async Task<ActionResult<IEnumerable<SessionResponseDto>>> GetSessionsNotStarted()
        {
            var sessions = await _sessionRepository.GetAllNotStarted();
            return _mapper.Map<List<SessionResponseDto>>(sessions);
        }

        [HttpGet("endedByDate")]
        public async Task<ActionResult<IEnumerable<SessionResponseDto>>> GetSessionsEnded()
        {
            var sessions = await _sessionRepository.GetAllEndedByDate();
            return _mapper.Map<List<SessionResponseDto>>(sessions);
        }

        [HttpGet("inProgress")]
        public ActionResult<SessionResponseDto> CheckIfSessionInUse()
        {
            var session = _sessionService.GetSessionInProggress();

            if (session == null)
            {
                return Ok(null);
            }

            return _mapper.Map<SessionResponseDto>(session);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionResponseDto>> GetSession(int id)
        {
            var session = await _sessionRepository.GetAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return _mapper.Map<SessionResponseDto>(session);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(int id, Session session)
        {
            if (id != session.Id)
            {
                return BadRequest();
            }

            try
            {
                _sessionRepository.Update(session);
                await _sessionRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await _sessionRepository.ExistsAsync(id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Session>> PostSession(RegisterSessionDto sessionDto)
        {
            var session = new Session()
            {
                Races = _mapper.Map<List<Race>>(sessionDto.RacerDtos)
            };
            await _sessionRepository.AddAsync(session);
            await _sessionRepository.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.Id }, session);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _sessionRepository.GetAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            await _sessionRepository.DeleteAsync(id);
            await _sessionRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
