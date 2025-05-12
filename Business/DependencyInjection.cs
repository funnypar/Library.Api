using Business.Services.Implementations;
using Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using Business.Validators;

namespace Business
{
    public static class DependencyInjection
    {
        public static void AddBusinessServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBookServices, BookServices>();
            builder.Services.AddScoped<IAuthorServices, AuthorServices>();
            builder.Services.AddScoped<IPublisherServices, PublisherServices>();
            builder.Services.AddScoped<IBookTagServices, BookTagServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IImageServices, ImageServices>();


            builder.Services.AddValidatorsFromAssemblyContaining<BookValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<AuthorValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<PublisherValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<BookTagValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ImageValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RequestBooksOptionsValidator>();
        }
    }
}
