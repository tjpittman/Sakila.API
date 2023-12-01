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
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public MoviesController(IActorRepository actorRepository, ICategoryRepository categoryRepository,
            IFilmActorRepository filmActorRepository, IFilmCategoryRepository filmCategoryRepository, 
            IFilmRepository filmRepository, ILanguageRepository languageRepository,
            IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            _filmActorRepositroy = filmActorRepository;
            _filmCategoryRepository = filmCategoryRepository;
            _filmRepository = filmRepository;
            _languageRepository = languageRepository;
            _inventoryRepository = inventoryRepository;
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

        private async Task<MovieDTO> MapMovieDto(Film film, IEnumerable<ActorDTO> actors,
            IEnumerable<CategoryDTO> categories)
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

                allCategoryDTOs.Add(new() { Name = category.Name });
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

        //TODO: This is repetitive, could likely replace with generic types?
        private Actor MapActor(ActorDTO actorDto) => _mapper.Map<ActorDTO, Actor>(actorDto);
        private Film MapFilm(FilmDTO filmDto) => _mapper.Map<FilmDTO, Film>(filmDto);
        private FilmActor MapFilmActor(FilmActorDTO filmActorDto) => _mapper.Map<FilmActorDTO, FilmActor>(filmActorDto);
        private FilmCategory MapFilmCategory(FilmCategoryDTO filmCategoryDto) =>
            _mapper.Map<FilmCategoryDTO, FilmCategory>(filmCategoryDto);
        private Category MapCategory(CategoryDTO categoryDto) => _mapper.Map<CategoryDTO, Category>(categoryDto);
        public Language MapLanguage(LanguageDTO languageDto) => _mapper.Map<LanguageDTO, Language>(languageDto);
        public Inventory MapInventory(InventoryDTO inventoryDto) => _mapper.Map<InventoryDTO, Inventory>(inventoryDto);

        #endregion

        #region Actor
        [HttpPost("actor/add")]
        public async Task<IActionResult> AddNewActor(ActorDTO actorDto)
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
        [HttpGet("actor/get/all")]
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
        [HttpPut("actor/update")]
        public async Task<IActionResult> UpdateActor(ActorDTO actorDto)
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
        [HttpPut("actor/delete")]
        public async Task<IActionResult> DeleteActor(ActorDTO actorDto)
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

        [HttpPost("film/add")]
        public async Task<IActionResult> AddFilmAsync(FilmDTO filmDto)
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
        [HttpGet("film/get/all")]
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
        [HttpGet("film/get/film-id/{filmId}")]
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
        [HttpPut("film/update")]
        public async Task<IActionResult> UpdateFilmAsync(FilmDTO filmDto)
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
        [HttpDelete("film/delete")]
        public async Task<IActionResult> DeleteFilmByFilmIdAsync(FilmDTO filmDto)
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

        #endregion

        #region FilmCategory

        [HttpPost("film-category/add")]
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
        [HttpGet("film-category/get/all")]
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
        [HttpGet("film-category/get/film-id/{filmId}")]
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
        [HttpGet("film-category/get/category-id/{categoryId}")]
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
        [HttpDelete("film-category/delete")]
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

        [HttpPost("film-actor/add")]
        public async Task<IActionResult> AddFilmActor(FilmActorDTO filmActorDto)
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
        [HttpGet("film-actor/get/all")]
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
        [HttpPut("film-actor/update")]
        public async Task<IActionResult> UpdateFilmActor(FilmActorDTO filmActorDto)
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
        [HttpDelete("film-actor/delete")]
        public async Task<IActionResult> DeleteFilmActor(FilmActorDTO filmActorDto)
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

        [HttpPost("category/add")]
        public async Task<IActionResult> AddNewCategory(CategoryDTO categoryDto)
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
        [HttpGet("category/get/all")]
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
        [HttpPut("categories/update")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDto)
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
        [HttpDelete("category/delete")]
        public async Task<IActionResult> DeleteCategory(CategoryDTO categoryDto)
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

        #region Language

        [HttpPost("language/add")]
        public async Task<IActionResult> AddNewLanguage(LanguageDTO languageDto)
        {
            try
            {
                var language = MapLanguage(languageDto);

                var result = await _languageRepository.AddLanguageAsync(language);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("language/get/all")]
        public async Task<ActionResult<IEnumerable<LanguageDTO>>> GetAllLanguages()
        {
            try
            {
                var languages = await _languageRepository.GetAllLanguagesAsync();
                
                if (!languages.Any())
                    return NoContent();

                var languageDtos = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(languages);

                return Ok(languageDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut("language/update")]
        public async Task<IActionResult> UpdateLanguage(LanguageDTO languageDto)
        {
            try
            {
                var language = MapLanguage(languageDto);

                var result = await _languageRepository.UpdateLanguageAsync(language);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("language/delete")]
        public async Task<IActionResult> DeleteLanguage(LanguageDTO languageDto)
        {
            try
            {
                var language = MapLanguage(languageDto);

                var result = await _languageRepository.DeleteLanguageAsync(language);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        #endregion

        #region Inventory

        [HttpPost("inventory/add")]
        public async Task<IActionResult> AddInventory(InventoryDTO inventoryDto)
        {
            try
            {
                var inventory = MapInventory(inventoryDto);

                var result = await _inventoryRepository.AddInventoryAsync(inventory);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("inventory/get/all")]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetAllInventory()
        {
            try
            {
                var inventory = await _inventoryRepository.GetAllInventoryAsync();

                var inventoryDtos = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryDTO>>(inventory);

                return Ok(inventoryDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("inventory/get/film/{filmId}")]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetAllFilmInventory(int filmId)
        {
            try
            {
                var inventory = await _inventoryRepository.GetAllInventoryByFilmIdAsync(filmId);

                var inventoryDtos = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryDTO>>(inventory);

                return Ok(inventoryDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("inventory/get/store/{storeId}")]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetAllStoreInventory(int storeId)
        {
            try
            {
                var inventory = await _inventoryRepository.GetAllInventoryByStoreIdAsync(storeId);

                var inventoryDtos = _mapper.Map<IEnumerable<Inventory>, IEnumerable<InventoryDTO>>(inventory);

                return Ok(inventoryDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut("inventory/update")]
        public async Task<IActionResult> UpdateInventory(InventoryDTO inventoryDto)
        {
            try
            {
                var inventory = MapInventory(inventoryDto);

                var result = await _inventoryRepository.UpdateInventoryAsync(inventory);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("inventory/delete")]
        public async Task<IActionResult> DeleteInventory(InventoryDTO inventoryDto)
        {
            try
            {
                var inventory = MapInventory(inventoryDto);

                var result = await _inventoryRepository.DeleteInventoryAsync(inventory);

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

