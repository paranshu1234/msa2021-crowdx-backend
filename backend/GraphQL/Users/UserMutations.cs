using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using backend.Model;
using backend.Data;
using backend.Extensions;

namespace backend.GraphQL.Users
{
    [ExtendObjectType(name: "Mutation")]
    public class UserMutations
    {
        [UseAppDbContext]
        public async Task<User> AddUserAsync(AddUserInput input,
        [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = input.UserName,
                Email = input.Email,
                ImageURI = input.ImageURI,
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return user;
        }

        [UseAppDbContext]
        public async Task<User> EditUserAsync(EditUserInput input,
                [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(int.Parse(input.UserId));

            user.UserName = input.UserName ?? user.UserName;
            user.Email = input.Email ?? user.Email;
            user.ImageURI = input.ImageURI ?? user.ImageURI;

            await context.SaveChangesAsync(cancellationToken);

            return user;
        }

    }
}
