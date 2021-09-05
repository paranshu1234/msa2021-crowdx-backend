using backend.Data;
using backend.GraphQL.Comments;
using backend.GraphQL.Creators;
using backend.GraphQL.Posts;
using backend.GraphQL.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                  .AddGraphQLServer()
                  .AddQueryType(d => d.Name("Query"))
                  .AddTypeExtension<PostQueries>()
                  .AddTypeExtension<CreatorQueries>()
                  .AddTypeExtension<UserQueries>()
                  .AddMutationType(d => d.Name("Mutation"))
                  .AddTypeExtension<UserMutations>()
                  .AddTypeExtension<PostMutations>()
                  .AddTypeExtension<CommentMutations>()
                  .AddType<PostType>()
                  .AddType<CreatorType>()
                  .AddType<UserType>()
                  .AddType<CommentType>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();

                // Voyager middleware at default path /ui/voyager
                endpoints.MapGraphQLVoyager();
            });
        }
    }
}
