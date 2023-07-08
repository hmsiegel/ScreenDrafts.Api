namespace ScreenDrafts.Api.Presentation.OpenApi;
internal static class Startup
{
    internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection(nameof(OpenApiSettings)).Get<OpenApiSettings>();
        if (settings is null)
        {
            return services;
        }

        if (settings.Enable)
        {
            services.AddVersionedApiExplorer(o => o.SubstituteApiVersionInUrl = true);
            services.AddEndpointsApiExplorer();
            services.AddScoped<FluentValidationSchemaProcessor>(provider =>
            {
                var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
            });
            services.AddOpenApiDocument((document, sp) =>
            {
                document.PostProcess = doc =>
                {
                    doc.Info.Title = settings.Title;
                    doc.Info.Description = settings.Description;
                    doc.Info.Version = settings.Version;
                    doc.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = settings.ContactName,
                        Email = settings.ContactEmail,
                        Url = settings.ContactUrl,
                    };
                    doc.Info.License = new()
                    {
                        Name = settings.LicenseName,
                        Url = settings.LicenseUrl,
                    };
                };

                if (config["SecuritySettings:Provider"]!.Equals("AzureAd", StringComparison.OrdinalIgnoreCase))
                {
                    document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.OAuth2,
                        Flow = OpenApiOAuth2Flow.AccessCode,
                        Description = "OAuth2.0 Auth Code with PKCE",
                        Flows = new()
                        {
                            AuthorizationCode = new()
                            {
                                AuthorizationUrl = config["SecuritySettings:Swagger:AuthorizationUrl"],
                                TokenUrl = config["SecuritySettings:Swagger:TokenUrl"],
                                Scopes = new Dictionary<string, string>
                                {
                                    { config["SecuritySettings:Swagger:ApiScope"]!, "access the api" },
                                },
                            },
                        },
                    });
                }
                else
                {
                    document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Input your Bearer token to access this API",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Type = OpenApiSecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                    });
                }

                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
                document.OperationProcessors.Add(new SwaggerGlobalAuthProcessor());

                document.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TimeSpan), schema =>
                {
                    schema.Type = NJsonSchema.JsonObjectType.String;
                    schema.IsNullableRaw = true;
                    schema.Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$";
                    schema.Example = "02:00:00";
                }));

                document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());

                var fluentValidationSchemaProcessor = sp.CreateScope().ServiceProvider.GetService<FluentValidationSchemaProcessor>();
                document.SchemaProcessors.Add(fluentValidationSchemaProcessor);
            });
        }

        return services;
    }

    internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IConfiguration config)
    {
        if (config.GetValue<bool>("OpenApiSettings:Enable"))
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(options =>
            {
                options.DefaultModelsExpandDepth = -1;
                options.DocExpansion = "none";
                options.TagsSorter = "alpha";
                if (config["SecuritySettings:Provider"]!.Equals("AzureAd", StringComparison.OrdinalIgnoreCase))
                {
                    options.OAuth2Client = new OAuth2ClientSettings
                    {
                        AppName = "Screen Drafts Api Client",
                        ClientId = config["SecuritySettings:Swagger:OpenIdClientId"],
                        ClientSecret = string.Empty,
                        UsePkceWithAuthorizationCodeGrant = true,
                        ScopeSeparator = " ",
                    };
                    options.OAuth2Client.Scopes.Add(config["SecuritySettings:Swagger:ApiScope"]);
                }
            });
        }

        return app;
    }
}
