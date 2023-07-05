﻿namespace ScreenDrafts.Api.Application.Common.Exceptions;
public sealed class UnauthorizedException : CustomException
{
    public UnauthorizedException(
        string message)
        : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}
