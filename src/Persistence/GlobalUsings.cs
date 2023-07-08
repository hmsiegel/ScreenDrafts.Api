global using Ardalis.Specification.EntityFrameworkCore;

global using Mapster;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.WebUtilities;
global using Microsoft.Data.SqlClient;
global using Microsoft.Data.Sqlite;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.Identity.Web;
global using Microsoft.IdentityModel.Tokens;

global using MySqlConnector;

global using Npgsql;

global using Oracle.ManagedDataAccess.Client;

global using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

global using ScreenDrafts.Api.Application.Authentication.Roles;
global using ScreenDrafts.Api.Application.Authentication.Tokens;
global using ScreenDrafts.Api.Application.Authentication.Users;
global using ScreenDrafts.Api.Application.Common.Caching;
global using ScreenDrafts.Api.Application.Common.Events;
global using ScreenDrafts.Api.Application.Common.Exceptions;
global using ScreenDrafts.Api.Application.Common.FileStorage;
global using ScreenDrafts.Api.Application.Common.Interfaces.Services;
global using ScreenDrafts.Api.Application.Common.Models;
global using ScreenDrafts.Api.Application.Common.Persistence;
global using ScreenDrafts.Api.Application.Common.Specifications;
global using ScreenDrafts.Api.Contracts.Authentication.Password;
global using ScreenDrafts.Api.Contracts.Authentication.Roles;
global using ScreenDrafts.Api.Contracts.Authentication.Tokens;
global using ScreenDrafts.Api.Contracts.Authentication.Users;
global using ScreenDrafts.Api.Domain.DrafterEntity;
global using ScreenDrafts.Api.Domain.Enums;
global using ScreenDrafts.Api.Domain.HostEntitty;
global using ScreenDrafts.Api.Domain.Identity;
global using ScreenDrafts.Api.Domain.Identity.DomainEvents;
global using ScreenDrafts.Api.Infrastructure.Auth;
global using ScreenDrafts.Api.Infrastructure.Auth.Jwt;
global using ScreenDrafts.Api.Infrastructure.Common;
global using ScreenDrafts.Api.Persistence.Common;
global using ScreenDrafts.Api.Persistence.ConnectionString;
global using ScreenDrafts.Api.Persistence.Identity;
global using ScreenDrafts.Api.Persistence.Initialization;
global using ScreenDrafts.Api.Shared.Authorization;

global using Serilog;

global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
