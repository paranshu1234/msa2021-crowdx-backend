using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using backend.Model;
using backend.Data;
using backend.Extensions;

namespace backend.GraphQL.Posts
{
    [ExtendObjectType(name: "Mutation")]
    public class PostMutations
    {
        [UseAppDbContext]
        public async Task<Post> AddProjectAsync(AddPostInput input,
           [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Title = input.Title,
                Content = input.Content,
                Likes = input.Likes,
                ImageURI = input.ImageURI,
                CreatorId = int.Parse(input.CreatorId),
                Modified = DateTime.Now,
                Created = DateTime.Now,
            };
            context.Posts.Add(post);

            await context.SaveChangesAsync(cancellationToken);

            return post;

        }

        [UseAppDbContext]
        public async Task<Post> EditProjectAsync(EditPostInput input,
          [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var post = await context.Posts.FindAsync(int.Parse(input.PostId));

            post.Title = input.Title ?? post.Title;
            post.Content = input.Content ?? post.Content;
            post.Likes = input.Likes ?? post.Likes;
            post.ImageURI = input.ImageURI ?? post.ImageURI;
            post.Modified = DateTime.Now;

            await context.SaveChangesAsync(cancellationToken);

            return post;
        }
    }
}
