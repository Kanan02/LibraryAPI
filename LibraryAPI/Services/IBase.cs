namespace LibraryAPI.Services
{
    public interface IBase<T,UT>
    {
        Task<T?> Get(int id);
        Task Add(T obj);
        Task Delete(T obj);
        Task<T> Update(int id, UT source);
    }
}
