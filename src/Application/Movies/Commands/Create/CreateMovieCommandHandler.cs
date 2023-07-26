using ScreenDrafts.Api.Domain.MovieAggregate;

namespace ScreenDrafts.Api.Application.Movies.Commands.Create;
internal sealed class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand>
{
    private readonly IMovieRepository  _movieRepository;

    public CreateMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public Task<Result> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = Movie.Create(
            request.Title,
            request.Year,
            request.ImageUrl,
            request.ImdbUrl);

        _movieRepository.Add(movie);

        return Task.FromResult(Result.Success());
    }
}
