namespace API.DbAccess
{
    public interface IRepository<BaseModel>
    {
        IEnumerable<BaseModel> Get();
        BaseModel? GetById(int id);
        void Add(BaseModel model);
        void Update(BaseModel model);
        bool Delete(BaseModel model);
        bool DeleteById(int id);
    }
}