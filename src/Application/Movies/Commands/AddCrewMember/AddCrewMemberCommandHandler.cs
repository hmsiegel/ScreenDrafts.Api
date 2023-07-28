namespace ScreenDrafts.Api.Application.Movies.Commands.AddCrewMember;
internal sealed class AddCrewMemberCommandHandler : ICommandHandler<AddCrewMemberCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICrewMemberRepository _crewMemberRepository;

    public AddCrewMemberCommandHandler(
        IMovieRepository movieRepository,
        ICrewMemberRepository crewMemberRepository)
    {
        _movieRepository = movieRepository;
        _crewMemberRepository = crewMemberRepository;
    }

    public async Task<Result> Handle(AddCrewMemberCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.MovieId);

        if (movie is null)
        {
            return Result.Failure<Movie>(DomainErrors.Movie.NotFound);
        }

        var crewMember = await _crewMemberRepository.GetByCrewMemberIdAsync(request.CrewMemberId, cancellationToken);

        if (crewMember is null)
        {
            return Result.Failure<CrewMember>(DomainErrors.CrewMember.NotFound);
        }

        await _movieRepository.AddCrewMemberAsync(
            movie,
            crewMember,
            request.CrewType.ToString());

        return Result.Success();
    }
}
