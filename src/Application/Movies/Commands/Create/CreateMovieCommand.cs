namespace ScreenDrafts.Api.Application.Movies.Commands.Create;
public sealed record CreateMovieCommand(string Title, string Year, string ImageUrl, string ImdbUrl) : ICommand;
