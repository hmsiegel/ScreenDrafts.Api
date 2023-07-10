namespace ScreenDrafts.Api.Domain.Primitives;
public interface ISoftDelete
{
    DateTime? DeletedOn { get; set; }
    DefaultIdType? DeletedBy { get; set; }
}
