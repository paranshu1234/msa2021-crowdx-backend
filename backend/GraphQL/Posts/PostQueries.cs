using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using backend.Data;
using backend.Model;
using backend.Extensions;

namespace backend.GraphQL.Posts
{
    [ExtendObjectType(name: "Query")]
    public class PostQueries
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<Post> GetPosts([ScopedService] AppDbContext context)
        {
            return context.Posts.OrderBy(c => c.Created);
        }


        [UseAppDbContext]
        public Post GetPost(int id, [ScopedService] AppDbContext context)
        {
            return context.Posts.Find(id);
        }
    }
}
