using Microsoft.EntityFrameworkCore;
using Movie.Core.Contracts;
using Movie.Core.Entities;
using Movie.Data.Data;

namespace Movie.Data.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly MovieContext _context;

    public ActorRepository(MovieContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        return await _context.Actors.ToListAsync();
    }

    public async Task<Actor?> GetByIdAsync(int id)
    {
        return await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Actors.AnyAsync(a => a.Id == id);
    }

    public async Task AddAsync(Actor actor)
    {
        await _context.Actors.AddAsync(actor);
    }

    public void Update(Actor actor)
    {
        _context.Actors.Update(actor);
    }

    public void Delete(Actor actor)
    {
        _context.Actors.Remove(actor);
    }
}