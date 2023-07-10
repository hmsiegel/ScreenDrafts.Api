global using Figgle;

global using FluentValidation;

global using Hangfire;
global using Hangfire.Client;
global using Hangfire.Console;
global using Hangfire.Console.Extensions;
global using Hangfire.Dashboard;
global using Hangfire.Logging;
global using Hangfire.MySql;
global using Hangfire.PostgreSql;
global using Hangfire.Server;
global using Hangfire.SQLite;
global using Hangfire.SqlServer;
global using Hangfire.States;
global using Hangfire.Storage;

global using MailKit.Net.Smtp;
global using MailKit.Security;

global using MediatR;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.FileProviders;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Logging.Abstractions;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;
global using Microsoft.Identity.Web;
global using Microsoft.IdentityModel.Tokens;

global using MimeKit;

global using Newtonsoft.Json;
global using Newtonsoft.Json.Converters;
global using Newtonsoft.Json.Serialization;

global using NJsonSchema.Generation.TypeMappers;

global using NSwag;
global using NSwag.AspNetCore;
global using NSwag.Generation.AspNetCore;
global using NSwag.Generation.Processors;
global using NSwag.Generation.Processors.Contexts;
global using NSwag.Generation.Processors.Security;

global using RazorEngineCore;

global using ScreenDrafts.Api.Application.Authentication.Users;
global using ScreenDrafts.Api.Application.Common.Caching;
global using ScreenDrafts.Api.Application.Common.Events;
global using ScreenDrafts.Api.Application.Common.Exceptions;
global using ScreenDrafts.Api.Application.Common.FileStorage;
global using ScreenDrafts.Api.Application.Common.Interfaces.Services;
global using ScreenDrafts.Api.Application.Common.Mailing;
global using ScreenDrafts.Api.Contracts.FileStorage;
global using ScreenDrafts.Api.Contracts.Mailing;
global using ScreenDrafts.Api.Domain.Enums;
global using ScreenDrafts.Api.Domain.Primitives;
global using ScreenDrafts.Api.Domain.Shared;
global using ScreenDrafts.Api.Infrastructure.Auth;
global using ScreenDrafts.Api.Infrastructure.Auth.AzureAd;
global using ScreenDrafts.Api.Infrastructure.Auth.Jwt;
global using ScreenDrafts.Api.Infrastructure.Auth.Permissions;
global using ScreenDrafts.Api.Infrastructure.BackgroundJobs;
global using ScreenDrafts.Api.Infrastructure.Caching;
global using ScreenDrafts.Api.Infrastructure.Common;
global using ScreenDrafts.Api.Infrastructure.Common.Extensions;
global using ScreenDrafts.Api.Infrastructure.FileStorage;
global using ScreenDrafts.Api.Infrastructure.Mailing;
global using ScreenDrafts.Api.Infrastructure.Middleware;
global using ScreenDrafts.Api.Infrastructure.OpenApi;
global using ScreenDrafts.Api.Infrastructure.SecurityHeaders;
global using ScreenDrafts.Api.Infrastructure.Validations;
global using ScreenDrafts.Api.Persistence.BackgroundJobs;
global using ScreenDrafts.Api.Presentation.OpenApi;
global using ScreenDrafts.Api.Shared.Authorization;
global using ScreenDrafts.Api.Shared.Common;

global using Serilog;
global using Serilog.Context;
global using Serilog.Events;
global using Serilog.Exceptions;
global using Serilog.Formatting.Compact;

global using System.ComponentModel.DataAnnotations;
global using System.Linq.Expressions;
global using System.Net;
global using System.Net.Http.Headers;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Security.Claims;
global using System.Text;
global using System.Text.RegularExpressions;

global using ZymLabs.NSwag.FluentValidation;
