
namespace backend.GraphQL.Users
{
    public record AddUserInput
    (
        string UserName,
        string Email,
        string? ImageURI
    );
}
