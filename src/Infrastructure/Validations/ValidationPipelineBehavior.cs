﻿using ValidationException = FluentValidation.ValidationException;

namespace ScreenDrafts.Api.Infrastructure.Validations;
public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }

    //private static TResult CreateValidationResult<TResult>(Error[] errors)
    //     where TResult : Result
    //{
    //    if (typeof(TRequest) == typeof(Result))
    //    {
    //        return (ValidationResult.WithErrors(errors) as TResult)!;
    //    }

    //    var validationResult = typeof(ValidationResult<>)
    //        .GetGenericTypeDefinition()
    //        .MakeGenericType(typeof(TRequest).GenericTypeArguments[0])
    //        .GetMethod(nameof(ValidationResult.WithErrors))!
    //        .Invoke(null, new object[] { errors })!;

    //    return (TResult)validationResult;
    //}
}
