namespace ScreenDrafts.Api.Application.Common.Mailing;
public interface IMailService : ITransientService
{
    Task SendAsync(MailRequest request, CancellationToken cancellationToken = default);
}
