using E_commerceFinalAPI.Data;
using E_commerceFinalAPI.Models;
using E_commerceFinalAPI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_commerceFinalAPI.Repos.Implementation
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllWithDetails()
        {
           return await _context.Orders.Include(c=>c.User).Include(x=>x.OrderItems).ThenInclude(x=>x.Product).ToListAsync();
        }

        public async Task<Order> GetOrderByIdWithDetails(int id)
        {
            return await _context.Orders.Where(x => x.Id == id).Include(c=>c.User).Include(c => c.OrderItems).ThenInclude(c => c.Product).FirstOrDefaultAsync();
        }
    }
}
