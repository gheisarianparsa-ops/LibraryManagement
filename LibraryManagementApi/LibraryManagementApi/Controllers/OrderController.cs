using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.OrderModels;
using LibraryManagementApi.Models.ProductModels;
using LibraryManagementApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IGenericRepository<OrderModel, OrderReadDto, OrderUpdateDto, OrderCreateDto> _orderRepository;
        public OrderController(IGenericRepository<OrderModel, OrderReadDto, OrderUpdateDto, OrderCreateDto> repository)
        {
            _orderRepository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<OrderReadDto>> GetAllOrders()
        {
            var Orders = await _orderRepository.GetAllAsync();
            return Ok(Orders);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetOrder(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order is not null)
            {
                return Ok(order);
            }
            return NotFound();
        }

        // POST: OrderController/Create
        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> Create(OrderCreateDto entity)
        {
            var order = await _orderRepository.CreateAsync(entity);
            if (order is not null)
            {
                return Ok(order);
            }
            return BadRequest();
        }



        // PUT: OrderController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderReadDto>> Edit(int id, OrderUpdateDto entity)
        {

            var updatedOrder = await _orderRepository.UpdateAsync(id, entity);
            if (updatedOrder == null)
                return NotFound();

            return Ok(updatedOrder);
        }

        // POST: OrderController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _orderRepository.IsExist(id);
            if (!exist)
                return NotFound();

            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
