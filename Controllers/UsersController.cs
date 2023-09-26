using AutoMapper;
using DragRacerApi.Entities;
using DragRacerApi.Models;
using DragRacerApi.Repositories.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DragRacerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        private readonly IMapper _mapper;

        public UsersController(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<List<UserResponseDto>>(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _userRepository.ExistsAsync(id))
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
