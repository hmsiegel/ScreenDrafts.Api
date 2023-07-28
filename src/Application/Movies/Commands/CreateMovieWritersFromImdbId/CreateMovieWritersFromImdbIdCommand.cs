namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieWritersFromImdbId;
public sealed record CreateMovieWritersFromImdbIdCommand(string ImdbId) : ICommand;
