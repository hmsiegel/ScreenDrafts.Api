namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieProducersFromImdbId;
public sealed record CreateMovieProducersFromImdbIdCommand(string ImdbId) : ICommand;
