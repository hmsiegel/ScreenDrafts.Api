namespace ScreenDrafts.Api.Domain.Repositories;
public interface ICastMemberRepository
{
    void Add(CastMember castMember);
    void UpdateCastMember(CastMember castMember);
    Task<CastMember> GetByCastMemberIdAsync(DefaultIdType id, CancellationToken cancellationToken = default);
    Task<CastMember> GetByName(string name, CancellationToken cancellationToken = default);
    Task<List<CastMember>> GetAllCastMembers(CancellationToken cancellationToken = default);
}
