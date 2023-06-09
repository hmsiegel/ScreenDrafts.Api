﻿namespace ScreenDrafts.Api.Application.Common.Interfaces.Services;
public interface IImdbService
{
    Task<FullCastData> GetFullCast(string id);
    Task<TitleData> GetMovieInformation(string id, string options);
    Task<SearchData> SearchByKeyword(string searchExpression);
    Task<SearchData> SearchByTitle(string searchExpression);
    Task<SearchData> SearchForMovie(string searchExpression);
    Task<SearchData> SearchForSeries(string searchExpression);
}
