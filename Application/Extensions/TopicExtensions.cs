namespace Application.Extensions;

public static class TopicExtensions
{
    public static TopicResponseDto ToTopicResponseDto(this Topic topic)
    {
        var location = new LocationDto(topic.Location.City, topic.Location.Street);
        var result = new TopicResponseDto(
            topic.Id.Value,
            topic.Title,
            topic.Summary,
            topic.TopicType,
            location,
            topic.EventStartedAt
        );

        return result;
    }

    public static List<TopicResponseDto> ToTopicResponseDtoList(this List<Topic> topics)
    {
        return topics.Select(t => t.ToTopicResponseDto()).ToList();
    }
}
