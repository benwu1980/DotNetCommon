using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DotNetCommon.Extension
{
    /// <summary>
    /// 查询扩展
    /// </summary>
    public static class QueryableExtension
    {
        /// <summary>
        /// 查询扩展排序
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="query">查询</param>
        /// <param name="propertyName">排序史</param>
        /// <param name="desc">是否倒序</param>
        /// <returns>返回一个查询</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, bool desc)
        {
            var entity = Expression.Parameter(typeof(T), "c");
            var property = Expression.Property(entity, propertyName);
            var castExpression = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(castExpression, entity);

            return desc ? Queryable.OrderByDescending(query, lambda) : Queryable.OrderBy(query, lambda);
        }
    }
}
