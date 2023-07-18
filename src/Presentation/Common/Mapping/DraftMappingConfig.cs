using ScreenDrafts.Api.Contracts.Drafts;
using ScreenDrafts.Api.Domain.DraftAggregate.Enums;

namespace ScreenDrafts.Api.Presentation.Common.Mapping;
public class DraftMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDraftRequest, CreateDraftCommand>()
            .Map(dest => dest.DraftType, src => DraftType.FromName(src.DraftType, false))
            .Map(dest => dest, src => src);
    }
}
