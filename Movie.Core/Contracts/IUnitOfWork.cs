namespace Movie.Core.Contracts;

public interface IUnitOfWork
{
    IMovieRepository Movies { get; }
    IActorRepository Actors { get; }
    IReviewRepository Reviews { get; }

    Task<int> CompleteAsync();
}