namespace backend.GraphQL.Posts
{
    public record EditPostInput
    (
        string PostId,
        string? Title,
        string? Content,
        string? Likes,
        string? ImageURI
    );
}
