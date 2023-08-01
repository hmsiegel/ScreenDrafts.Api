namespace ScreenDrafts.Api.Presentation.Common.Mapping;
public class DraftMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDraftRequest, CreateDraftCommand>()
            .Map(dest => dest.DraftType, src => DraftType.FromName(src.DraftType, true))
            .Map(dest => dest, src => src);

        config.NewConfig<(string Id, UpdateDraftRequest Request), UpdateDraftCommand>()
            .Map(dest => dest.DraftId, src => src.Id)
            .Map(dest => dest.DraftType, src => DraftType.FromName(src.Request.DraftType, true))
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(string DraftId, AddDraftPickRequest Request), AddDraftPickCommand>()
            .Map(dest => dest.DraftId, src => src.DraftId)
            .Map(dest => dest.DrafterId, src => src.Request.DrafterId)
            .Map(dest => dest.MovieId, src => src.Request.MovieId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(string DraftId, string PickId, AddBlessingDecisionRequest Request), AddBlessingDecisionCommand>()
            .Map(dest => dest.DraftId, src => src.DraftId)
            .Map(dest => dest.PickId, src => src.PickId)
            .Map(dest => dest.DrafterId, src => src.Request.DrafterId)
            .Map(dest => dest.MovieId, src => src.Request.MovieId)
            .Map(dest => dest.BlessingUsed, src => BlessingUsed.FromName(src.Request.BlessingUsed, true));
    }
}
