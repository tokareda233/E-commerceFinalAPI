namespace E_commerceFinalAPI.Repos.Interfaces
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);

        Task<List<TEntity>> GetAll();

        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
