namespace API.Services
{
    public interface ICrud<TModel> where TModel : class
    {
        void Add(TModel model);
        bool DeleteById(int id);
        IEnumerable<TModel> Get();
        TModel? GetById(int id);
        void Update(TModel model);
    }
}