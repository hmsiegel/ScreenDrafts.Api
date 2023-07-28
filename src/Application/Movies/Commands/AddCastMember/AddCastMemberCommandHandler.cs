namespace ScreenDrafts.Api.Application.Movies.Commands.AddCastMember;
internal sealed class AddCastMemberCommandHandler : ICommandHandler<AddCastMemberCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICastMemberRepository _castMemberRepository;

    public AddCastMemberCommandHandler(
        IMovieRepository movieRepository,
        ICastMemberRepository castMemberRepository)
    {
        _movieRepository = movieRepository;
        _castMemberRepository = castMemberRepository;
    }

    public async Task<Result> Handle(AddCastMemberCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.MovieId);

        if (movie is null)
        {
            return Result.Failure<Movie>(DomainErrors.Movie.NotFound);
        }

        var castMember = await _castMemberRepository.GetByCastMemberIdAsync(request.CastMemberId, cancellationToken);

        if (castMember is null)
        {
            return Result.Failure<CastMember>(DomainErrors.CastMember.NotFound);
        }

        await _movieRepository.AddCastMemberAsync(movie, castMember, request.RoleDescription);

        return Result.Success();
    }
}
