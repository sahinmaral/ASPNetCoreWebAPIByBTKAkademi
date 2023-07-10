namespace StoreApp.Repositories.Abstract
{
    public interface IRepositoryManager
    {
        IBookRepository BookRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
