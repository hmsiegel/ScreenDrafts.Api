global using Figgle;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

global using ScreenDrafts.Api.Application.Authentication;
global using ScreenDrafts.Api.Application.Behaviors;
global using ScreenDrafts.Api.Application.Common.Exceptions;
global using ScreenDrafts.Api.Contracts.Authentication;
global using ScreenDrafts.Api.Domain.Identity;
global using ScreenDrafts.Api.Infrastructure.Auth;
global using ScreenDrafts.Api.Infrastructure.Identity;
global using ScreenDrafts.Api.Persistence;

global using Scrutor;

global using Serilog;
global using Serilog.Events;
global using Serilog.Exceptions;
global using Serilog.Formatting.Compact;

global using System.Reflection;
global using System.Security.Claims;
