using System.Linq;
using HotChocolate.Types;
using backend.Data;
using backend.Model;
using HotChocolate;

namespace backend.GrapgQL.Users
{
    [ExtendObjectType(name: "Query")]
    public class UserQueries
    {
        public IQueryable<User> GetUsers([ScopedService] AppDbContext context)
        {
            return context.Users;
        }
    }
}
