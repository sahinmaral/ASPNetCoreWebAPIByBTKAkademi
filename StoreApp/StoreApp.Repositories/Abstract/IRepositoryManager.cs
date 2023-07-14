namespace StoreApp.Repositories.Abstract
{
    public interface IRepositoryManager
    {
        IBookRepository BookRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
