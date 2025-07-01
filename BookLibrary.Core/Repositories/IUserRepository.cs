using BookLibrary.Core.Entities;

namespace BookLibrary.Core.Repositories;

public interface IUserRepository
{
    Task<IUser?> GetByEmail(string email);
    Task Add(IUser user);
    Task<bool> EmailExists(string email);
    Task<IUser?> GetById(int id);
}