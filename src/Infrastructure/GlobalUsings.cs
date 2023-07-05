global using Figgle;

global using FluentValidation;

global using Mapster;

global using MediatR;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.FileProviders;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;
global using Microsoft.Identity.Web;
global using Microsoft.IdentityModel.Tokens;

global using ScreenDrafts.Api.Application.Authentication.Tokens;
global using ScreenDrafts.Api.Application.Authentication.Users;
global using ScreenDrafts.Api.Application.Common.Events;
global using ScreenDrafts.Api.Application.Common.Exceptions;
global using ScreenDrafts.Api.Application.Common.FileStorage;
global using ScreenDrafts.Api.Application.Common.Interfaces;
global using ScreenDrafts.Api.Application.Common.Interfaces.Services;
global using ScreenDrafts.Api.Contracts.Authentication.Tokens;
global using ScreenDrafts.Api.Contracts.Authentication.Users;
global using ScreenDrafts.Api.Contracts.FileStorage;
global using ScreenDrafts.Api.Domain.Enums;
global using ScreenDrafts.Api.Domain.Identity;
global using ScreenDrafts.Api.Domain.Identity.DomainEvents;
global using ScreenDrafts.Api.Domain.Primitives;
global using ScreenDrafts.Api.Infrastructure.Auth;
global using ScreenDrafts.Api.Infrastructure.Auth.AzureAd;
global using ScreenDrafts.Api.Infrastructure.Auth.Jwt;
global using ScreenDrafts.Api.Infrastructure.Common;
global using ScreenDrafts.Api.Infrastructure.Common.Extensions;
global using ScreenDrafts.Api.Infrastructure.FileStorage;
global using ScreenDrafts.Api.Infrastructure.Identity;
global using ScreenDrafts.Api.Infrastructure.Validations;
global using ScreenDrafts.Api.Persistence;
global using ScreenDrafts.Api.Shared.Authorization;

global using Scrutor;

global using Serilog;
global using Serilog.Events;
global using Serilog.Exceptions;
global using Serilog.Formatting.Compact;

global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using System.Text.RegularExpressions;
