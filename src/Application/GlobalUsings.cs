global using Ardalis.Specification;

global using FluentValidation;

global using IMDbApiLib.Models;

global using MediatR;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

global using ScreenDrafts.Api.Application.Authentication.Roles;
global using ScreenDrafts.Api.Application.Authentication.Users;
global using ScreenDrafts.Api.Application.Common.Behaviors;
global using ScreenDrafts.Api.Application.Common.Exceptions;
global using ScreenDrafts.Api.Application.Common.Interfaces.Services;
global using ScreenDrafts.Api.Application.Common.Messaging;
global using ScreenDrafts.Api.Application.Common.Models;
global using ScreenDrafts.Api.Application.Common.Persistence;
global using ScreenDrafts.Api.Application.Common.Processing;
global using ScreenDrafts.Api.Application.Common.Services;
global using ScreenDrafts.Api.Application.Drafts.Queries.GetById;
global using ScreenDrafts.Api.Contracts.Auditing;
global using ScreenDrafts.Api.Contracts.Authentication.Password;
global using ScreenDrafts.Api.Contracts.Authentication.Roles;
global using ScreenDrafts.Api.Contracts.Authentication.Tokens;
global using ScreenDrafts.Api.Contracts.Authentication.Users;
global using ScreenDrafts.Api.Contracts.CastMembers.Responses;
global using ScreenDrafts.Api.Contracts.CrewMembers.Responses;
global using ScreenDrafts.Api.Contracts.Drafters;
global using ScreenDrafts.Api.Contracts.Drafts.Responses;
global using ScreenDrafts.Api.Contracts.FileStorage;
global using ScreenDrafts.Api.Contracts.Hosts;
global using ScreenDrafts.Api.Contracts.Mailing;
global using ScreenDrafts.Api.Contracts.Movies.Responses;
global using ScreenDrafts.Api.Domain.CastMemberAggregate;
global using ScreenDrafts.Api.Domain.CastMemberAggregate.ValueObjects;
global using ScreenDrafts.Api.Domain.CrewMemberAggregate;
global using ScreenDrafts.Api.Domain.CrewMemberAggregate.ValueObjects;
global using ScreenDrafts.Api.Domain.DraftAggregate;
global using ScreenDrafts.Api.Domain.DraftAggregate.Entities;
global using ScreenDrafts.Api.Domain.DraftAggregate.Enums;
global using ScreenDrafts.Api.Domain.Enums;
global using ScreenDrafts.Api.Domain.Errors;
global using ScreenDrafts.Api.Domain.Identity;
global using ScreenDrafts.Api.Domain.Identity.Entities;
global using ScreenDrafts.Api.Domain.MovieAggregate;
global using ScreenDrafts.Api.Domain.MovieAggregate.Enums;
global using ScreenDrafts.Api.Domain.Primitives;
global using ScreenDrafts.Api.Domain.Repositories;
global using ScreenDrafts.Api.Domain.Shared;
global using ScreenDrafts.Api.Shared.Notifications;

global using System.Linq.Expressions;
global using System.Net;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text.Json;
