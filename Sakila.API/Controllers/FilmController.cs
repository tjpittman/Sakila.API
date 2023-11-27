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
        private readonly IFilmCategoryRepository _filmCategoryRepository;

        public FilmController(IFilmRepository filmRepository, IFilmCategoryRepository filmCategoryRepository)
        {
            _filmRepository = filmRepository;
            _filmCategoryRepository = filmCategoryRepository;
        }

        #region Film
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
        
        [HttpDelete("delete-film/{filmId}")]
        public async Task<IActionResult> DeleteFilmByFilmIdAsync(int filmId)
        {
            try
            {
                var film = await _filmRepository.GetFilmByFilmIdAsync(filmId);


                //TODO: Create custom exceptions for handling this scenario 
                //throw New FilmException("Film not found by FilmId"); or something...
                if (film == null)
                    return BadRequest(filmId);

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
        #endregion

        #region FilmCategory
        [HttpGet("categories/get-all-film-categories")]
        public async Task<IActionResult> GetAllFilmCategories()
        {
            try
            {
                var filmCategories = await _filmCategoryRepository.GetFilmCategoriesAsync();

                if (!filmCategories.Any())
                    return NoContent();

                return Ok(filmCategories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("categories/add-new-film-category/{filmId}/{categoryId}")]
        public async Task<IActionResult> AddNewFilmCategory(int filmId, int categoryId)
        {
            try
            {
                var filmCategory = new FilmCategory()
                {
                    FilmId = filmId,
                    CategoryId = categoryId,
                    LastUpdate = DateTime.Now
                };

                var result = await _filmCategoryRepository.AddFilmCategoryAsync(filmCategory);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("categories/delete-film-category/{filmId}/{categoryId}")]
        public async Task<IActionResult> DeleteFilmCategory(int filmId, int categoryId)
        {
            try
            {
                var filmCategory = await _filmCategoryRepository.GetFilmCategoryByFilmIdCategoryIdAsync(filmId, categoryId);

                //TODO: Create custom exceptions for handling this scenario  
                if (filmCategory == null)
                    return BadRequest();

                var result = _filmCategoryRepository.DeleteFilmCategoryAsync(filmCategory);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        } 
        #endregion
    }
}

