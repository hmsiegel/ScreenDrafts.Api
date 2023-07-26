namespace ScreenDrafts.Api.Domain.Repositories;
public interface ICrewMemberRepository
{
    void Add(CrewMember crewMember);
    void UpdateCrewMember(CrewMember crewMember);
    Task<CrewMember> GetByCrewMemberIdAsync(DefaultIdType id, CancellationToken cancellationToken = default);
    Task<CrewMember> GetByName(string name, CancellationToken cancellationToken = default);
    Task<List<CrewMember>> GetAllCrewMembers(CancellationToken cancellationToken = default);
}
