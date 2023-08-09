namespace ScreenDrafts.Api.Persistence.Seeders;
internal sealed partial class MovieSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MovieSeeder> _logger;
    private readonly IMovieRepository _movieRepository;
    private readonly IImdbService _imdbService;
    private readonly ICastMemberRepository _castMemberRepository;
    private readonly ICrewMemberRepository _crewMemberRepository;
    private readonly ICsvFileService _csv;

    public MovieSeeder(
        ApplicationDbContext context,
        ILogger<MovieSeeder> logger,
        IMovieRepository movieRepository,
        IImdbService imdbService,
        ICastMemberRepository castMemberRepository,
        ICrewMemberRepository crewMemberRepository,
        ICsvFileService csv)
    {
        _context = context;
        _logger = logger;
        _movieRepository = movieRepository;
        _imdbService = imdbService;
        _castMemberRepository = castMemberRepository;
        _crewMemberRepository = crewMemberRepository;
        _csv = csv;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        string path = ProjectSourcePath.Value;

        if (!_context.Movies.Any())
        {
            _logger.LogInformation("Starting to seed movies");

            var movieData = _csv.ReadCsvFile<MovieRequest>(path + "Seeders\\Files\\movies.csv").ToList();

            if (movieData is not null)
            {
                foreach (var movieTitle in movieData)
                {
                    string movieId;
                    SearchData movie;

                    if (movieTitle.Year != string.Empty)
                    {
                        var title = YearRegex().Replace(movieTitle.Title, string.Empty);
                        var releaseToYear = (int.Parse(movieTitle.Year) + 1).ToString();
                        var releaseToDate = DateTime.ParseExact(releaseToYear, "yyyy", CultureInfo.InvariantCulture);
                        var releaseFromDate = DateTime.ParseExact(movieTitle.Year, "yyyy", CultureInfo.InvariantCulture);
                        var releaseDateTo = releaseToDate.ToString("yyyy-MM-dd");
                        var releaseDateFrom = releaseFromDate.ToString("yyyy-MM-dd");
                        var searchQuery = new AdvancedSearchInput
                        {
                            Title = title,
                            ReleaseDateFrom = releaseDateFrom,
                            ReleaseDateTo = releaseDateTo,
                            TitleType = AdvancedSearchTitleType.Feature_Film,
                        };

                        var advancedMovie = await _imdbService.AdvancedSearch(searchQuery);

                        if (advancedMovie.Results.Count == 0)
                        {
                            movie = await _imdbService.SearchForMovie(movieTitle.Title);
                            movieId = movie.Results.FirstOrDefault()!.Id;
                        }
                        else
                        {
                            movieId = advancedMovie.Results.FirstOrDefault()!.Id;
                        }
                    }
                    else
                    {
                        movie = await _imdbService.SearchByTitle(movieTitle.Title);
                        movieId = movie.Results.FirstOrDefault()!.Id;
                    }

                    if (movieId is not null)
                    {
                        var movieInformation = await _imdbService.GetMovieInformation(movieId);
                        _logger.LogInformation("Seeding movie {movieName}", movieInformation.Title);

                        await CreateMovieFromImdbAsync(movieInformation, cancellationToken);
                        await CreateMovieCastFromImdbAsync(movieInformation, cancellationToken);
                        await CreateMovieDirectorsFromImdbAsync(movieInformation, cancellationToken);
                        await CreateMovieWritersFromImdbAsync(movieInformation, cancellationToken);
                        movieInformation = await _imdbService.GetMovieInformation(movieId, TitleOptions.FullCast);
                        await CreateMovieProducersFromImdbAsync(movieInformation, cancellationToken);
                    }
                }
            }
        }
    }

    [GeneratedRegex("\\((\\d{4})\\)")]
    private static partial Regex YearRegex();

    private async Task CreateMovieFromImdbAsync(TitleData titleData, CancellationToken cancellationToken)
    {
        var imdbUrl = $"https://www.imdb.com/title/{titleData.Id}";

        var movie = Movie.Create(
            titleData.Title,
            titleData.Year,
            titleData.Image,
            imdbUrl);

        _movieRepository.Add(movie);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task CreateMovieCastFromImdbAsync(TitleData titleData, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByImdbIdAsync(titleData.Id, cancellationToken);

        foreach (var castMember in titleData.ActorList)
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

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task CreateMovieDirectorsFromImdbAsync(TitleData titleData, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByImdbIdAsync(titleData.Id, cancellationToken);

        foreach (var crewMember in titleData.DirectorList)
        {
            var existingCrewMember = await _crewMemberRepository.GetByImdbIdAsync(crewMember.Id, cancellationToken);

            if (existingCrewMember is null)
            {
                existingCrewMember = CrewMember.Create(
                    crewMember.Id,
                    crewMember.Name);

                _crewMemberRepository.Add(existingCrewMember);
            }

            await _movieRepository.AddCrewMemberAsync(movie, existingCrewMember, "Director", cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task CreateMovieWritersFromImdbAsync(TitleData titleData, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByImdbIdAsync(titleData.Id, cancellationToken);

        foreach (var crewMember in titleData.WriterList)
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

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task CreateMovieProducersFromImdbAsync(TitleData titleData, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByImdbIdAsync(titleData.Id, cancellationToken);

        foreach (var crewMember in titleData.FullCast.Others.Where(x => x.Job.Contains("Produced", StringComparison.InvariantCultureIgnoreCase)))
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

        await _context.SaveChangesAsync(cancellationToken);
    }
}
