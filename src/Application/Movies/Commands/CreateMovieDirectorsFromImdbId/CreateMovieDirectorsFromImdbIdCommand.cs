namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieDirectorsFromImdbId;
public sealed record CreateMovieDirectorsFromImdbIdCommand(string ImdbId) : ICommand;
