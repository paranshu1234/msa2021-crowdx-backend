using backend.Data;
using backend.GraphQL.Comments;
using backend.GraphQL.Creators;
using backend.GraphQL.Posts;
using backend.GraphQL.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters =
                            new TokenValidationParameters
                            {
                                ValidIssuer = "CrowdX",
                                ValidAudience = "MSA-Student",
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = signingKey
                            };
                    });

            services
                  .AddGraphQLServer()
                  .AddQueryType(d => d.Name("Query"))
                  .AddTypeExtension<PostQueries>()
                  .AddTypeExtension<CreatorQueries>()
                  .AddTypeExtension<UserQueries>()
                  .AddMutationType(d => d.Name("Mutation"))
                  .AddTypeExtension<UserMutations>()
                  .AddTypeExtension<CreatorMutations>()
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

            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();

                // Voyager middleware at default path /ui/voyager
                endpoints.MapGraphQLVoyager();
            });
        }
    }
}
