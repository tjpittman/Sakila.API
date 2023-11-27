using Microsoft.AspNetCore.Mvc;

using Sakila.Core.Movies.DTOs;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.API.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFilmActorRepository _filmActorRepositroy;
        private readonly IFilmCategoryRepository _filmCategoryRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly ILanguageRepository _languageRepository;

        public MoviesController(IActorRepository actorRepository, ICategoryRepository categoryRepository, IFilmActorRepository filmActorRepository,
                                IFilmCategoryRepository filmCategoryRepository, IFilmRepository filmRepository, ILanguageRepository languageRepository)
        {
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            _filmActorRepositroy = filmActorRepository;
            _filmCategoryRepository = filmCategoryRepository;
            _filmRepository = filmRepository;
            _languageRepository = languageRepository;
        }
        
        [HttpGet("get-all-movies")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAllMovies()
        {
            try
            {
                var allFilms = await _filmRepository.GetAllFilmsAsync();

                var movieList = new List<MovieDTO>();
                
                foreach (var film in allFilms)
                {            
                    var filmCategoriesList = await _filmCategoryRepository.GetFilmCategoriesByFilmIdAsync(film.FilmId);
                    
                    var categoryList = await MapCategoryDtoList(filmCategoriesList);

                    var filmActorList = await _filmActorRepositroy.GetFilmActorByFilmIdAsync(film.FilmId);
                    var actorList = await MapActorDtoList(filmActorList);
                    
                    movieList.Add(await MapMovieDTO(film, actorList, categoryList));                    
                }

                return Ok(movieList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-movie/{filmId}")]
        public async Task<IActionResult> GetMovieByFilmId(int filmId)
        {
            try
            {
                var film = await _filmRepository.GetFilmByFilmIdAsync(filmId);

                if (film == null)
                    return NotFound(filmId);

                var filmCategories = (await _filmCategoryRepository.GetFilmCategoriesByFilmIdAsync(filmId)).ToList();
                
                var filmActors = await _filmActorRepositroy.GetFilmActorByFilmIdAsync(filmId);

                var allActors = await MapActorDtoList(filmActors);
                var allCategories = await MapCategoryDtoList(filmCategories);

                var movieDto = await MapMovieDTO(film, allActors, allCategories);

                return Ok(movieDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<MovieDTO> MapMovieDTO(Film film, IEnumerable<ActorDTO> actors, IEnumerable<CategoryDTO> categories)
        {
            var language = await _languageRepository.GetLanguageByLanguageIdAsync(film.LanguageId);

            var originalLanguage = await _languageRepository.GetLanguageByLanguageIdAsync(film.OriginalLanguageId);

            var movie = new MovieDTO
            {
                Title = film.Title,
                Description = film.Description,
                ReleaseYear = film.ReleaseYear,
                Language = language.Name,
                OriginalLanguage = originalLanguage?.Name,
                RentalDuration = film.RentalDuration,
                RentalRate = film.RentalRate,
                Length = film.Length,
                Rating = film.Rating,
                SpecialFeatures = film.SpecialFeatures,
                Actors = actors,
                Categories = categories
            };
            
            return movie;
        }
        
        private async Task<IEnumerable<CategoryDTO>> MapCategoryDtoList(List<FilmCategory> filmCategories)
        {
            var allCategoryDTOs = new List<CategoryDTO>();
             
            foreach (var fc in filmCategories)
            {
                var category = await _categoryRepository.GetCategoryByCategoryIdAsync(fc.CategoryId);

                allCategoryDTOs.Add(new() {Name = category.Name});
            }

            return allCategoryDTOs.AsEnumerable();
        }

        private async Task<IEnumerable<ActorDTO>> MapActorDtoList(List<FilmActor> filmActors)
        {
            var allActorDTOs = new List<ActorDTO>();

            foreach (var fa in filmActors)
            {
                var actor = await _actorRepository.GetActorByIdAsync(fa.ActorId);

                allActorDTOs.Add(new() { FirstName = actor.FirstName, LastName = actor.LastName });
            }

            return allActorDTOs.AsEnumerable();
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
        #endregion
    }
}
