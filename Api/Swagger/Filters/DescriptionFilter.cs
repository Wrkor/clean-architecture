namespace Api.Swagger.Filters;

public class DescriptionFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument document, DocumentFilterContext context)
    {
        document.Info = new OpenApiInfo()
        {
            Title = "REST API | Социальная сеть",
        };
    }
}
