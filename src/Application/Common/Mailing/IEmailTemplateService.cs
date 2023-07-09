namespace ScreenDrafts.Api.Application.Common.Mailing;
public interface IEmailTemplateService : ITransientService
{
    string GenericEmailTemplate<T>(string templateName, T model)
        where T : class;
}
