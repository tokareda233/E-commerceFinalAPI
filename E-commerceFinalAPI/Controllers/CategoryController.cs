using AutoMapper;
using E_commerceFinalAPI.DTO.Category;
using E_commerceFinalAPI.Models;
using E_commerceFinalAPI.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var c = await _unit.Category.GetAll();
            var dto=_mapper.Map<List<GetCategoryDTO>>(c);
            return Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryDTO dto)
        {
            if(dto== null || !ModelState.IsValid) { return BadRequest(); }

            var categories=await _unit.Category.GetAll();
            var c = categories.Any(x => x.Name == dto.Name);

            if(c) { return BadRequest("Name must be Uniqe"); }

            var cat=_mapper.Map<Category>(dto);
            await _unit.Category.Add(cat);
           await _unit.SaveAsync();
            return Created();
        }
    }
}
