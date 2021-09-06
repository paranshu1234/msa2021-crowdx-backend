
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using backend.Model;
using backend.Data;
using backend.Extensions;
namespace backend.GraphQL.Creators
{
    [ExtendObjectType(name: "Mutation")]
    public class CreatorMutations
    {
        [UseAppDbContext]
        public async Task<Creator> AddCreatorAsync(AddCreatorInput input,
        [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var creator = new Creator
            {
                CreatorName = input.CreatorName,
                CoverImageURI = input.CoverImageURI,
                AvatarImageURI = input.AvatarImageURI,
            };

            context.Creators.Add(creator);
            await context.SaveChangesAsync(cancellationToken);

            return creator;
        }

        [UseAppDbContext]
        public async Task<Creator> EditCreatorAsync(EditCreatorInput input,
                [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var creator = await context.Creators.FindAsync(int.Parse(input.CreatorId));

            creator.CreatorName = input.CreatorName ?? creator.CreatorName;
            creator.CoverImageURI = input.CoverImageURI ?? creator.CoverImageURI;
            creator.AvatarImageURI = input.AvatarImageURI ?? creator.AvatarImageURI;

            await context.SaveChangesAsync(cancellationToken);

            return creator;
        }
    }
}
