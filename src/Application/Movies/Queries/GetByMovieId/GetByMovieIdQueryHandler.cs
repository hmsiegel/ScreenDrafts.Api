﻿namespace ScreenDrafts.Api.Application.Movies.Queries.GetByMovieId;
internal sealed class GetByMovieIdQueryHandler : IQueryHandler<GetByMovieIdQuery, MovieResponse>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICrewMemberRepository _crewMemberRepository;
    private readonly ICastMemberRepository _castMemberRepository;

    public GetByMovieIdQueryHandler(
        IMovieRepository movieRepository,
        ICrewMemberRepository crewMemberRepository,
        ICastMemberRepository castMemberRepository)
    {
        _movieRepository = movieRepository;
        _crewMemberRepository = crewMemberRepository;
        _castMemberRepository = castMemberRepository;
    }

    public async Task<Result<MovieResponse>> Handle(GetByMovieIdQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetAll();

        var movie = movies.SingleOrDefault(x => x.Id!.Value == request.Id);

        if (movie is null)
        {
            return Result.Failure<MovieResponse>(DomainErrors.Movie.NotFound);
        }

        var response = new MovieResponse(
            movie.Id!.Value,
            movie.Title!,
            movie.Year!,
            movie.ImageUrl!,
            movie.ImdbUrl!,
            GetCrewMembers(movie),
            GetCastMembers(movie));

        return Result.Success(response);
    }

    private List<MovieCastMemberResponse> GetCastMembers(Movie? movie)
    {
        return _movieRepository.GetAllMovieCastMembers(movie!.Id!.Value)
                        .Result
                        .ConvertAll(x => new MovieCastMemberResponse(
                            x.CastMemberId.Value,
                            _castMemberRepository.GetByCastMemberIdAsync(x.CastMemberId.Value).Result.Name!,
                            _castMemberRepository.GetByCastMemberIdAsync(x.CastMemberId.Value).Result.ImdbId!,
                            x.RoleDescription!));
    }

    private List<MovieCrewMemberResponse> GetCrewMembers(Movie? movie)
    {
        return _movieRepository.GetAllMovieCrewMembers(movie!.Id!.Value)
                        .Result
                        .ConvertAll(x => new MovieCrewMemberResponse(
                        x.CrewMemberId.Value,
                        _crewMemberRepository.GetByCrewMemberIdAsync(x.CrewMemberId.Value).Result.Name!,
                        _crewMemberRepository.GetByCrewMemberIdAsync(x.CrewMemberId.Value).Result.ImdbId!,
                        x.JobDescription!));
    }
}