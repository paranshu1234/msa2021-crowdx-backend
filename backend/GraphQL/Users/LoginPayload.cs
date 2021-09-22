using backend.Model;

namespace backend.GraphQL.Users
{
    public record LoginPayload
    (
        User user,
        string jwt
    );
}
