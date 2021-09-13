namespace backend.GraphQL.Creators
{
    public record AddCreatorInput
    (
        string CreatorName,
        string? CoverImageURI,
        string? AvatarImageURI,
        string UserId   
    );
}
