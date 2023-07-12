namespace ScreenDrafts.Api.Domain.Primitives;
public interface IAuditableEntity
{
    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
