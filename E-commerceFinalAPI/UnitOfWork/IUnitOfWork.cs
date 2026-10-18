using E_commerceFinalAPI.Models;
using E_commerceFinalAPI.Repos.Interfaces;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace E_commerceFinalAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthRepo authRepo { get; }
        IGenericRepo<Category> Category {  get; }
        IProductRepo Product { get; }
        IOrderRepo Order { get; }
        void Save();
        Task SaveAsync();
    }
}
