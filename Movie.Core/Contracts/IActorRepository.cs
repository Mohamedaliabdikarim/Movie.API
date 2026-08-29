using Movie.Core.Entities;

namespace Movie.Core.Contracts;

public interface IActorRepository
{
    Task<IEnumerable<Actor>> GetAllAsync();
    Task<Actor?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(Actor actor);
    void Update(Actor actor);
    void Delete(Actor actor);
}