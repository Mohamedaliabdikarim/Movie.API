using Movie.Contracts.Services;
using Movie.Core.Contracts;

namespace Movie.Services.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IMovieService> _movieService;
    private readonly Lazy<IActorService> _actorService;
    private readonly Lazy<IReviewService> _reviewService;

    public ServiceManager(IUnitOfWork unitOfWork)
    {
        _movieService = new Lazy<IMovieService>(() => new MovieService(unitOfWork));
        _actorService = new Lazy<IActorService>(() => new ActorService(unitOfWork));
        _reviewService = new Lazy<IReviewService>(() => new ReviewService(unitOfWork));
    }

    public IMovieService MovieService => _movieService.Value;
    public IActorService ActorService => _actorService.Value;
    public IReviewService ReviewService => _reviewService.Value;
}