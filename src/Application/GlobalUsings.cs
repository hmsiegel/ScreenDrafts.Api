global using Ardalis.Specification;

global using FluentValidation;

global using IMDbApiLib.Models;

global using MediatR;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

global using ScreenDrafts.Api.Application.Common.Exceptions;
global using ScreenDrafts.Api.Application.Common.Interfaces.Services;
global using ScreenDrafts.Api.Application.Common.Models;
global using ScreenDrafts.Api.Application.Common.Persistence;
global using ScreenDrafts.Api.Contracts.Auditing;
global using ScreenDrafts.Api.Contracts.Authentication.Password;
global using ScreenDrafts.Api.Contracts.Authentication.Roles;
global using ScreenDrafts.Api.Contracts.Authentication.Tokens;
global using ScreenDrafts.Api.Contracts.Authentication.Users;
global using ScreenDrafts.Api.Contracts.FileStorage;
global using ScreenDrafts.Api.Contracts.Mailing;
global using ScreenDrafts.Api.Domain.Enums;
global using ScreenDrafts.Api.Domain.Identity;
global using ScreenDrafts.Api.Domain.Primitives;
global using ScreenDrafts.Api.Domain.Shared;
global using ScreenDrafts.Api.Shared.Notifications;

global using System.Linq.Expressions;
global using System.Net;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text.Json;
