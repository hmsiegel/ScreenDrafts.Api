namespace ScreenDrafts.Api.Application.Common.Messaging;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TReponse> : IRequestHandler<TCommand, Result<TReponse>>
    where TCommand : ICommand<TReponse>
{
}
