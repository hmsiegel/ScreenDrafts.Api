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
global using ScreenDrafts.Api.Application.Drafters.Command.AssignDrafter;
global using ScreenDrafts.Api.Application.Drafters.Command.UpdateDrafter;
global using ScreenDrafts.Api.Application.Drafters.Queries.GetAll;
global using ScreenDrafts.Api.Application.Drafters.Queries.GetById;
global using ScreenDrafts.Api.Application.Drafts.Commands.AddDrafter;
global using ScreenDrafts.Api.Application.Drafts.Commands.AddHost;
global using ScreenDrafts.Api.Application.Drafts.Commands.Create;
global using ScreenDrafts.Api.Application.Drafts.Commands.UpdateDraft;
global using ScreenDrafts.Api.Application.Drafts.Queries.GetAll;
global using ScreenDrafts.Api.Application.Drafts.Queries.GetById;
global using ScreenDrafts.Api.Application.Hosts.Commands.AssignHost;
global using ScreenDrafts.Api.Application.Hosts.Commands.UpdateHost;
global using ScreenDrafts.Api.Application.Hosts.Queries.GetAll;
global using ScreenDrafts.Api.Application.Hosts.Queries.GetById;
global using ScreenDrafts.Api.Contracts.Authentication.Password;
global using ScreenDrafts.Api.Contracts.Authentication.Roles;
global using ScreenDrafts.Api.Contracts.Authentication.Tokens;
global using ScreenDrafts.Api.Contracts.Authentication.Users;
global using ScreenDrafts.Api.Contracts.Drafters;
global using ScreenDrafts.Api.Contracts.Drafts.Requests;
global using ScreenDrafts.Api.Contracts.Hosts;
global using ScreenDrafts.Api.Domain.DraftAggregate.Enums;
global using ScreenDrafts.Api.Domain.Shared;
global using ScreenDrafts.Api.Infrastructure.Auth.Permissions;
global using ScreenDrafts.Api.Presentation.Abstractions;
global using ScreenDrafts.Api.Shared.Authorization;

global using System.Reflection;
