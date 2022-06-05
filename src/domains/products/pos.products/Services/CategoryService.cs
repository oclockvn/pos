using Light.GuardClauses;
using Microsoft.EntityFrameworkCore;
using pos.core;
using pos.core.Data;
using pos.core.Extensions;
using pos.core.Models;
using pos.products.Models;

namespace pos.products.Services
{
    public interface ICategoryService
    {
        Task<Result<CategoryCreate.Response>> CreateCategoryAsync(CategoryCreate.Request category);

        Task<Paging.Response<CategoryList.Response>> GetCategoryList(Paging.Request<CategoryList.Request> request);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public CategoryService(ITenantDbContextFactory dbContextFactory)
        {
            _tenantDbContextFactory = dbContextFactory;
        }

        public async Task<Result<CategoryCreate.Response>> CreateCategoryAsync(CategoryCreate.Request request)
        {
            request.MustNotBeNull();
            request.Name.MustNotBeNullOrWhiteSpace();

            static Result<CategoryCreate.Response> FailedResult(StatusCode statusCode)
            {
                return new Result<CategoryCreate.Response>(statusCode);
            }

            var category = new core.Entities.Category
            {
                Name = request.Name
            };

            using var context = _tenantDbContextFactory.CreateDbContext();

            if (await CategoryNameExistAsync(context, request.Name))
            {
                return FailedResult(StatusCode.Data_already_exist);
            }

            category = context.Categories.Add(category).Entity;
            await context.SaveChangesAsync();

            return new Result<CategoryCreate.Response>(
                new CategoryCreate.Response { Id = category.Id });
        }

        public async Task<Paging.Response<CategoryList.Response>> GetCategoryList(
            Paging.Request<CategoryList.Request> request)
        {
            using var context = _tenantDbContextFactory.CreateDbContext();

            var query = context.Categories.OrderByDescending(x => x.Id);
            var count = await query.CountAsync();
            var result = await query
                .Paging(request.CurrentPage, 100) // todo: paging by item per page
                .Select(x => new CategoryList.Response
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            return new Paging.Response<CategoryList.Response>(
                result,
                count,
                request.CurrentPage);
        }

        private static Task<bool> CategoryNameExistAsync(TenantDbContext context, string name)
        {
            return context.Categories
                .Where(x => x.Name == name)
                .AnyAsync();
        }
    }
}
