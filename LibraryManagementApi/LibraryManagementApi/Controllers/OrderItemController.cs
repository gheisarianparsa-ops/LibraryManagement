using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.OrderItemsModels;
using LibraryManagementApi.Models.ProductModels;
using LibraryManagementApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IGenericRepository<OrderItemModel, OrderItemReadDto, OrderItemUpdateDto, OrderItemCreateDto> _orderitemrepository;
        public OrderItemController(IGenericRepository<OrderItemModel, OrderItemReadDto, OrderItemUpdateDto, OrderItemCreateDto> repository)
        {
            _orderitemrepository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<OrderItemReadDto>> GetAllOrderItem()
        {
            var orderItems = await _orderitemrepository.GetAllAsync();
            return Ok(orderItems);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemReadDto>> GetOrderItem(int id)
        {
            var orderitem = await _orderitemrepository.GetById(id);
            if (orderitem is not null)
            {
                return Ok(orderitem);
            }
            return NotFound();
        }

        // POST: OrderItemController/Create
        [HttpPost]
        public async Task<ActionResult<OrderItemReadDto>> Create(OrderItemCreateDto entity)
        {
            var orderitem = await _orderitemrepository.CreateAsync(entity);
            if (orderitem is not null)
            {
                return Ok(orderitem);
            }
            return BadRequest();
        }



        // PUT: OrderItemController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItemReadDto>> Edit(int id, OrderItemUpdateDto entity)
        {

            var updatedOrderItem = await _orderitemrepository.UpdateAsync(id, entity);
            if (updatedOrderItem == null)
                return NotFound();

            return Ok(updatedOrderItem);
        }

        // POST: OrderItemController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _orderitemrepository.IsExist(id);
            if (!exist)
                return NotFound();

            await _orderitemrepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
