using BookLibrary.Core.Entities;

namespace BookLibrary.Core.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<IBook>> GetAll();
    Task<IBook?> GetById(int id);
    Task Add(IBook book);
    Task Update(IBook book);
    Task Delete(int id);
    Task<bool> Exists(int id);
}