using BookLibrary.Core.Entities;

namespace BookLibrary.Core.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<IReview>> GetByBookId(int bookId);
    Task<IReview?> GetById(int id);
    Task Add(IReview review);
    Task Update(IReview review);
    Task Delete(int id);
    Task<double> GetAverageRatingForBook(int bookId);
}