using ScreenDrafts.Api.Domain.MovieAggregate.Enums;

namespace ScreenDrafts.Api.Presentation.Common.Mapping;
public sealed class MovieMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(string MovieId, AddCastMemberRequest Request), AddCastMemberCommand>()
            .Map(dest => dest.MovieId, src => src.MovieId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(string MovieId, AddCrewMemberRequest Request), AddCrewMemberCommand>()
            .Map(dest => dest.MovieId, src => src.MovieId)
            .Map(dest => dest.CrewType, src => CrewType.FromName(src.Request.JobDescription, false))
            .Map(dest => dest, src => src.Request);
    }
}
