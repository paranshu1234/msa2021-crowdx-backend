using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using backend.Data;

namespace backend.Extensions
{
    public class UseAppDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
           IDescriptorContext context,
           IObjectFieldDescriptor descriptor,
           MemberInfo member)
        {
            descriptor.UseDbContext<AppDbContext>();
        }
    }
}
