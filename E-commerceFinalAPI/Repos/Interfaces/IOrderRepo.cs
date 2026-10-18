using E_commerceFinalAPI.Models;

namespace E_commerceFinalAPI.Repos.Interfaces
{
    public interface IOrderRepo:IGenericRepo<Order>
    {

        Task<Order> GetOrderByIdWithDetails(int id);
        Task<List<Order>> GetAllWithDetails();
    }
}
