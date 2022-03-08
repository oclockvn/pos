using System.Linq.Expressions;

namespace pos.core.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int currentPage, int take = 20) where T : class
        {
            var skip = Math.Max(0, currentPage) * take;
            return query.Skip(skip).Take(take);
        }

        public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> query, Expression<Func<T, object>> orderBy, bool asc = true)
        {
            if (asc)
            {
                return query.OrderBy(orderBy);
            }
            else
            {
                return query.OrderByDescending(orderBy);
            }
        }
    }
}
