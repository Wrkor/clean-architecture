namespace Application.Topics.Mapping;

public class TopicRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TopicId, Guid>().MapWith(src => src.Value);
        config.NewConfig<Guid, TopicId>().MapWith(src => TopicId.Of(src));
        config
            .NewConfig<CreateTopicDto, Topic>()
            .MapWith(src =>
                Topic.Create(
                    TopicId.Of(Guid.NewGuid()),
                    src.Title,
                    src.Summary,
                    src.TopicType,
                    src.EventStartedAt,
                    Location.Of(src.Location.City, src.Location.Street)
                )
            );
        config.NewConfig<UpdateTopicDto, Topic>();
        config.NewConfig<Topic, TopicResponseDto>();
        config.NewConfig<Location, LocationDto>();
        config
            .NewConfig<LocationDto, Location>()
            .MapWith(src => Location.Of(src.City, src.Street));
    }
}
