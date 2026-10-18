using AutoMapper;
using E_commerceFinalAPI.DTO.Category;
using E_commerceFinalAPI.DTO.Product;
using E_commerceFinalAPI.Models;
using E_commerceFinalAPI.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var c = await _unit.Product.GetAll();
            var dto = _mapper.Map<List<GetProductDTO>>(c);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _unit.Product.GetById(id);
            if (c == null) { return NotFound("Product not found"); }
            var dto = _mapper.Map<GetProductDTO>(c);
            return Ok(dto);
        }



        [HttpPost]
        public async Task<IActionResult> Add(CreateProductDTO dto)
        {
            if (dto == null || !ModelState.IsValid) { return BadRequest(); }


            var cat = _mapper.Map<Product>(dto);
            await _unit.Product.Add(cat);
            await _unit.SaveAsync();
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id ,CreateProductDTO dto)
        {
            if (dto == null || !ModelState.IsValid) { return BadRequest(); }

            var p= await _unit.Product.GetById(id);
            if (p== null) { return NotFound("Product not found"); }

            _mapper.Map(dto, p);
            _unit.Product.Update(p);
            _unit.Save();

            return NoContent();

        }

        [HttpGet("pagnation")]
        public async Task<IActionResult> Pagnation(int? categoryid, string? sortby, int pagenumber = 1, int pagesize = 5)
        {
            var p=await _unit.Product.Pagnation(categoryid,sortby, pagenumber, pagesize);


            var dto = _mapper.Map<List<GetProductDTO>>(p);
            return Ok(dto);
        }


        [HttpGet("second-most-expensive")]
        public async Task<IActionResult> SecondExpensive(int categoryid)
        {
            var p=await _unit.Product.SecondMostExpensive(categoryid);
           
            var dto = _mapper.Map<GetProductDTO>(p);
            return Ok(dto);
        }


        [HttpPatch]
        public async Task<IActionResult> Patch(int id,int stockQuantity)
        {
            var p = await _unit.Product.GetById(id);
            if(p== null) { return NotFound("Product not found"); }

            p.StockQuantity=stockQuantity;
            _unit.Product.Update(p);
            _unit.Save();
           
            return NoContent();
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string Name)
        {
            var c = await _unit.Product.SearchByName(Name);
            if (c == null) { return NotFound("No Products Found"); }
            var dto = _mapper.Map<List<GetProductDTO>>(c);
            return Ok(dto);
        }
    }
}
