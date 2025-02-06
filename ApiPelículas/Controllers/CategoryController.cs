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
            var categoryList = _categoryRepository.GetAllCategories();
            var categoryListDto = new List<CategoryDto>();

            foreach (var category in categoryList)
            {
                categoryListDto.Add(_mapper.Map<CategoryDto>(category));
            }
            return Ok(categoryListDto);
        }

        [HttpGet("{categoryId:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int categoryId)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);

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
                ModelState.AddModelError("", $"'Error saving {category.Name}' in DB.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { Id = category.Id }, category);
        }

        [HttpPatch("{categoryId:int}", Name = "UpdatePatchCatergory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdatePatchCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoryDto == null  || categoryId != categoryDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.GetCategoryById(categoryId) == null)
            {
                return NotFound($"Categori with ID {categoryId} was not found.");
            }

            var category = _mapper.Map<Category>(categoryDto);


            if (!_categoryRepository.Update(category))
            {
                ModelState.AddModelError("", $"'Error saving changes in {category.Name}' in DB.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut("{categoryId:int}", Name = "UpdatePutCatergory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdatePutCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoryDto == null  || categoryId != categoryDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.GetCategoryById(categoryId) == null)
            { 
                return NotFound($"Categori with ID {categoryId} was not found.");
            }

            var category = _mapper.Map<Category>(categoryDto);


            if (!_categoryRepository.Update(category))
            {
                ModelState.AddModelError("", $"'Error saving changes in {category.Name}' in DB.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId:int}", Name = "DeleteCatergory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.ExistsById(categoryId))
            {
                return NotFound($"Category with ID {categoryId} not found.");
            }

            var category = _categoryRepository.GetCategoryById(categoryId);
            if (!_categoryRepository.Delete(category))
            {
                ModelState.AddModelError("", $"Could not remove category ID {categoryId}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}