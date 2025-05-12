using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static void AddDataAccessServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
            builder.Services.AddScoped<IBookTagRepository, BookTagRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();




            builder.Services.AddDbContext<DataContext.DataContext>(options =>
            {
                options.UseSqlServer(builder?.Configuration?.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
