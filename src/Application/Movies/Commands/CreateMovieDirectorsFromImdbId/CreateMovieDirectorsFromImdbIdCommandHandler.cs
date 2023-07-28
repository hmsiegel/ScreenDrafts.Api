namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieDirectorsFromImdbId;
internal sealed class CreateMovieDirectorsFromImdbIdCommandHandler
    : ICommandHandler<CreateMovieDirectorsFromImdbIdCommand>
{
    private readonly IImdbService _imdbService;
    private readonly IMovieRepository _movieRepository;
    private readonly ICrewMemberRepository _crewMemberRepository;

    public CreateMovieDirectorsFromImdbIdCommandHandler(
        IImdbService imdbService,
        IMovieRepository movieRepository,
        ICrewMemberRepository crewMemberRepository)
    {
        _imdbService = imdbService;
        _movieRepository = movieRepository;
        _crewMemberRepository = crewMemberRepository;
    }

    public async Task<Result> Handle(CreateMovieDirectorsFromImdbIdCommand request, CancellationToken cancellationToken)
    {
        var imdbTitle = await _imdbService.GetMovieInformation(request.ImdbId, TitleOptions.FullCast);
        var movie = await _movieRepository.GetByImdbIdAsync(request.ImdbId);

        foreach (var crewMember in imdbTitle.DirectorList)
        {
            var existingCrewMember = await _crewMemberRepository.GetByImdbIdAsync(crewMember.Id, cancellationToken);

            if (existingCrewMember is null)
            {
                existingCrewMember = CrewMember.Create(
                    crewMember.Id,
                    crewMember.Name);

                _crewMemberRepository.Add(existingCrewMember);
            }

            await _movieRepository.AddCrewMemberAsync(movie, existingCrewMember, "Director");
        }

        return Result.Success();
    }
}
