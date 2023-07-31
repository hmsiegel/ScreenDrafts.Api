global using IMDbApiLib.Models;

global using Mapster;

global using MapsterMapper;

global using MediatR;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.Extensions.DependencyInjection;

global using NSwag;
global using NSwag.Annotations;

global using Presentation.Abstractions;

global using ScreenDrafts.Api.Application.Authentication.Roles;
global using ScreenDrafts.Api.Application.Authentication.Tokens;
global using ScreenDrafts.Api.Application.Authentication.Users;
global using ScreenDrafts.Api.Application.CastMembers.Commands.Create;
global using ScreenDrafts.Api.Application.CastMembers.Queries.GetAll;
global using ScreenDrafts.Api.Application.CastMembers.Queries.GetById;
global using ScreenDrafts.Api.Application.Common.Interfaces.Services;
global using ScreenDrafts.Api.Application.CrewMembers.Commands.Create;
global using ScreenDrafts.Api.Application.CrewMembers.Queries.GetAll;
global using ScreenDrafts.Api.Application.CrewMembers.Queries.GetById;
global using ScreenDrafts.Api.Application.Drafters.Command.AssignDrafter;
global using ScreenDrafts.Api.Application.Drafters.Command.UpdateDrafter;
global using ScreenDrafts.Api.Application.Drafters.Queries.GetAll;
global using ScreenDrafts.Api.Application.Drafts.Commands.AddDrafter;
global using ScreenDrafts.Api.Application.Drafts.Commands.AddHost;
global using ScreenDrafts.Api.Application.Drafts.Commands.AddMovie;
global using ScreenDrafts.Api.Application.Drafts.Commands.Create;
global using ScreenDrafts.Api.Application.Drafts.Commands.UpdateDraft;
global using ScreenDrafts.Api.Application.Drafts.Queries.GetAll;
global using ScreenDrafts.Api.Application.Drafts.Queries.GetAllDraftersAndHosts;
global using ScreenDrafts.Api.Application.Drafts.Queries.GetAllDrafts;
global using ScreenDrafts.Api.Application.Drafts.Queries.GetById;
global using ScreenDrafts.Api.Application.Hosts.Commands.AssignHost;
global using ScreenDrafts.Api.Application.Hosts.Commands.UpdateHost;
global using ScreenDrafts.Api.Application.Hosts.Queries.GetAll;
global using ScreenDrafts.Api.Application.Hosts.Queries.GetById;
global using ScreenDrafts.Api.Application.Movies.Commands.AddCastMember;
global using ScreenDrafts.Api.Application.Movies.Commands.AddCrewMember;
global using ScreenDrafts.Api.Application.Movies.Commands.Create;
global using ScreenDrafts.Api.Application.Movies.Commands.CreateMovieCastFromImdbId;
global using ScreenDrafts.Api.Application.Movies.Commands.CreateMovieDirectorsFromImdbId;
global using ScreenDrafts.Api.Application.Movies.Commands.CreateMovieFromImdb;
global using ScreenDrafts.Api.Application.Movies.Commands.CreateMovieProducersFromImdbId;
global using ScreenDrafts.Api.Application.Movies.Commands.CreateMovieWritersFromImdbId;
global using ScreenDrafts.Api.Application.Movies.Queries.GetAll;
global using ScreenDrafts.Api.Application.Movies.Queries.GetByImdbId;
global using ScreenDrafts.Api.Application.Movies.Queries.GetByMovieId;
global using ScreenDrafts.Api.Contracts.Authentication.Password;
global using ScreenDrafts.Api.Contracts.Authentication.Roles;
global using ScreenDrafts.Api.Contracts.Authentication.Tokens;
global using ScreenDrafts.Api.Contracts.Authentication.Users;
global using ScreenDrafts.Api.Contracts.CastMembers.Requests;
global using ScreenDrafts.Api.Contracts.CrewMembers.Requests;
global using ScreenDrafts.Api.Contracts.Drafters;
global using ScreenDrafts.Api.Contracts.Drafts.Requests;
global using ScreenDrafts.Api.Contracts.Hosts;
global using ScreenDrafts.Api.Contracts.Imdb;
global using ScreenDrafts.Api.Contracts.Movies.Requests;
global using ScreenDrafts.Api.Domain.DraftAggregate.Enums;
global using ScreenDrafts.Api.Domain.Enums;
global using ScreenDrafts.Api.Domain.Shared;
global using ScreenDrafts.Api.Infrastructure.Auth.Permissions;
global using ScreenDrafts.Api.Presentation.Abstractions;
global using ScreenDrafts.Api.Shared.Authorization;

global using System.Reflection;
