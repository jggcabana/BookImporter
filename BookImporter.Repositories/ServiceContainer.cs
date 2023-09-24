using BookImporter.Repositories.Interfaces;
using BookImporter.Repositories.Persistence;
using BookImporter.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Repositories
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
                });

            return services;
        }
    }
}
