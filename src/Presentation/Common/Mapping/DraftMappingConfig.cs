namespace ScreenDrafts.Api.Presentation.Common.Mapping;
public class DraftMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDraftRequest, CreateDraftCommand>()
            .Map(dest => dest.DraftType, src => DraftType.FromName(src.DraftType, false))
            .Map(dest => dest, src => src);

        config.NewConfig<(string Id, UpdateDraftRequest Request), UpdateDraftCommand>()
            .Map(dest => dest.DraftId, src => src.Id)
            .Map(dest => dest.DraftType, src => DraftType.FromName(src.Request.DraftType, false))
            .Map(dest => dest, src => src.Request);
    }
}
