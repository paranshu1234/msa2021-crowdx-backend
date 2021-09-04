using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using backend.GraphQL.Creators;
using backend.GraphQL.Users;
using backend.GraphQL.Posts;
using backend.Data;
using backend.Model;

namespace backend.GraphQL.Comments
{
    public class CommentType : ObjectType<Comment>
    {
        protected override void Configure(IObjectTypeDescriptor<Comment> descriptor) 
        {
            descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
            descriptor.Field(c => c.Content).Type<NonNullType<StringType>>();
            descriptor
                .Field(p => p.Post)
                .ResolveWith<Resolvers>(r => r.GetPost(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<PostType>>>>();
            descriptor
                .Field(p => p.Creator)
                .ResolveWith<Resolvers>(r => r.GetCreator(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<CreatorType>>>>();
            descriptor
                .Field(p => p.User)
                .ResolveWith<Resolvers>(r => r.GetUser(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<UserType>>>>();
            descriptor.Field(c => c.Description).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Created).Type<NonNullType<DateTimeType>>();
            descriptor.Field(c => c.Modified).Type<NonNullType<DateTimeType>>();
        }

        private class Resolvers {

            public async Task<Post> GetPost(Comment comment, [ScopedService] AppDbContext context,
            CancellationToken cancellationToken)
            {
                return await context.Posts.FindAsync(new object[] { comment.PostId }, cancellationToken);
            }

            public async Task<Creator> GetCreator(Comment comment, [ScopedService] AppDbContext context,
            CancellationToken cancellationToken)
            {
                return await context.Creators.FindAsync(new object[] { comment.CreatorId }, cancellationToken);
            }

            public async Task<User> GetUser(Comment comment, [ScopedService] AppDbContext context,
            CancellationToken cancellationToken)
            {
                return await context.Users.FindAsync(new object[] { comment.UserId }, cancellationToken);
            }
        }
    }
}
