namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieCastFromImdbId;
internal sealed class CreateMovieCastFromImdbIdCommandHandler
    : ICommandHandler<CreateMovieCastFromImdbIdCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICastMemberRepository _castMemberRepository;

    public CreateMovieCastFromImdbIdCommandHandler(
        IMovieRepository movieRepository,
        ICastMemberRepository castMemberRepository)
    {
        _movieRepository = movieRepository;
        _castMemberRepository = castMemberRepository;
    }

    public async Task<Result> Handle(CreateMovieCastFromImdbIdCommand request, CancellationToken cancellationToken)
    {
        var imdbTitle = request.TitleData;
        var movie = await _movieRepository.GetByImdbIdAsync(imdbTitle.Id, cancellationToken);

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

            await _movieRepository.AddCastMemberAsync(movie, existingCastMember, castMember.AsCharacter, cancellationToken);
        }

        return Result.Success();
    }
}
