namespace API.DbAccess
{
    public interface IRepository<BaseModel>
    {
        IEnumerable<BaseModel> Get();
        BaseModel GetById(int id);
        void Add(BaseModel model);
        void Update(BaseModel model);
        void Delete(BaseModel model);
        void DeleteById(int id);
    }
}