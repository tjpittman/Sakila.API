using AutoMapper;

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
        private readonly IFilmRepository _filmRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IFilmActorRepository _filmActorRepositroy;
        private readonly IFilmCategoryRepository _filmCategoryRepository;
        private readonly IMapper _mapper;
        public MoviesController(IActorRepository actorRepository, ICategoryRepository categoryRepository, IFilmActorRepository filmActorRepository,
                                IFilmCategoryRepository filmCategoryRepository, IFilmRepository filmRepository, ILanguageRepository languageRepository,
                                IMapper mapper)
        {
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            _filmActorRepositroy = filmActorRepository;
            _filmCategoryRepository = filmCategoryRepository;
            _filmRepository = filmRepository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        #region Movies
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

                    var filmActorList = await _filmActorRepositroy.GetFilmActorsByFilmIdAsync(film.FilmId);
                    var actorList = await MapActorDtoList(filmActorList.ToList());
                    
                    movieList.Add(await MapMovieDto(film, actorList, categoryList));                    
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
                
                var filmActors = await _filmActorRepositroy.GetFilmActorsByFilmIdAsync(filmId);

                var allActors = await MapActorDtoList(filmActors.ToList());
                var allCategories = await MapCategoryDtoList(filmCategories);

                var movieDto = await MapMovieDto(film, allActors, allCategories);

                return Ok(movieDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Mapping methods
        private async Task<MovieDTO> MapMovieDto(Film film, IEnumerable<ActorDTO> actors, IEnumerable<CategoryDTO> categories)
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


        private Actor MapActor(ActorDTO actorDto) => _mapper.Map<ActorDTO, Actor>(actorDto);
        private Film MapFilm(FilmDTO filmDto) => _mapper.Map<FilmDTO, Film>(filmDto);
        private FilmActor MapFilmActor(FilmActorDTO filmActorDto) => _mapper.Map<FilmActorDTO, FilmActor>(filmActorDto);
        private FilmCategory MapFilmCategory(FilmCategoryDTO filmCategoryDto) =>  _mapper.Map<FilmCategoryDTO, FilmCategory>(filmCategoryDto);
        private Category MapCategory(CategoryDTO categoryDto) => _mapper.Map<CategoryDTO, Category>(categoryDto);
        #endregion

        #region Actor

        [HttpGet("actor/get-all-actors")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetAllActors()
        {
            try
            {
                var actors = await _actorRepository.GetAllActorsAsync();

                return Ok(actors);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("actor/add-new-actor")]
        public async Task<IActionResult> AddNewActor([FromBody] ActorDTO actorDto)
        {
            try
            {
                var actor = MapActor(actorDto);

                var result = await _actorRepository.AddActorAsync(actor);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPut("actor/update-actor")]
        public async Task<IActionResult> UpdateActor([FromBody] ActorDTO actorDto)
        {
            try
            {
                var actor = MapActor(actorDto);

                var result = await _actorRepository.UpdateActorAsync(actor);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPut("actor/delete-actor")]
        public async Task<IActionResult> DeleteActor([FromBody] ActorDTO actorDto)
        {
            try
            {
                var actor = MapActor(actorDto);

                var result = await _actorRepository.DeleteActor(actor);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion  

        #region Film
        [HttpPost("film/add-film")]
        public async Task<IActionResult> AddFilmAsync([FromBody] FilmDTO filmDto)
        {
            try
            {
                var film = MapFilm(filmDto);
                
                var result = await _filmRepository.AddFilmAsync(film);

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("film/get-all-films")]
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

        [HttpGet("film/get-film/{filmId}")]
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

        [HttpDelete("film/delete-film")]
        public async Task<IActionResult> DeleteFilmByFilmIdAsync([FromBody] FilmDTO filmDto)
        {
            try
            {
                var film = MapFilm(filmDto);
                
                var result = await _filmRepository.DeleteFilmAsync(film);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("film/update-film")]
        public async Task<IActionResult> UpdateFilmAsync([FromBody] FilmDTO filmDto)
        {
            try
            {
                var film = MapFilm(filmDto);
                
                var result = await _filmRepository.UpdateFilmAsync(film);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region FilmCategory
        [HttpGet("film-categories/get-all-film-categories")]
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
        [HttpGet("film-categories/get-film-categories/film-id/{filmId}")]
        public async Task<IActionResult> GetFilmCategoriesByFilmId(int filmId)
        {
            try
            {
                var result = await _filmCategoryRepository.GetFilmCategoriesByFilmIdAsync(filmId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
        [HttpGet("film-categories/get-film-categories/category-id/{categoryId}")]
        public async Task<IActionResult> GetFilmCategoriesByCategoryId(int categoryId)
        {
            try
            {
                var result = await _filmCategoryRepository.GetFilmCategoriesByCategoryIdAsync(categoryId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost("film-categories/add-new-film-category/{filmId}/{categoryId}")]
        public async Task<IActionResult> AddNewFilmCategory(FilmCategoryDTO filmCategoryDto)
        {
            try
            {
                var filmCategory = MapFilmCategory(filmCategoryDto);

                var result = await _filmCategoryRepository.AddFilmCategoryAsync(filmCategory);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("film-categories/delete-film-category")]
        public async Task<IActionResult> DeleteFilmCategory(FilmCategoryDTO filmCategoryDto)
        {
            try
            {
                var filmCategory = MapFilmCategory(filmCategoryDto);

                var result = await _filmCategoryRepository.DeleteFilmCategoryAsync(filmCategory);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        #endregion

        #region FilmActor

        [HttpGet("film-actor/get-all-film-actors")]
        public async Task<ActionResult<IEnumerable<FilmActorDTO>>> GetAllFilmActors()
        {
            try
            {
                var filmActors = await _filmActorRepositroy.GetAllFilmActorsAsync();
                
                return Ok(_mapper.Map<IEnumerable<FilmActor>, IEnumerable<FilmActorDTO>>(filmActors));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
        [HttpPost("film-actor/add-film-actor")]
        public async Task<IActionResult> AddFilmActor([FromBody] FilmActorDTO filmActorDto)
        {
            try
            {
                var filmActor = MapFilmActor(filmActorDto);

                var result = await _filmActorRepositroy.AddFilmActorAsync(filmActor);

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPatch("film-actor/update-film-actor")]
        public async Task<IActionResult> UpdateFilmActor([FromBody] FilmActorDTO filmActorDto)
        {
            try
            {
                var filmActor = MapFilmActor(filmActorDto);

                var result = await _filmActorRepositroy.UpdateFilmActorAsync(filmActor);

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("film-actor/delete-film-actor")]
        public async Task<IActionResult> DeleteFilmActor([FromBody] FilmActorDTO filmActorDto)
        {
            try
            {
                var filmActor = MapFilmActor(filmActorDto);

                var result = await _filmActorRepositroy.DeleteFilmActorAsync(filmActor);

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        #endregion

        #region Category

        [HttpPost("categories/add-new-category")]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var category = MapCategory(categoryDto);

                var result = await _categoryRepository.AddNewCategoryAsync(category);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("category/get-all-categories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();

                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        [HttpPut("categories/update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var category = MapCategory(categoryDto);

                var result = await _categoryRepository.UpdateCategoryAsync(category);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("categories/delete-category")]
        public async Task<IActionResult> DeleteCategory([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var category = MapCategory(categoryDto);

                var result = await _categoryRepository.DeleteCategoryAsync(category);

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
