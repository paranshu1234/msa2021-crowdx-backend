using System.Linq;
using HotChocolate.Types;
using backend.Data;
using backend.Model;
using HotChocolate;
using backend.Extensions;

namespace backend.GraphQL.Users
{
    [ExtendObjectType(name: "Query")]
    public class UserQueries
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<User> GetUsers([ScopedService] AppDbContext context)
        {
            return context.Users;
        }

        [UseAppDbContext]
        public User GetUser(int id, [ScopedService] AppDbContext context)
        {
            return context.Users.Find(id);
        }
    }
}
