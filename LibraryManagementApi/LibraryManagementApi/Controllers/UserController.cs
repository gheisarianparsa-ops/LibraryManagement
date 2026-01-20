using LibraryManagementApi.Models.UserModels;
using LibraryManagementApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository repository)
        {
            _userRepository = repository;
        }

        // GET: UserController/Details/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<UserReadDto>> GetUser(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user is not null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST: UserController/Create
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> Create(UserCreateDto entity)
        {
            var user = await _userRepository.CreateAsync(entity);
            if (user is not null)
            {
                return Ok(user);
            }
            return BadRequest();
        }



        // PUT: UserController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserReadDto>> Edit(int id, UserUpdateDto entity)
        {
           
            var updatedUser = await _userRepository.UpdateAsync(id,entity);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        // POST: UserController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _userRepository.IsExist(id);
            if (!exist)
                return NotFound();

            await _userRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
