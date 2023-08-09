namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieProducersFromImdbId;
internal sealed class CreateMovieProducersFromImdbIdCommandHandler
    : ICommandHandler<CreateMovieProducersFromImdbIdCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICrewMemberRepository _crewMemberRepository;

    public CreateMovieProducersFromImdbIdCommandHandler(
        IMovieRepository movieRepository,
        ICrewMemberRepository crewMemberRepository)
    {
        _movieRepository = movieRepository;
        _crewMemberRepository = crewMemberRepository;
    }

    public async Task<Result> Handle(CreateMovieProducersFromImdbIdCommand request, CancellationToken cancellationToken)
    {
        var imdbTitle = request.TitleData;
        var movie = await _movieRepository.GetByImdbIdAsync(imdbTitle.Id, cancellationToken);

        foreach (var crewMember in imdbTitle.FullCast.Others.Where(x => x.Job.Contains("Produced", StringComparison.InvariantCultureIgnoreCase)))
        {
            var crewMemberId = crewMember.Items.Find(x => x.Id.StartsWith("nm", StringComparison.InvariantCultureIgnoreCase))?.Id;

            var existingCrewMember = await _crewMemberRepository.GetByImdbIdAsync(crewMemberId!, cancellationToken);

            if (existingCrewMember is null)
            {
                var crewMemberName = crewMember.Items.Find(x => x.Id.StartsWith("nm", StringComparison.InvariantCultureIgnoreCase))?.Name;

                existingCrewMember = CrewMember.Create(
                    crewMemberId!,
                    crewMemberName!);

                _crewMemberRepository.Add(existingCrewMember);
            }

            await _movieRepository.AddCrewMemberAsync(movie, existingCrewMember, "Producer", cancellationToken);
        }

        return Result.Success();
    }
}
