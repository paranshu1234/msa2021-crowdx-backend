using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using backend.GraphQL.Creators;
using backend.GraphQL.Comments;
using backend.GraphQL.Posts;
using backend.Data;
using backend.Model;

namespace backend.GraphQL.Users
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(u => u.Id).Type<NonNullType<IdType>>();
            descriptor.Field(u => u.UserName).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.Email).Type<NonNullType<StringType>>();
            descriptor.Field(u => u.ImageURI).Type<NonNullType<StringType>>();

            descriptor
              .Field(u => u.Creator)
              .ResolveWith<Resolvers>(r => r.GetCreator(default!, default!, default))
              .UseDbContext<AppDbContext>()
              .Type<NonNullType<ListType<NonNullType<CreatorType>>>>();

            descriptor
               .Field(u => u.Comments)
               .ResolveWith<Resolvers>(r => r.GetComments(default!, default!, default))
               .UseDbContext<AppDbContext>()
               .Type<NonNullType<ListType<NonNullType<CommentType>>>>();

            descriptor.Field(c => c.Created).Type<NonNullType<DateTimeType>>();
            descriptor.Field(c => c.Modified).Type<NonNullType<DateTimeType>>();
        }

        private class Resolvers 
        {
            public async Task<Creator> GetCreator(User user, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Creators.FindAsync(new object[] { user.Creator }, cancellationToken);
            }

            public async Task<IEnumerable<Comment>> GetComments(User user, [ScopedService] AppDbContext context,
             CancellationToken cancellationToken)
            {
                return await context.Comments.Where(u => u.UserId == user.Id).ToArrayAsync(cancellationToken);
            }
        }
    }
}
