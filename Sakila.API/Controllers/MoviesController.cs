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
                var allActors = await _actorRepository.GetAllActorsAsync();
                var allCategories = await _categoryRepository.GetAllCategoriesAsync();
                var allFilmActors = await _filmActorRepositroy.GetAllFilmActorsAsync();
                var allFilmCategories = await _filmCategoryRepository.GetFilmCategoriesAsync();
                var allFilms = await _filmRepository.GetAllFilmsAsync();
                var allLanguages = await _languageRepository.GetAllLanguagesAsync();

                var movieList = new List<MovieDTO>();

                foreach (var film in allFilms)
                {            
                    //Because of the relational table film_category between film and category,
                    //we do a little filtering to get only the categories 
                     
                    var categoryIds = allFilmCategories.Where(fc => fc.FilmId == film.FilmId).Select(c => c.CategoryId);
                    var filmCategories= allCategories.Where(c => categoryIds.Contains(c.CategoryId)).ToList();

                    var categoryList = new List<CategoryDTO>();

                    filmCategories.ForEach(c => categoryList.Add(new CategoryDTO { Name = c.Name }));

                    //filter actors same as we do categories
                    var actorIds = allFilmActors.Where(fa => fa.FilmId == film.FilmId).Select(a => a.ActorId);
                    var actors = allActors.Where(a => actorIds.Contains(a.ActorId)).ToList();

                    var actorList = new List<ActorDTO>();

                    actors.ForEach(a => actorList.Add(new() { FirstName = a.FirstName, LastName = a.LastName }));
                     
                    
                    movieList.Add(MapMovieDTO(film, actorList, categoryList, allLanguages));                    
                }

                return Ok(movieList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        private MovieDTO MapMovieDTO(Film film, IEnumerable<ActorDTO> actors, IEnumerable<CategoryDTO> categories,  IEnumerable<Language> languages)
        {
            var movie = new MovieDTO
            {
                Title = film.Title,
                Description = film.Description,
                ReleaseYear = film.ReleaseYear,
                Language = languages == default ? string.Empty : languages.FirstOrDefault(l => l.LanguageId == film.LanguageId).Name,
                OriginalLanguage = languages == default ? string.Empty : languages.FirstOrDefault(l => l.LanguageId == film.OriginalLanguageId)?.Name,
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
