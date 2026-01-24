using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<ProductModel, ProductReadDto, ProductUpdateDto, ProductCreateDto> _productrepository;
        public ProductController(IGenericRepository<ProductModel, ProductReadDto, ProductUpdateDto, ProductCreateDto> repository)
        {
            _productrepository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<ProductReadDto>> GetAllProduct()
        {
            var Products = await _productrepository.GetAllAsync();
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetProduct(int id)
        {
            var Product = await _productrepository.GetById(id);
            if (Product is not null)
            {
                return Ok(Product);
            }
            return NotFound();
        }

        // POST: ProductController/Create
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create(ProductCreateDto entity)
        {
            var Product = await _productrepository.CreateAsync(entity);
            if (Product is not null)
            {
                return Ok(Product);
            }
            return BadRequest();
        }



        // PUT: ProductController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductReadDto>> Edit(int id, ProductUpdateDto entity)
        {

            var updatedProduct = await _productrepository.UpdateAsync(id, entity);
            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        // POST: ProductController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _productrepository.IsExist(id);
            if (!exist)
                return NotFound();

            await _productrepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
