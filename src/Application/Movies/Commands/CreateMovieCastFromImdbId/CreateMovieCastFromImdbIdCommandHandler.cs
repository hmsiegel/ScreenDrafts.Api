namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieCastFromImdbId;
internal sealed class CreateMovieCastFromImdbIdCommandHandler
    : ICommandHandler<CreateMovieCastFromImdbIdCommand>
{
    private readonly IImdbService _imdbService;
    private readonly IMovieRepository _movieRepository;
    private readonly ICastMemberRepository _castMemberRepository;

    public CreateMovieCastFromImdbIdCommandHandler(
        IImdbService imdbService,
        IMovieRepository movieRepository,
        ICastMemberRepository castMemberRepository)
    {
        _imdbService = imdbService;
        _movieRepository = movieRepository;
        _castMemberRepository = castMemberRepository;
    }

    public async Task<Result> Handle(CreateMovieCastFromImdbIdCommand request, CancellationToken cancellationToken)
    {
        var imdbTitle = await _imdbService.GetMovieInformation(request.ImdbId, TitleOptions.FullCast);
        var movie = await _movieRepository.GetByImdbIdAsync(request.ImdbId);

        foreach (var castMember in imdbTitle.ActorList)
        {
            var existingCastMember = await _castMemberRepository.GetByImdbIdAsync(castMember.Id, cancellationToken);

            if (existingCastMember is null)
            {
                existingCastMember = CastMember.Create(
                    castMember.Id,
                    castMember.Name);

                _castMemberRepository.Add(existingCastMember);
            }

            await _movieRepository.AddCastMemberAsync(movie, existingCastMember, castMember.AsCharacter);
        }

        return Result.Success();
    }
}
