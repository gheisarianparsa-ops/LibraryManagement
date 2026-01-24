using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.CategoryModels;
using LibraryManagementApi.Models.ProductModels;
using LibraryManagementApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<CategoryModel, CategoryReadDto, CategoryUpdateDto, CategoryCreateDto> _categoryrepository;
        public CategoryController(IGenericRepository<CategoryModel, CategoryReadDto, CategoryUpdateDto, CategoryCreateDto> repository)
        {
            _categoryrepository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<CategoryReadDto>> GetAllCategory()
        {
            var Categories = await _categoryrepository.GetAllAsync();
            return Ok(Categories);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategory(int id)
        {
            var category = await _categoryrepository.GetById(id);
            if (category is not null)
            {
                return Ok(category);
            }
            return NotFound();
        }

        // POST: categoryController/Create
        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> Create(CategoryCreateDto entity)
        {
            var category = await _categoryrepository.CreateAsync(entity);
            if (category is not null)
            {
                return Ok(category);
            }
            return BadRequest();
        }



        // PUT: categoryController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryReadDto>> Edit(int id, CategoryUpdateDto entity)
        {

            var updatedcategory = await _categoryrepository.UpdateAsync(id, entity);
            if (updatedcategory == null)
                return NotFound();

            return Ok(updatedcategory);
        }

        // POST: categoryController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _categoryrepository.IsExist(id);
            if (!exist)
                return NotFound();

            await _categoryrepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
