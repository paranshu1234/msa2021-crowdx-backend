using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using backend.GraphQL.Posts;
using backend.GraphQL.Comments;
using backend.GraphQL.Users;
using backend.Data;
using backend.Model;

namespace backend.GraphQL.Creators
{
    public class CreatorType : ObjectType<Creator>
    {
        protected override void Configure(IObjectTypeDescriptor<Creator> descriptor) 
        {
            descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
            descriptor.Field(c => c.CreatorName).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.CoverImageURI).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.AvatarImageURI).Type<NonNullType<StringType>>();

            descriptor
                .Field(c => c.Posts)
                .ResolveWith<Resolvers>(r => r.GetPosts(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<PostType>>>>();

            descriptor
              .Field(c => c.Comments)
              .ResolveWith<Resolvers>(r => r.GetComments(default!, default!, default))
              .UseDbContext<AppDbContext>()
              .Type<NonNullType<ListType<NonNullType<CommentType>>>>();

            descriptor
              .Field(c => c.User)
              .ResolveWith<Resolvers>(r => r.GetUser(default!, default!, default))
              .UseDbContext<AppDbContext>()
              .Type<NonNullType<UserType>>();
        
            descriptor.Field(c => c.Created).Type<NonNullType<DateTimeType>>();
            descriptor.Field(c => c.Modified).Type<NonNullType<DateTimeType>>();

        }


        private class Resolvers
        {

            public async Task<IEnumerable<Post>> GetPosts(Creator creator, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Posts.Where(c => c.CreatorId == creator.Id).ToArrayAsync(cancellationToken);
            }

            public async Task<IEnumerable<Comment>> GetComments(Creator creator, [ScopedService] AppDbContext context,
              CancellationToken cancellationToken)
            {
                return await context.Comments.Where(c => c.CreatorId == creator.Id).ToArrayAsync(cancellationToken);
            }

            public async Task<User> GetUser(Creator creator, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Users.FindAsync(new object[] { creator.UserId }, cancellationToken);
            }
        }
    }
}
