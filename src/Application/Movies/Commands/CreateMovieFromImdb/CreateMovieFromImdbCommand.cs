namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieFromImdb;
public sealed record CreateMovieFromImdbCommand(string ImdbId) : ICommand;
