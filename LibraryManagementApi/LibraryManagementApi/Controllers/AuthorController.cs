using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.AuthorModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IGenericRepository<AuthorModel, AuthorReadDto, AuthorUpdateDto, AuthorCreateDto> _repository;
        public AuthorController(IGenericRepository<AuthorModel, AuthorReadDto, AuthorUpdateDto, AuthorCreateDto> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<AuthorReadDto>>> GetAllAsync()
        {
            var authors = await _repository.GetAllAsync();
            return Ok(authors);
        }
        [HttpGet("Id")]
        public async Task<ActionResult<AuthorReadDto>> GetAuthorById(int Id)
        {
            if (!await _repository.IsExist(Id))
                return NotFound();
            var author = await _repository.GetById(Id);
            return Ok(author);
        }
        [HttpPut("Id")]
        public async Task<ActionResult<AuthorReadDto>> Update(int Id, AuthorUpdateDto authorUpdate)
        {
            if (Id != authorUpdate.Id)
                return BadRequest();
            var UpdatedAuthor = _repository.UpdateAsync(authorUpdate);
            if (UpdatedAuthor == null)
                return NotFound();
            return Ok(await UpdatedAuthor);
        }
        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int Id)
        {
            bool IsExist = await _repository.IsExist(Id);
            if (!IsExist)
                return NotFound();
            await _repository.DeleteAsync(Id);
            return NoContent();
        }
    }
}
