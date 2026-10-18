using E_commerceFinalAPI.Models;

namespace E_commerceFinalAPI.Repos.Interfaces
{
    public interface IProductRepo:IGenericRepo<Product>
    {
        Task<Product> SecondMostExpensive(int categoryid);
        Task<List<Product>> SearchByName(string name);
        Task<List<Product>> Pagnation(int ?categoryid,string ?sortby,int pagenumber=1,int pagesize=5);

    }
}
