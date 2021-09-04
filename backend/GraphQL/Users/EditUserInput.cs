
namespace backend.GraphQL.Users
{
    public record EditUserInput
    (
        string UserId,
        string? UserName,
        string? Email,
        string? ImageURI
    );
}
