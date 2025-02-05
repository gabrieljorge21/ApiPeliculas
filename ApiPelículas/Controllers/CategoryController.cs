using ApiPelículas.Models;
using ApiPelículas.Models.Dtos;
using ApiPelículas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPelículas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetCategories()
        {
            var categoryList = _categoryRepository.GetAll();
            var categoryListDto = new List<CategoryDto>();

            foreach (var category in categoryList)
            {
                categoryListDto.Add(_mapper.Map<CategoryDto>(category));
            }
            return Ok(categoryListDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createCategoryDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.ExistsByName(createCategoryDto.Name))
            {
                ModelState.AddModelError("", $"'{createCategoryDto.Name}' already exists in categories DB.");
                return StatusCode(409, ModelState);
            }

            var category = _mapper.Map<Category>(createCategoryDto);
            
            
            if (!_categoryRepository.Create(category))
            {
                ModelState.AddModelError("", $"'Error saving {createCategoryDto.Name}' in DB.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { Id = category.Id }, category);
        }
    }
}
