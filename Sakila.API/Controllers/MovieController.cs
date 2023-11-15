using Microsoft.AspNetCore.Mvc;
using Sakila.Core.Inventory.Movies.Interfaces;
using Sakila.Core.Inventory.Movies.Models;

namespace Sakila.API.Controllers
{
    [Route("silka/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    { 
        private readonly IFilmRepository _filmRepository;

        public MovieController( IFilmRepository filmRepository)
        { 
            _filmRepository = filmRepository;
        }

        [HttpPost]
        [Route("~/Film/AddFilm")]
        public async Task<IActionResult> AddFilmAsync(Film film)
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

        [HttpGet]
        [Route("~/Film/GetAllFilms")]
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

        [HttpGet]
        [Route("~/Film/GetFilm/{filmId}")]
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

        [HttpDelete]
        [Route("~/Film/DeleteFilm/{filmId}")]
        public async Task<IActionResult> DeleteFilmByFilmIdAsync(int filmId)
        {
            try
            {
                return Ok(await _filmRepository.DeleteFilmByFilmIdAsync(filmId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        [HttpPut]
        [Route("~/Film/Update/")]
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
