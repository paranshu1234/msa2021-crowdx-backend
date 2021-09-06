namespace backend.GraphQL.Creators
{
    public record EditCreatorInput
    (
        string CreatorId,
        string? CreatorName,
        string? CoverImageURI,
        string? AvatarImageURI
    );
}
