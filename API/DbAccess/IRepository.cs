namespace API.DbAccess
{
    public interface IRepository<BaseModel>
    {
        IEnumerable<BaseModel> Get();
        BaseModel? GetById(int id);
        BaseModel Add(BaseModel model);
        BaseModel Update(BaseModel model);
        bool Delete(BaseModel model);
        bool DeleteById(int id);
    }
}