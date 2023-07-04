namespace ScreenDrafts.Api.Domain.Shared;
public class Error : IEqualityComparer<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null");

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    public bool Equals(Error? x, Error? y)
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public int GetHashCode([DisallowNull] Error obj)
    {
        throw new NotImplementedException();
    }

    public override int GetHashCode() => HashCode.Combine(Code, Message);

    public override string ToString() => Code;
}
