namespace backend.GraphQL.Comments
{
    public record AddCommentInput
    (
        string Content,
        string PostId,
        string UserId,
        string? CreatorId,
        string? Description
    );
}
