namespace ScreenDrafts.Api.Infrastructure.Mailing;
public sealed class EmailTemplateService : IEmailTemplateService
{
    public string GenericEmailTemplate<T>(string templateName, T model)
        where T : class
    {
        string template = GetTemplate(templateName);

        IRazorEngine razorEngine = new RazorEngine();
        IRazorEngineCompiledTemplate modifiedTemplate = razorEngine.Compile(template);

        return modifiedTemplate.Run(model);
    }

    private static string GetTemplate(string templateName)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string templateFolder = Path.Combine(baseDirectory, "EmailTemplates");
        string filePath = Path.Combine(templateFolder, $"{templateName}.cshtml");

        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var streamReader = new StreamReader(fileStream, Encoding.Default);
        string mailText = streamReader.ReadToEnd();
        streamReader.Close();
        return mailText;
    }
}
