using AutoMapper;
using E_commerceFinalAPI.DTO.Category;
using E_commerceFinalAPI.DTO.Order;
using E_commerceFinalAPI.Models;
using E_commerceFinalAPI.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerceFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public OrderController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Add(CreateOrderDTO dto)
        {
            if (dto.OrderItems == null||!dto.OrderItems.Any()){ return BadRequest("the Order must have atleast one product "); }

            var userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var order = new Order
            {
                UserId=userid,
                TotalAmount=0,
                status="Pending",
                OrderDate=DateTime.Now,
                OrderItems=new List<OrderItem>()
               
            };

            foreach(var item in dto.OrderItems)
            {
                var product=await _unit.Product.GetById(item.ProductId);
                if (product == null) { return BadRequest("product not found"); }

                if (product.StockQuantity < item.Quantity) { return BadRequest("quantity not enough"); }

                var orderitem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };
              

                product.StockQuantity-=item.Quantity;
                order.TotalAmount += product.Price * item.Quantity;
                order.OrderItems.Add(orderitem);
                _unit.Product.Update(product);

            }

            await _unit.Order.Add(order);
            _unit.SaveAsync();

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var o = await _unit.Order.GetAllWithDetails();
      
            var dto = o.Select(x => new
            {
                OrderId = x.Id,
                TotalAmount = x.TotalAmount,
                Status = x.status,
                OrderDate = x.OrderDate,
                UserName = x.User.FullName,
                OrderItems = x.OrderItems.Select(x => new
                {
                    OrderItemId = x.Id,
                    ProductId=x.ProductId,
                    ProductName=x.Product.Name,
                    Quantity=x.Quantity,
                    UnitPrice=x.UnitPrice

                })

            }).ToList();
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var o = await _unit.Order.GetAllWithDetails();

            var dto = o.Select(x => new
            {
                OrderId = x.Id,
                TotalAmount = x.TotalAmount,
                Status = x.status,
                OrderDate = x.OrderDate,
                UserName = x.User.FullName,
                OrderItems = x.OrderItems.Select(x => new
                {
                    OrderItemId = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice

                })

            }).ToList();
            var order=dto.FirstOrDefault(x=>x.OrderId==id);
            return Ok(order);
        }

    }
}
