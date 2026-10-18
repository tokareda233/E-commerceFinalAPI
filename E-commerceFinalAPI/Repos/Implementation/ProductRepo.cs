using E_commerceFinalAPI.Data;
using E_commerceFinalAPI.Models;
using E_commerceFinalAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_commerceFinalAPI.Repos.Implementation
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> Pagnation(int? categoryid, string? sortby, int pagenumber = 1, int pagesize = 5)
        {
            var query =  _context.Products.AsQueryable();
            if (categoryid != null)
            {
                query=query.Where(c=>c.CategoryId==categoryid);
            }

            switch (sortby?.ToLower())
            {
                case "Name":
                    query=query.OrderBy(c=>c.Name);
                    break;

                case "StockQuantity":
                    query = query.OrderBy(c => c.StockQuantity);
                    break;

                case "Price":
                    query = query.OrderBy(c => c.Price);
                    break;

                default:
                    query = query.OrderBy(c => c.Id);
                    break;

            }

            return await query.Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();
        }

        public async Task<List<Product>> SearchByName(string name)
        {
           return await _context.Products.Where(c=>c.Name.Contains(name)).ToListAsync();
        }

        public async Task<Product> SecondMostExpensive(int categoryid)
        {
           var query= await _context.Products.Where(c=>c.CategoryId!=categoryid).ToListAsync();

            return  query.OrderByDescending(c => c.Price)
                .Skip(1)
                .FirstOrDefault();
        }
    }
}
