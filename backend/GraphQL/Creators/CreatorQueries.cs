using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using backend.Data;
using backend.Extensions;
using backend.Model;

namespace backend.GraphQL.Creators
{
    [ExtendObjectType(name: "Query")]
    public class CreatorQueries
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<Creator> GetCreators([ScopedService] AppDbContext context)
        {
            return context.Creators;
        }

        [UseAppDbContext]
        public Creator GetCreator(int id, [ScopedService] AppDbContext context)
        {
            return context.Creators.Find(id);
        }
    }
}
