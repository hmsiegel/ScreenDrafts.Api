﻿namespace ScreenDrafts.Api.Infrastructure.Common.Services;
public sealed class NewtonSoftService : ISerializerService
{
    public T Deserialize<T>(string text)
    {
        return JsonConvert.DeserializeObject<T>(text);
    }

    public string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter(namingStrategy: new CamelCaseNamingStrategy()),
            },
        });
    }

    public string Serialize<T>(T obj, Type type)
    {
        return JsonConvert.SerializeObject(obj, type, new());
    }
}
