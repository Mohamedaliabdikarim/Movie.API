using Movie.Core.Contracts;
using Movie.Data.Data;

namespace Movie.Data.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MovieContext _context;

    private IMovieRepository? _movies;
    private IActorRepository? _actors;
    private IReviewRepository? _reviews;

    public UnitOfWork(MovieContext context)
    {
        _context = context;
    }

    public IMovieRepository Movies => _movies ??= new MovieRepository(_context);

    public IActorRepository Actors => _actors ??= new ActorRepository(_context);

    public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}