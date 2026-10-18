using E_commerceFinalAPI.Data;
using E_commerceFinalAPI.Models;
using E_commerceFinalAPI.Repos.Interfaces;

namespace E_commerceFinalAPI.UnitOfWork
{
    public class UnitOfWorkk : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IAuthRepo authRepo {  get; }

        public IGenericRepo<Category> Category {  get; }

        public IProductRepo Product {  get; }

        public IOrderRepo Order { get; }

        public UnitOfWorkk(AppDbContext context, IAuthRepo authRepo, IGenericRepo<Category> category, IProductRepo product, IOrderRepo order)
        {
            _context = context;
            this.authRepo = authRepo;
            this.Category = category;
            Product = product;
            Order = order;
        }



        public void Save()
        {
           _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
