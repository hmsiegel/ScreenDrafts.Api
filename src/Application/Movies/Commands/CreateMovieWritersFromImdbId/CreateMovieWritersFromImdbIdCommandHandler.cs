﻿namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieWritersFromImdbId;
internal sealed class CreateMovieWritersFromImdbIdCommandHandler
    : ICommandHandler<CreateMovieWritersFromImdbIdCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICrewMemberRepository _crewMemberRepository;

    public CreateMovieWritersFromImdbIdCommandHandler(
        IMovieRepository movieRepository,
        ICrewMemberRepository crewMemberRepository)
    {
        _movieRepository = movieRepository;
        _crewMemberRepository = crewMemberRepository;
    }

    public async Task<Result> Handle(CreateMovieWritersFromImdbIdCommand request, CancellationToken cancellationToken)
    {
        var imdbTitle = request.TitleData;
        var movie = await _movieRepository.GetByImdbIdAsync(imdbTitle.Id, cancellationToken);

        foreach (var crewMember in imdbTitle.WriterList)
        {
            var existingCrewMember = await _crewMemberRepository.GetByImdbIdAsync(crewMember.Id, cancellationToken);

            if (existingCrewMember is null)
            {
                existingCrewMember = CrewMember.Create(
                    crewMember.Id,
                    crewMember.Name);

                _crewMemberRepository.Add(existingCrewMember);
            }

            await _movieRepository.AddCrewMemberAsync(movie, existingCrewMember, "Writer", cancellationToken);
        }

        return Result.Success();
    }
}
