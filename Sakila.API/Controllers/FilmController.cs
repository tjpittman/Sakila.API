using Microsoft.AspNetCore.Mvc;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.API.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;
        public FilmController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
        
        [HttpPost("add-film")]
        public async Task<IActionResult> AddFilmAsync([FromBody] Film film)
        {
            try
            {
                await _filmRepository.AddFilmAsync(film);

                return Ok(film);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e);
            }
        }

        [HttpGet("get-all-films")]
        public async Task<IActionResult> GetFilmsAsync()
        {
            try
            {
                var films = await _filmRepository.GetAllFilmsAsync();

                return Ok(films);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e);
            }
        }

        [HttpGet("get-film/{filmId}")]
        public async Task<IActionResult> GetFilmsByFilmIdAsync(int filmId)
        {
            try
            {
                var films = await _filmRepository.GetFilmByFilmIdAsync(filmId);

                if (films == null)
                    return BadRequest(filmId);

                return Ok(films);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e);
            }
        }

        [HttpDelete("delete-film")]
        public async Task<IActionResult> DeleteFilmByFilmIdAsync([FromBody] Film film)
        {
            try
            {
                return Ok(await _filmRepository.DeleteFilmAsync(film));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("delete-film/{filmId}")]
        public async Task<IActionResult> DeleteFilmByFilmIdAsync(int filmId)
        {
            try
            {
                var film = await _filmRepository.GetFilmByFilmIdAsync(filmId);

                return Ok(await _filmRepository.DeleteFilmAsync(film));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("update-film")]
        public async Task<IActionResult> UpdateFilmAsync(Film film)
        {
            try
            {
                return Ok(await _filmRepository.UpdateFilmAsync(film));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

