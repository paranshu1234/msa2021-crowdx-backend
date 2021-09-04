using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using backend.GraphQL.Creators;
using backend.GraphQL.Comments;
using backend.Data;
using backend.Model;

namespace backend.GraphQL.Posts
{
    public class PostType : ObjectType<Post>
    {
        protected override void Configure(IObjectTypeDescriptor<Post> descriptor) 
        {
            descriptor.Field(p => p.PostId).Type<NonNullType<IdType>>();
            descriptor.Field(p => p.Title).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.Content).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.Likes).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.ImageURI).Type<NonNullType<StringType>>();

            descriptor
                .Field(p => p.Creator)
                .ResolveWith<Resolvers>(r => r.GetCreator(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<CreatorType>>>>();

            descriptor
               .Field(p => p.Comments)
               .ResolveWith<Resolvers>(r => r.GetComments(default!, default!, default))
               .UseDbContext<AppDbContext>()
               .Type<NonNullType<ListType<NonNullType<CommentType>>>>();
            descriptor.Field(c => c.Created).Type<NonNullType<DateTimeType>>();
            descriptor.Field(c => c.Modified).Type<NonNullType<DateTimeType>>();

        }

        private class Resolvers 
        {
            public async Task<Creator> GetCreator(Post post, [ScopedService] AppDbContext context,
             CancellationToken cancellationToken)
            {
                return await context.Creators.FindAsync(new object[] { post.CreatorId }, cancellationToken);
            }

            public async Task<IEnumerable<Comment>> GetComments(Post post, [ScopedService] AppDbContext context,
             CancellationToken cancellationToken)
            {
                return await context.Comments.Where(p => p.PostId == post.PostId).ToArrayAsync(cancellationToken);
            }
        }
    }
}
