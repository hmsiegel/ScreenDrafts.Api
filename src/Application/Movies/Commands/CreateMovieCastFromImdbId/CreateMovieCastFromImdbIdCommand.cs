namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieCastFromImdbId;
public sealed record CreateMovieCastFromImdbIdCommand(string ImdbId) : ICommand;
