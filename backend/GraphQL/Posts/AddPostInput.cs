namespace backend.GraphQL.Posts
{
    public record AddPostInput
    (
        string Title,
        string Content,
        string Likes,
        string ImageURI,
        string CreatorId
    );
}
